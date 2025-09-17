import { useEffect, useState } from "react";
import { Container, Card, Table, Spinner } from "react-bootstrap";
import axiosInstance from "../../../../axios";

const GlobalRanking = () => {
  const [rankingData, setRankingData] = useState(null);
  const [pending, setPending] = useState(true);

  const loadGlobalRanking = async () => {
    try {
      setPending(true);
      const response = await axiosInstance.get("/quizzes/ranglist");
      setRankingData(response.data || {});
    } catch (err) {
      console.error("Failed to fetch global ranking", err);
    } finally {
      setPending(false);
    }
  };

  useEffect(() => {
    loadGlobalRanking();
  }, []);

  if (pending) {
    return (
      <Container className="my-4 d-flex justify-content-center">
        <Spinner animation="border" />
      </Container>
    );
  }

  const currentPosition = rankingData?.currentPosition;
  const leaderboard = rankingData?.topList || [];

  return (
    <Container className="my-4">
      <Card className="p-3">
        <h3 className="mb-3">Global Ranking</h3>

        {currentPosition !== undefined && (
          <p>
            Your current global position: <strong>{currentPosition}</strong>
          </p>
        )}

        <Table striped bordered hover responsive>
          <thead>
            <tr className="text-center">
              <th>Position</th>
              <th>User</th>
              <th>Total Score</th>
              <th>Completed Quizzes</th>
              <th>Last Activity</th>
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
                  <td>{entry.totalScore}</td>
                  <td>{entry.completedQuizzes}</td>
                  <td>{new Date(entry.lastActivity).toLocaleString()}</td>
                </tr>
              ))
            )}
          </tbody>
        </Table>
      </Card>
    </Container>
  );
};

export default GlobalRanking;
