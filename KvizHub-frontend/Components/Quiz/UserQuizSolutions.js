import { useEffect, useState } from "react";
import { Container, Card, Table, Spinner, Accordion } from "react-bootstrap";
import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, ResponsiveContainer } from "recharts";
import { useNavigate } from "react-router-dom";
import axiosInstance from "../../../axios";

const UserQuizSolutions = ({ username }) => {
  const [quizSolutions, setQuizSolutions] = useState([]);
  const [pending, setPending] = useState(true);
  const navigate = useNavigate();

  useEffect(() => {
    const loadUserSolutions = async () => {
      try {
        setPending(true);
        const res = await axiosInstance.get(`/users/${username}/solutions`);
        setQuizSolutions(res.data || []);
      } catch (err) {
        console.error("Failed to fetch user quiz solutions", err);
      } finally {
        setPending(false);
      }
    };

    loadUserSolutions();
  }, [username]);

  if (pending) {
    return (
      <Container className="my-4 d-flex justify-content-center">
        <Spinner animation="border" />
      </Container>
    );
  }

  if (!quizSolutions.length) {
    return (
      <Container className="text-center my-4">
        <p><b>No quiz solutions found for this user.</b></p>
      </Container>
    );
  }

  return (
    <Container className="my-4">
      <h2 className="mb-4 text-center">{username}'s Quiz Solutions</h2>

      <Accordion>
        {quizSolutions.map((sol, idx) => (
          <Card key={idx} className="mb-3">
            <Accordion.Item eventKey={idx.toString()}>
              <Accordion.Header>
                <h5 className="mb-0">{sol.information.quizTitle}</h5>
              </Accordion.Header>

              <Accordion.Body>
                <p className="mb-3">Description: {sol.information.quizTitle}</p>

                {sol.lastPercentages && (
                  <div style={{ width: "100%", height: 200 }} className="mb-3">
                    <ResponsiveContainer>
                      <LineChart
                        data={sol.lastPercentages.slice(-5).reverse().map((p, i) => ({
                          attempt: 5 - i,
                          percentage: p,
                        }))}
                        margin={{ top: 5, right: 20, left: 0, bottom: 5 }}
                      >
                        <CartesianGrid strokeDasharray="3 3" />
                        <XAxis dataKey="attempt" label={{ value: "Last Attempts", position: "insideBottom", offset: -5 }} />
                        <YAxis domain={[0, 100]} label={{ value: "Score %", angle: -90, position: "insideLeft" }} />
                        <Tooltip />
                        <Line type="monotone" dataKey="percentage" stroke="#007bff" strokeWidth={2} dot={{ r: 4 }} />
                      </LineChart>
                    </ResponsiveContainer>
                  </div>
                )}

                {sol.solutionAttempts.length > 0 ? (
                  <Table striped bordered hover responsive>
                    <thead>
                      <tr>
                        <th className="text-center">Score Points</th>
                        <th className="text-center">Score Percentage</th>
                        <th className="text-center">Attempt Date</th>
                      </tr>
                    </thead>
                    <tbody>
                      {sol.solutionAttempts
                        .slice()
                        .sort((a, b) => new Date(b.attemptDate) - new Date(a.attemptDate))
                        .map((attempt) => (
                          <tr
                            key={attempt.quizSolutionAttemptID}
                            style={{ cursor: "pointer" }}
                            onClick={() => navigate(`/results/${attempt.quizSolutionAttemptID}`)}
                          >
                            <td className="text-center">{attempt.scorePoints}</td>
                            <td className="text-center">{attempt.scorePercentage}%</td>
                            <td className="text-center">{new Date(attempt.attemptDate).toLocaleString()}</td>
                          </tr>
                        ))}
                    </tbody>
                  </Table>
                ) : (
                  <p className="text-muted">No attempts for this quiz yet.</p>
                )}
              </Accordion.Body>
            </Accordion.Item>
          </Card>
        ))}
      </Accordion>
    </Container>
  );
};

export default UserQuizSolutions;
