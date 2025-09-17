import { useState, useEffect } from "react";
import { useNavigate } from 'react-router-dom';
import { Form, Button, Table } from "react-bootstrap";
import axiosInstance from "../../../../axios";

function QuizCreator() {
  const [categoryList, setCategoryList] = useState([]);
  const [categoryError, setCategoryError] = useState(false);
  const [answerError, setAnswerError] = useState(false);
  const [duplicateAnswerError, setDuplicateAnswerError] = useState(false);
  const [formError, setFormError] = useState("");
  const navigate = useNavigate();

  const [quizData, setQuizData] = useState({
    title: "",
    description: "",
    difficultyLevel: "Easy",
    timeLimit: "00:05:00",
    questions: [],
  });

  const [draftQuestion, setDraftQuestion] = useState({
    type: "SingleOption",
    text: "",
    questionCategoryID: "",
    answers: Array(4).fill(null).map(() => ({ answerText: "", isCorrect: false })),
    fillInAnswer: "",
    trueFalseAnswer: null,
  });

  useEffect(() => {
    axiosInstance.get("/categories").then((res) => setCategoryList(res.data));
  }, []);

  const submitQuiz = async () => {
    try {
      if (quizData.title.trim().length < 3 || quizData.description.trim().length < 3) {
        setFormError("Quiz title and description must be at least 3 characters.");
        return;
      }

      if (quizData.questions.length === 0) {
        setFormError("Quiz must contain at least one question.");
        return;
      }

      const [hours, minutes, seconds] = quizData.timeLimit.split(":").map(Number);
      if (hours === 0 && minutes < 1) {
        return;
      }

      const payload = {
        ...quizData,
        timeLimit: { Hours: hours, Minutes: minutes, Seconds: seconds },
        questions: quizData.questions.map((q) => {
          if (q.type === "TextEntry" || q.type === "Boolean") {
            const { answers, ...rest } = q;
            return rest;
          }
          return q;
        }),
      };

      await axiosInstance.post("/quizzes", payload);
      navigate("/quizzes");
    } catch (err) {
      setFormError("Failed to create quiz. Please try again.");
    }
  };

  const addQuestionToQuiz = () => {
    if (!draftQuestion.questionCategoryID) {
      setCategoryError(true);
      return;
    }
    setCategoryError(false);

    if (draftQuestion.text.trim().length < 3) {
      return;
    }

    if (draftQuestion.type === "SingleOption" || draftQuestion.type === "MultipleOption") {
      const normalizedAnswers = draftQuestion.answers.map(a => a.answerText.trim().toLowerCase());
      const uniqueAnswers = new Set(normalizedAnswers);
      if (uniqueAnswers.size !== normalizedAnswers.length) {
        setDuplicateAnswerError(true);
        return;
      }
    }

    if (draftQuestion.type === "SingleOption") {
      const correctCount = draftQuestion.answers.filter((a) => a.isCorrect).length;
      if (correctCount !== 1) {
        setAnswerError(true);
        return;
      }
    }

    if (draftQuestion.type === "MultipleOption") {
      const correctCount = draftQuestion.answers.filter((a) => a.isCorrect).length;
      if (correctCount < 2) {
        setAnswerError(true);
        return;
      }
    }

    setAnswerError(false);
    setDuplicateAnswerError(false);

    setQuizData((prev) => ({
      ...prev,
      questions: [...prev.questions, { ...draftQuestion, answers: draftQuestion.answers.map((a) => ({ ...a })) }],
    }));

    setDraftQuestion({
      type: "SingleOption",
      text: "",
      questionCategoryID: "",
      answers: Array(4).fill(null).map(() => ({ answerText: "", isCorrect: false })),
      fillInAnswer: "",
      trueFalseAnswer: null,
    });
  };

  const removeQuestion = (index) => {
    setQuizData((prev) => ({
      ...prev,
      questions: prev.questions.filter((_, i) => i !== index),
    }));
  };

  const invalidTime = (() => {
    if (!quizData.timeLimit) return false;
    const regex = /^([0-1][0-9]|2[0-3]):([0-5][0-9]):([0-5][0-9])$/;
    if (!regex.test(quizData.timeLimit)) return true;
    const [h, m, s] = quizData.timeLimit.split(":").map(Number);
    const totalSeconds = h * 3600 + m * 60 + s;
    return totalSeconds < 60;
  })();

  return (
    <div className="p-3">
      <h3 className="text-center">Create Quiz</h3>
      <Form>
      </Form>

      <div className="text-center mt-4">
        {formError && <div className="text-danger mb-2">{formError}</div>}
        <Button variant="primary" onClick={submitQuiz}>Create Quiz</Button>
      </div>
    </div>
  );
}

export default QuizCreator;
