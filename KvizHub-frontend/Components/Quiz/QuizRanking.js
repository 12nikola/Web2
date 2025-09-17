import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { Container, Card, Table, Spinner, Form, Button } from "react-bootstrap";
import axiosInstance from "../../../../axios";

const QuizRanking = () => {
  const { id } = useParams();
  const [quizData, setQuizData] = useState(null);
  const [rankingData, setRankingData] = useState(null);
  const [pending, setPending] = useState(true);
  const [startDate, setStartDate] = useState("");

  const loadRankingData = async (date) => {
    try {
      setPending(true);
      const quizResponse = await axiosInstance.get(`/quizzes/${id}`);
      setQuizData(quizResponse.data);

      const rankResponse = await axiosInstance.get(`/quizzes/${id}/top-solutions`, {
        params: { startTime: date ? new Date(date).toISOString() : null },
      });

      setRankingData(rankResponse.data || {});
    } catch (err) {
      console.error("Failed to fetch ranking data", err);
    } finally {
      setPending(false);
    }
  };

  useEffect(() => {
    loadRankingData(startDate);
  }, [id, startDate]);

  if (pending) {
    return (
      <Container className="my-4 d-flex justify-content-center">
        <Spinner animation="border" />
      </Container>
    );
  }

  if (!quizData) return <Container className="my-4">No quiz data available.</Container>;

  const currentPosition = rankingData?.currentPosition;
  const leaderboard = rankingData?.topList || [];

  return (
    <Container className="my-4">
      <Card className="p-3 mb-4">
        <h3>{quizData.title} - Ranking</h3>
        <p>{quizData.description}</p>
        <Form className="mb-3 d-flex align-items-center">
          <Form.Label className="me-2 mb-0">Filter by start date:</Form.Label>
          <Form.Control
            type="date"
            value={startDate}
            onChange={(e) => setStartDate(e.target.value)}
            className="me-2"
            style={{ maxWidth: "200px" }}
          />
          <Button variant="primary" onClick={() => loadRankingData(startDate)}>Refresh</Button>
        </Form>

        {currentPosition !== undefined && (
          <p>
            Your current position: <strong>{currentPosition}</strong>
          </p>
        )}

        <Table striped bordered hover responsive>
          <thead>
            <tr className="text-center">
              <th>Position</th>
              <th>User</th>
              <th>Score</th>
              <th>Time Taken</th>
              <th>Completion Date</th>
            </tr>
          </thead>
          <tbody>
            {leaderboard.length === 0 ? (
              <tr>
                <td colSpan="5" className="text-center">No results available.</td>
              </tr>
            ) : (
              leaderboard.map((entry, index) => (
                <tr key={entry.userID} className="text-center">
                  <td>{index + 1}</td>
                  <td>{entry.userName}</td>
                  <td>{entry.score}</td>
                  <td>{entry.timeTaken}</td>
                  <td>{new Date(entry.completedAt).toLocaleString()}</td>
                </tr>
              ))
            )}
          </tbody>
        </Table>
      </Card>
    </Container>
  );
};

export default QuizRanking;

