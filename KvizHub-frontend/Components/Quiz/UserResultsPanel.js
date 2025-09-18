import { useState, useEffect } from "react";
import { Container, Form, Spinner } from "react-bootstrap";
import axiosInstance from "../../../../axios";
import UserQuizSolutions from "../../Components/UserQuizSolution";

function UserResultsPanel() {
  const [userList, setUserList] = useState([]);
  const [activeUser, setActiveUser] = useState("");
  const [pending, setPending] = useState(true);

  useEffect(() => {
    const loadUsers = async () => {
      try {
        const res = await axiosInstance.get("users");
        setUserList(res.data || []);
      } catch (err) {
        console.error("Error fetching users", err);
      } finally {
        setPending(false);
      }
    };

    loadUsers();
  }, []);

  return (
    <Container className="my-4">
      {pending ? (
        <div className="d-flex justify-content-center my-5">
          <Spinner animation="border" role="status">
            <span className="visually-hidden">Loading...</span>
          </Spinner>
        </div>
      ) : (
        <>
          <Form.Group className="mb-3" controlId="userSelectDropdown">
            <Form.Label>Select a User</Form.Label>
            <Form.Select
              value={activeUser}
              onChange={(e) => setActiveUser(e.target.value)}
            >
              <option value="">Choose a user</option>
              {userList.map((u) => (
                <option key={u} value={u}>
                  {u}
                </option>
              ))}
            </Form.Select>
          </Form.Group>

          {activeUser && <UserQuizSolutions username={activeUser} />}
        </>
      )}
    </Container>
  );
}

export default UserResultsPanel;
