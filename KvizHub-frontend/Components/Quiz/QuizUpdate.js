import { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { Modal, Form, Button, Table } from "react-bootstrap";
import axiosInstance from "../../../../axios";

function QuizUpdate() {
  const { id } = useParams();
  const navigate = useNavigate();

  const [loading, setLoading] = useState(true);
  const [categories, setCategories] = useState([]);
  const [showModal, setShowModal] = useState(false);

  const [quiz, setQuiz] = useState({
    title: "",
    description: "",
    difficultyLevel: "Easy",
    timeLimit: "00:05:00",
    available: true,
    questions: [],
  });

  const [newQuestion, setNewQuestion] = useState({
    type: "SingleOption",
    text: "",
    questionCategoryID: "",
    answers: Array(4).fill(null).map(() => ({ answerText: "", isCorrect: false })),
    fillInAnswer: "",
    trueFalseAnswer: null,
  });

  const [categoryError, setCategoryError] = useState(false);
  const [answerError, setAnswerError] = useState(false);
  const [formError, setFormError] = useState("");
  const [duplicateAnswerError, setDuplicateAnswerError] = useState(false);

  const handleAddQuestion = async () => {
    if (!newQuestion.questionCategoryID) {
      setCategoryError(true);
      return;
    }
    setCategoryError(false);

    if (newQuestion.text.trim().length < 3) return;

    if (newQuestion.type === "SingleOption") {
      const correctCount = newQuestion.answers.filter(a => a.Correct).length;
      if (correctCount !== 1) {
        setAnswerError(true);
        return;
      }
    }

    if (newQuestion.type === "MultipleOption") {
      const correctCount = newQuestion.answers.filter(a => a.isCorrect).length;
      if (correctCount < 2) {
        setAnswerError(true);
        return;
      }
    }

    setAnswerError(false);

    if (newQuestion.type === "SingleOption" || newQuestion.type === "MultipleOption") {
      const normalizedAnswers = newQuestion.answers.map(a => a.answerText.trim().toLowerCase());
      if (new Set(normalizedAnswers).size !== normalizedAnswers.length) {
        setDuplicateAnswerError(true);
        return;
      }
    }

    try {
      const payload = { ...newQuestion };
      if (payload.type === "TextEntry" || payload.type === "Boolean") payload.answers = null;

      await axiosInstance.post(`/quizzes/${id}/questions`, payload);

      const res = await axiosInstance.get(`/quizzes/${id}/questions/answers`);
      const updatedQuestions = res.data.map(q => ({
        ...q,
        answers: (q.type === "SingleOption" || q.type === "MultipleOption")
          ? q.answers.map(a => ({ ...a }))
          : [],
      }));

      setQuiz(prev => ({ ...prev, questions: updatedQuestions }));
      setShowModal(false);
      resetNewQuestion();
    } catch (err) {
      console.error("Failed to add question", err);
    }
  };

  const resetNewQuestion = () => {
    setNewQuestion({
      type: "SingleOption",
      text: "",
      questionCategoryID: "",
      answers: Array(4).fill(null).map(() => ({ answerText: "", isCorrect: false })),
      fillInAnswer: "",
      trueFalseAnswer: null,
    });
  };

  const handleDeleteQuestion = async (questionID) => {
    if (quiz.questions.length <= 1) return;

    try {
      await axiosInstance.delete(`/quizzes/questions/${questionID}`);
      setQuiz(prev => ({
        ...prev,
        questions: prev.questions.filter(q => q.questionID !== questionID),
      }));
    } catch (err) {
      console.error("Failed to delete question", err);
    }
  };

  const handleUpdateQuiz = async () => {
    try {
      const [hours, minutes, seconds] = quiz.timeLimit.split(":").map(Number);
      if (hours === 0 && minutes < 1) return;

      const payload = {
        title: quiz.title,
        description: quiz.description,
        difficultyLevel: quiz.difficultyLevel,
        timeLimit: { hours, minutes, seconds },
        available: quiz.available,
        questions: quiz.questions,
      };

      await axiosInstance.patch(`/quizzes/${id}`, payload);
      navigate("/quizzes");
    } catch (err) {
      console.error(err);
      setFormError("Failed to update quiz. Please check your inputs.");
    }
  };

  const timeInvalid = (() => {
    if (!quiz.timeLimit) return false;
    const regex = /^([0-1][0-9]|2[0-3]):([0-5][0-9]):([0-5][0-9])$/;
    if (!regex.test(quiz.timeLimit)) return true;
    const [h, m, s] = quiz.timeLimit.split(":").map(Number);
    return h * 3600 + m * 60 + s < 60;
  })();
a
  useEffect(() => {
    const fetchQuiz = async () => {
      try {
        const res = await axiosInstance.get(`/quizzes/${id}`);
        const data = res.data;
        const timeLimit = `${String(data.timeLimit.hours).padStart(2,"0")}:${String(data.timeLimit.minutes).padStart(2,"0")}:${String(data.timeLimit.seconds).padStart(2,"0")}`;
        setQuiz({
          title: data.title,
          description: data.description,
          difficultyLevel: data.difficultyLevel,
          timeLimit,
          available: data.available ?? true,
          questions: [],
        });
      } catch (err) { console.error(err); }
    };

    const fetchCategories = async () => {
      const res = await axiosInstance.get("/categories");
      setCategories(res.data);
    };

    fetchCategories();
    fetchQuiz();
  }, [id]);

  useEffect(() => {
    const fetchQuestions = async () => {
      try {
        const res = await axiosInstance.get(`/quizzes/${id}/questions/answers`);
        const questions = res.data.map(q => ({
          ...q,
          answers: (q.type === "SingleOption" || q.type === "MultipleOption")
            ? q.answers.map(a => ({ ...a }))
            : [],
        }));
        setQuiz(prev => ({ ...prev, questions }));
        setLoading(false);
      } catch (err) { console.error(err); }
    };

    if (quiz.title) fetchQuestions();
  }, [quiz.title, id]);

  if (loading) return <div>Loading...</div>;

  return (
    <div className="p-3">
      <h3 className="text-center">Update Quiz</h3>
      <Form>
        <div className="border border-secondary card p-2">
          <Form.Group className="mb-3">
            <Form.Label>Title</Form.Label>
            <Form.Control
              type="text"
              value={quiz.title}
              onChange={(e) => setQuiz({ ...quiz, title: e.target.value })}
              isInvalid={quiz.title.trim().length > 0 && quiz.title.trim().length < 3} />
          </Form.Group>

          <Form.Group className="mb-3">
            <Form.Label>Description</Form.Label>
            <Form.Control
              type="text"
              value={quiz.description}
              onChange={(e) => setQuiz({ ...quiz, description: e.target.value })}
              isInvalid={quiz.description.trim().length > 0 && quiz.description.trim().length < 3} />
          </Form.Group>

          <Form.Group className="mb-3">
            <Form.Label>Difficulty Level</Form.Label>
            <Form.Select
              value={quiz.difficultyLevel}
              onChange={(e) => setQuiz({ ...quiz, difficultyLevel: e.target.value })}>
              <option value="Easy">Easy</option>
              <option value="Medium">Medium</option>
              <option value="Hard">Hard</option>
            </Form.Select>
          </Form.Group>

          <Form.Group className="mb-3">
            <Form.Label>Time Limit (HH:MM:SS)</Form.Label>
            <Form.Control
              type="text"
              value={quiz.timeLimit}
              onChange={(e) => setQuiz({ ...quiz, timeLimit: e.target.value })}
              isInvalid={timeInvalid} />
          </Form.Group>

          <Form.Group className="mb-3">
            <Form.Check
              type="checkbox"
              label="Available"
              checked={quiz.available}
              onChange={(e) => setQuiz({ ...quiz, available: e.target.checked })}
            />
          </Form.Group>
        </div>

        <h4 className="text-center mt-3 mb-4">Questions</h4>
        <div className="border border-secondary card p-2">
          <Table striped bordered>
            <thead>
              <tr className="text-center">
                <th>Text</th>
                <th>Type</th>
                <th>Category</th>
                <th>Answers</th>
                <th>Actions</th>
              </tr>
            </thead>
            <tbody>
              {quiz.questions.map(q => (
                <tr key={q.questionID} className="text-center">
                  <td>{q.text}</td>
                  <td>{q.type}</td>
                  <td>{q.questionCategory}</td>
                  <td style={{ whiteSpace: "normal", width: "50%", wordBreak: "break-word" }}>
                    {q.type === "SingleChoice" || q.type === "MultipleChoice"
                      ? q.answers.map(a => a.answerText + (a.isCorrect ? " (correct)" : "")).join(", ")
                      : q.answer?.answerText || ""}
                  </td>
                  <td>
                    <Button
                      variant="danger"
                      size="sm"
                      onClick={() => handleDeleteQuestion(q.questionID)}
                      disabled={quiz.questions.length <= 1}>
                      Delete
                    </Button>
                  </td>
                </tr>
              ))}
            </tbody>
          </Table>

          <div className="text-center">
            <Button className="mb-2" variant="success" onClick={() => setShowModal(true)}>
              Add Question
            </Button>
          </div>
        </div>

        <div className="mt-3 text-center">
          <Button variant="primary" onClick={handleUpdateQuiz}>Update Quiz Informations</Button>
          {formError && <div className="text-danger mb-2">{formError}</div>}
        </div>
      </Form>

      <Modal show={showModal} onHide={() => setShowModal(false)}>
        <Modal.Header closeButton>
          <Modal.Title>Add Question</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form.Group className="mb-2">
            <Form.Label>Question Text</Form.Label>
            <Form.Control
              type="text"
              value={newQuestion.text}
              onChange={(e) => setNewQuestion({ ...newQuestion, text: e.target.value })}
              isInvalid={newQuestion.text.trim().length > 0 && newQuestion.text.trim().length < 3} />
          </Form.Group>

          <Form.Group className="mb-2">
            <Form.Label>Category</Form.Label>
            <Form.Select
              value={newQuestion.questionCategoryID}
              isInvalid={categoryError}
              onChange={(e) => setNewQuestion({ ...newQuestion, questionCategoryID: parseInt(e.target.value) })}>
              <option value="">Select Category</option>
              {categories.map(c => <option key={c.categoryID} value={c.categoryID}>{c.name}</option>)}
            </Form.Select>
          </Form.Group>

          <Form.Group className="mb-2">
            <Form.Label>Type</Form.Label>
            <Form.Select
              value={newQuestion.type}
              onChange={(e) => setNewQuestion({ ...newQuestion, type: e.target.value })}>
              <option value="SingleOption">SingleOption</option>
              <option value="MultipleOption">MultipleOption</option>
              <option value="Boolean">Boolean</option>
              <option value="TextEntry">TextEntry</option>
            </Form.Select>
          </Form.Group>

          {(newQuestion.type === "SingleOption" || newQuestion.type === "MultipleOption") && (
            <div>
              <h6>Answers:</h6>
              {newQuestion.answers.map((a, ai) => (
                <div key={ai} className="d-flex align-items-center mb-2">
                  <Form.Control
                    type="text"
                    placeholder={`Answer ${ai + 1}`}
                    value={a.answerText}
                    onChange={(e) => {
                      const updated = { ...newQuestion };
                      updated.answers[ai].answerText = e.target.value;
                      setNewQuestion(updated);
                    }}
                    className="me-2"
                    isInvalid={(a.answerText.trim().length > 0 && a.answerText.trim().length < 3) || duplicateAnswerError} />

                  {newQuestion.type === "SingleOption" ? (
                    <Form.Check
                      type="radio"
                      name="correctAnswer"
                      checked={a.isCorrect}
                      isInvalid={answerError}
                      onChange={() => {
                        const updated = { ...newQuestion };
                        updated.answers = updated.answers.map((ans, idx) => ({ ...ans, isCorrect: idx === ai }));
                        setNewQuestion(updated);
                        setAnswerError(false);
                      }}
                      label="Correct" />
                  ) : (
                    <Form.Check
                      type="checkbox"
                      checked={a.isCorrect}
                      isInvalid={answerError}
                      onChange={(e) => {
                        const updated = { ...newQuestion };
                        updated.answers[ai].isCorrect = e.target.checked;
                        setNewQuestion(updated);
                        setAnswerError(false);
                      }}
                      label="Correct" />
                  )}
                </div>
              ))}
            </div>
          )}

          {newQuestion.type === "Boolean" && (
            <Form.Group className="mb-2">
              <Form.Check
                type="radio"
                label="True"
                checked={newQuestion.trueFalseAnswer === true}
                onChange={() => setNewQuestion({ ...newQuestion, trueFalseAnswer: true })} />
              <Form.Check
                type="radio"
                label="False"
                checked={newQuestion.trueFalseAnswer === false}
                onChange={() => setNewQuestion({ ...newQuestion, trueFalseAnswer: false })} />
            </Form.Group>
          )}

          {newQuestion.type === "TextEntry" && (
            <Form.Group className="mb-2">
              <Form.Label>Correct Answer</Form.Label>
              <Form.Control
                type="text"
                value={newQuestion.fillInAnswer}
                onChange={(e) => setNewQuestion({ ...newQuestion, fillInAnswer: e.target.value })}
                isInvalid={newQuestion.fillInAnswer.trim().length > 0 && newQuestion.fillInAnswer.trim().length < 3} />
            </Form.Group>
          )}
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={() => setShowModal(false)}>Cancel</Button>
          <Button variant="success" onClick={handleAddQuestion}>Add Question</Button>
        </Modal.Footer>
      </Modal>
    </div>
  );
}

export default QuizUpdate;
