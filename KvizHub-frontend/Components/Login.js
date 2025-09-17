import { useState } from "react";
import { Form, Button, Card, Container, Row, Col, Alert } from "react-bootstrap";
import axiosInstance from "../../../axios";

function Login({ handleLogin }) {
  const [identifier, setIdentifier] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");

  const validateEmail = (email) => {
    return /\S+@\S+\.\S+/.test(email);
  };

  const submitHandler = async (e) => {
    e.preventDefault();

    if (identifier.includes("@")) {
      if (!validateEmail(identifier)) {
        setError("Please enter a valid email address.");
        return;
      }
    } else {
      if (identifier.length < 3) {
        setError("Username must be at least 3 characters long.");
        return;
      }
    }

    if (password.length < 8) {
      setError("Password must be at least 8 characters long.");
      return;
    }

    setError(""); // Clear previous errors

    try {
      const response = await axiosInstance.post("users/login", {
        Identifier: identifier,
        Password: password,
      });

      const { token } = response.data;

      if (token) {
        handleLogin(token);
        setIdentifier("");
        setPassword("");
      } else {
        setError("Internal Server Error");
      }
    } catch (err) {
      console.error(err);
      setError("Login failed: invalid username/email or password");
    }
  };

  return (
    <Container className="d-flex  mt-5">
      <Row className="justify-content-center align-self-center w-100">
        <Col xs={12} sm={8} md={6} lg={4}>
          <Card>
            <Card.Body>
              <Card.Title className="text-center mb-4">Login</Card.Title>
              {error && <Alert variant="danger">{error}</Alert>}
              <Form onSubmit={submitHandler}>
                <Form.Group className="mb-3" controlId="formIdentifier">
                  <Form.Label>Username or Email</Form.Label>
                  <Form.Control
                    type="text"
                    placeholder="Enter username or email"
                    value={identifier}
                    onChange={(e) => setIdentifier(e.target.value)}
                    required
                  />
                </Form.Group>

                <Form.Group className="mb-4" controlId="formPassword">
                  <Form.Label>Password</Form.Label>
                  <Form.Control
                    type="password"
                    placeholder="Password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    required
                  />
                </Form.Group>

                <Button variant="primary" type="submit" className="w-100">
                  Login
                </Button>
              </Form>
            </Card.Body>
          </Card>
        </Col>
      </Row>
    </Container>
  );
}

export default Login;