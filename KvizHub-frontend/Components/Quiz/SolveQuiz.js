import { useState, useEffect, useRef } from "react";
import { Container, Card, Button, Spinner, Alert } from "react-bootstrap";
import { useParams } from "react-router-dom";
import Question from "../../../../Web2/KvizHub/KvizHub/Models/Quiz/";
import QuizResult from "../QuizResult/QuizResult";
import axiosInstance from "../../../../axios";

function SolveQuiz() {
  const { id } = useParams();
  const [quizInfo, setQuizInfo] = useState(null);
  const [questionList, setQuestionList] = useState([]);
  const [started, setStarted] = useState(false);
  const [finished, setFinished] = useState(false);
  const [resultData, setResultData] = useState(null);
  const [loadingQuizInfo, setLoadingQuizInfo] = useState(true);
  const [loadingQuestionList, setLoadingQuestionList] = useState(false);
  const [validationMessages, setValidationMessages] = useState([]);
  const [timeRemaining, setTimeRemaining] = useState(null);

  const errorBlockRef = useRef(null);
  const timerRef = useRef(null);

  useEffect(() => {
    const loadQuizInfo = async () => {
      setLoadingQuizInfo(true);
      try {
        const res = await axiosInstance.get(`quizzes/${id}`);
        setQuizInfo(res.data);
      } catch (err) {
        console.error("Failed to fetch quiz details", err);
      } finally {
        setLoadingQuizInfo(false);
      }
    };

    loadQuizInfo();
  }, [id]);

  useEffect(() => {
    if (started && timeRemaining !== null) {
      if (timeRemaining <= 0) {
        submitSolutions();
        clearInterval(timerRef.current);
      } else {
        timerRef.current = setInterval(() => {
          setTimeRemaining((prev) => prev - 1);
        }, 1000);
        return () => clearInterval(timerRef.current);
      }
    }
  }, [started, timeRemaining]);

  const formatDuration = (seconds) => {
    const h = Math.floor(seconds / 3600);
    const m = Math.floor((seconds % 3600) / 60);
    const s = seconds % 60;

    return `${h > 0 ? h + "h " : ""}${m}m ${s}s`;
  };

  const startQuiz = async () => {
    setLoadingQuestionList(true);

    try {
      const res = await axiosInstance.get(`quizzes/${id}/questions`);
      setQuestionList(Question.fromJsonArray(res.data));
      setStarted(true);

      const { timeLimit } = quizInfo;
      const totalSeconds =
        (timeLimit.days || 0) * 86400 +
        (timeLimit.hours || 0) * 3600 +
        (timeLimit.minutes || 0) * 60 +
        (timeLimit.seconds || 0);

      setTimeRemaining(totalSeconds);
    } catch (err) {
      console.error("Failed to fetch questions", err);
    } finally {
      setLoadingQuestionList(false);
    }
  };

  const finishQuiz = () => {
    const errors = [];

    questionList.forEach((q, index) => {
      if (q.type === "SingleOption" && !q.userAnswer) {
        errors.push(`Question ${index + 1}: Please select one answer.`);
      }

      if (q.type === "MultipleOption" && (!q.userAnswers || q.userAnswers.length < 2)) {
        errors.push(`Question ${index + 1}: Please select at least 2 answers.`);
      }

      if (q.type === "Boolean" && !q.userAnswer) {
        errors.push(`Question ${index + 1}: Please select True or False.`);
      }

      if (q.type === "TextEntry" && (!q.userAnswer || q.userAnswer.trim().length < 3)) {
        errors.push(`Question ${index + 1}: Please enter at least 3 characters.`);
      }
    });

    if (errors.length > 0) {
      setValidationMessages(errors);
      if (errorBlockRef.current) {
        errorBlockRef.current.scrollIntoView({ behavior: "smooth", block: "end" });
      }
      return;
    }

    setValidationMessages([]);
    submitSolutions();
  };

  const submitSolutions = async () => {
    try {
      const totalSeconds =
        (quizInfo.timeLimit?.days || 0) * 86400 +
        (quizInfo.timeLimit?.hours || 0) * 3600 +
        (quizInfo.timeLimit?.minutes || 0) * 60 +
        (quizInfo.timeLimit?.seconds || 0);

      const elapsedSeconds = Math.max(totalSeconds - timeRemaining, 0);

      const duration = {
        hours: Math.floor(elapsedSeconds / 3600),
        minutes: Math.floor((elapsedSeconds % 3600) / 60),
        seconds: elapsedSeconds % 60,
      };

      const solvedQuestions = questionList.map((q) => q.toSubmissionJSON());

      const payload = {
        Duration: duration,
        SolvesQuestions: solvedQuestions,
      };

      const res = await axiosInstance.post(`quizzes/${id}/solution`, payload);

      setResultData(res.data);
      setFinished(true);
    } catch (err) {
      console.error("Failed to submit quiz", err);
    }
  };

  if (loadingQuizInfo) {
    return (
      <Container className="my-4 d-flex justify-content-center">
        <Spinner animation="border" />
      </Container>
    );
  }

  return (
    <Container className="my-4 d-flex justify-content-center">
      <Card style={{ maxWidth: "600px", width: "100%" }}>
        <Card.Body className="text-center">
          {finished && resultData ? (
            <QuizResult attempt={resultData} />
          ) : (
            <div>
              {started && timeRemaining !== null && (
                <h5 className="text-danger">Time Left: {formatDuration(timeRemaining)}</h5>
              )}

              <Card.Title className="mb-3">{quizInfo.title}</Card.Title>
              <p><strong>Description:</strong> {quizInfo.description}</p>
              <p><strong>Difficulty:</strong> {quizInfo.difficultyLevel}</p>
              <p>
                <strong>Time Limit:</strong>{" "}
                {quizInfo.timeLimit?.days > 0 && `${quizInfo.timeLimit.days}d `}
                {quizInfo.timeLimit?.hours}h {quizInfo.timeLimit?.minutes}m {quizInfo.timeLimit?.seconds}s
              </p>
              <p><strong>Number of questions:</strong> {quizInfo.questionsID?.length}</p>
              <p><strong>Created:</strong> {new Date(quizInfo.creationDate).toLocaleDateString()}</p>

              {!started ? (
                <Button
                  variant="success"
                  onClick={startQuiz}
                  disabled={!quizInfo.available || loadingQuestionList}
                >
                  {loadingQuestionList ? <Spinner animation="border" size="sm" /> : "Start Quiz"}
                </Button>
              ) : (
                <div>
                  {validationMessages.length > 0 && (
                    <div ref={errorBlockRef} className="mb-3 text-start">
                      {validationMessages.map((err, i) => (
                        <Alert key={i} variant="danger" className="py-1">
                          {err}
                        </Alert>
                      ))}
                    </div>
                  )}

                  <div className="mt-4">
                    <h5>Questions</h5>
                    {questionList.map((q, index) => (
                      <Card key={q.questionID} className="my-3">
                        <Card.Header>
                          <strong>Question {index + 1}</strong> - {q.questionCategory}
                        </Card.Header>
                        <Card.Body>
                          <p><b>Text:</b> {q.text}</p>
                          <p><b>Type:</b> {q.type}</p>
                          <div className="card border border-secondary p-1 mb-1">
                        
                            {q.type === "SingleOption" &&
                              q.answers.map((ans) => (
                                <div key={ans.answerID} className="form-check">
                                  <input
                                    className="form-check-input border-primary"
                                    type="radio"
                                    name={`question-${q.questionID}`}
                                    value={ans.answerText}
                                    checked={q.userAnswer === ans.answerText}
                                    onChange={() => {
                                      const updated = [...questionList];
                                      const target = updated.find((qq) => qq.questionID === q.questionID);
                                      if (target) target.userAnswer = ans.answerText;
                                      setQuestionList(updated);
                                    }}
                                  />
                                  <label className="form-check-label">{ans.answerText}</label>
                                </div>
                              ))}

                            
                            {q.type === "MultipleOption" &&
                              q.answers.map((ans) => (
                                <div key={ans.answerID} className="form-check">
                                  <input
                                    className="form-check-input border-primary"
                                    type="checkbox"
                                    value={ans.answerText}
                                    checked={q.userAnswers?.includes(ans.answerText) || false}
                                    onChange={() => {
                                      const updated = [...questionList];
                                      const target = updated.find((qq) => qq.questionID === q.questionID);
                                      if (target) {
                                        if (!target.userAnswers) target.userAnswers = [];
                                        if (target.userAnswers.includes(ans.answerText)) {
                                          target.userAnswers = target.userAnswers.filter((a) => a !== ans.answerText);
                                        } else {
                                          target.userAnswers.push(ans.answerText);
                                        }
                                      }
                                      setQuestionList(updated);
                                    }}
                                  />
                                  <label className="form-check-label">{ans.answerText}</label>
                                </div>
                              ))}

                            
                            {q.type === "Boolean" &&
                              ["True", "False"].map((val) => (
                                <div key={val} className="form-check">
                                  <input
                                    className="form-check-input border-primary"
                                    type="radio"
                                    name={`question-${q.questionID}`}
                                    value={val}
                                    checked={q.userAnswer === val}
                                    onChange={() => {
                                      const updated = [...questionList];
                                      const target = updated.find((qq) => qq.questionID === q.questionID);
                                      if (target) target.userAnswer = val;
                                      setQuestionList(updated);
                                    }}
                                  />
                                  <label className="form-check-label">{val}</label>
                                </div>
                              ))}

                            
                            {q.type === "TextEntry" && (
                              <input
                                type="text"
                                className="form-control"
                                value={q.userAnswer}
                                onChange={(e) => {
                                  const updated = [...questionList];
                                  const target = updated.find((qq) => qq.questionID === q.questionID);
                                  if (target) target.userAnswer = e.target.value;
                                  setQuestionList(updated);
                                }}
                                placeholder="Write your answer here"
                              />
                            )}
                          </div>
                        </Card.Body>
                      </Card>
                    ))}
                  </div>
                  <div className="d-flex justify-content-center mt-3">
                    <Button variant="success" onClick={finishQuiz}>
                      Finish Quiz
                    </Button>
                  </div>
                </div>
              )}
            </div>
          )}
        </Card.Body>
      </Card>
    </Container>
  );
}

export default SolveQuiz;
