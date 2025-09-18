import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { Container, Card, Spinner, ListGroup } from "react-bootstrap";
import axiosInstance from "../../../../axios";

const QuizResultDetail = () => {
  const { id } = useParams();
  const [quizSolution, setQuizSolution] = useState(null);
  const [questionDetails, setQuestionDetails] = useState([]);
  const [pending, setPending] = useState(true);

  useEffect(() => {
    const loadSolution = async () => {
      try {
        setPending(true);

        const res = await axiosInstance.get(`/quizzes/${id}/solution`);
        setQuizSolution(res.data);

        const detailRequests = res.data.quizSolutionAttempt.questionSolutions.map((qid) =>
          axiosInstance.get(`/questions/${qid}/solution`).then((r) => r.data)
        );

        const resolvedDetails = await Promise.all(detailRequests);
        setQuestionDetails(resolvedDetails);
      } catch (err) {
        console.error("Failed to fetch quiz solution", err);
      } finally {
        setPending(false);
      }
    };

    loadSolution();
  }, [id]);

  if (pending) {
    return (
      <Container className="my-4 d-flex justify-content-center">
        <Spinner animation="border" />
      </Container>
    );
  }

  if (!quizSolution) {
    return <Container className="my-4">No quiz solution found.</Container>;
  }

  const { information, quizSolutionAttempt } = quizSolution;

  return (
    <Container className="my-4">
      <Card className="mb-4">
        <Card.Body>
          <h3 className="text-center">{information.quizTitle}</h3>
          <p><strong>Description:</strong> {information.quizDescription}</p>
          <p><strong>Difficulty:</strong> {information.difficultyLevel}</p>
          <p><strong>Score:</strong> {quizSolutionAttempt.scorePoints} points ({quizSolutionAttempt.scorePercentage}%)</p>
          <p><strong>Correct:</strong> {quizSolutionAttempt.numberOfCorrectAnswers} / {quizSolutionAttempt.numberOfAllAnswers}</p>
          <p><strong>Wrong:</strong> {quizSolutionAttempt.numberOfBadAnswers}</p>
          <p><strong>Attempt Date:</strong> {new Date(quizSolutionAttempt.attemptDate).toLocaleString()}</p>
          <p><strong>Duration:</strong> {quizSolutionAttempt.duration}</p>
        </Card.Body>
      </Card>

      {questionDetails.map((q) => (
        <Card key={q.questionSolutionID} className="mb-3">
          <Card.Body>
            <h5 className="text-center">{q.questionText}</h5>
            <p><strong>Category:</strong> {q.categoryName}</p>
            <p><strong>Type:</strong> {q.questionType}</p>

            {(q.questionType === "SingleChoice" || q.questionType === "MultipleChoice") && (
              <>
                <p className="text-center"><strong>Possible Answers:</strong></p>
                <ListGroup className="mb-2">
                  {q.answers.map((a) => (
                    <ListGroup.Item className="text-center" key={a.answerID}>
                      {a.answerText}
                    </ListGroup.Item>
                  ))}
                </ListGroup>
              </>
            )}

            <p className="text-center"><strong>User's Answer(s):</strong></p>
            {q.userAnswers?.length > 0 ? (
              <ListGroup>
                {q.userAnswers.map((ua, i) => (
                  <ListGroup.Item className="text-center" key={i}>
                    {ua}
                  </ListGroup.Item>
                ))}
              </ListGroup>
            ) : (
              <p className="text-muted">No answer submitted</p>
            )}
          </Card.Body>
        </Card>
      ))}
    </Container>
  );
};

export default QuizResultDetail;
