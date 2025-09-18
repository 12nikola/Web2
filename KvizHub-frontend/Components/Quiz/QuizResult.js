import { useEffect, useState } from "react";
import axiosInstance from "../../../../axios";
import Question from "../../../../Web2/KvizHub/KvizHub/Models/Quiz";
import Answer from "../../../../Web2/KvizHub/KvizHub/Models/Answers";

const QuizResult = ({ attempt }) => {
  const [questionList, setQuestionList] = useState([]);
  const [pending, setPending] = useState(true);

  const quizId = attempt.information.quizID;

  useEffect(() => {
    const loadResultData = async () => {
      try {
        const requests = attempt.quizSolutionAttempt.questionSolutions.map((questionId) =>
          axiosInstance.get(`questions/${questionId}/solution`)
        );

        const responses = await Promise.all(requests);
        const solutions = responses.map((r) => r.data);

        const mappedQuestions = solutions.map((solution) => {
          const question = new Question({
            questionID: solution.questionID,
            text: solution.questionText,
            type: solution.questionType,
            category: solution.categoryName,
            answer: solution.answers[0],
            answers: Answer.fromJsonArray(solution.answers),
          });

          if (question.type === "MultipleChoice" || question.type === "SingleChoice") {
            question.userAnswers = solution.userAnswers;
          } else {
            question.userAnswer = new Answer({ answerText: solution.userAnswers[0] });
          }

          return question;
        });

        setQuestionList(mappedQuestions);
      } catch (err) {
        console.error("Failed to fetch quiz result details", err);
      } finally {
        setPending(false);
      }
    };

    loadResultData();
  }, [attempt, quizId]);

  if (pending) return <p>Loading quiz results...</p>;

  return (
    <div className="container mt-4">
      {/* quiz info */}
      <div className="card mb-4">
        <div className="card-body">
          <h2>{attempt.information.quizTitle}</h2>
          <p>{attempt.information.quizDescription}</p>
          <p><strong>Difficulty:</strong> {attempt.information.difficultyLevel}</p>
          <p><strong>Number of questions:</strong> {questionList.length}</p>
        </div>
      </div>

      {/* results summary */}
      <div className="card mb-4">
        <div className="card-body">
          <p><strong>Score:</strong> {attempt.quizSolutionAttempt.scorePoints} points ({attempt.quizSolutionAttempt.scorePercentage}%)</p>
          <p><strong>Correct:</strong> {attempt.quizSolutionAttempt.numberOfCorrectAnswers} / {attempt.quizSolutionAttempt.numberOfAllAnswers}</p>
          <p><strong>Wrong:</strong> {attempt.quizSolutionAttempt.numberOfBadAnswers}</p>
          <p><strong>Duration:</strong> {attempt.quizSolutionAttempt.duration}</p>
          <p><strong>Attempt Date:</strong> {new Date(attempt.quizSolutionAttempt.attemptDate).toLocaleString()}</p>
        </div>
      </div>

      {/* question details */}
      {questionList.map((question) => (
        <div key={question.questionID} className="card mb-3">
          <div className="card-body">
            <div className="card mb-3 border border-secondary">
              <h5 className="mt-1">{question.text}</h5>
            </div>
            <div className="card mb-3 border border-secondary">
              <h6 className="mt-1">Question Type: {question.type}</h6>
            </div>
            <p className="mb-3"><b>Answers:</b></p>

            {(question.type === "MultipleChoice" || question.type === "SingleChoice") ? (
              question.answers.map((ans) => (
                <p
                  key={ans.answerID}
                  className={ans.isCorrect ? "card text-success border border-secondary" : "card border border-secondary"}
                >
                  <b>{ans.answerText}</b>
                </p>
              ))
            ) : (
              <p
                key={question.answer.answerID}
                className={question.answer.isCorrect ? "card text-success border border-secondary" : "card border border-secondary"}
              >
                <b>{question.answer.answerText}</b>
              </p>
            )}

            <p><strong>Your Answer(s):</strong></p>
            {(question.type === "MultipleChoice" || question.type === "SingleChoice") ? (
              question.userAnswers?.length > 0 ? (
                question.userAnswers.map((ua, i) => (
                  <p
                    key={i}
                    className={
                      question.answers.find((ans) => ans.answerText === ua && ans.isCorrect)
                        ? "text-success card mb-3 border border-secondary"
                        : "text-danger card mb-3 border border-secondary"
                    }
                  >
                    <b>{ua}</b>
                  </p>
                ))
              ) : (
                <p className="card mb-3 border border-secondary text-muted">No answer submitted</p>
              )
            ) : question.userAnswer ? (
              <p
                className={
                  question.answer.answerText === question.userAnswer.answerText
                    ? "text-success card mb-3 border border-secondary"
                    : "text-danger card mb-3 border border-secondary"
                }
              >
                <b>{question.userAnswer.answerText}</b>
              </p>
            ) : (
              <p className="text-muted card mb-3 border border-secondary">No answer submitted</p>
            )}
          </div>
        </div>
      ))}
    </div>
  );
};

export default QuizResult;
