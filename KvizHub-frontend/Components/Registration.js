import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { Form, Button, Card, Container, Row, Col, Alert } from "react-bootstrap";
import axiosInstance from "../../../axios";

function Registration() {
  const [user, setUser] = useState("");
  const [mail, setMail] = useState("");
  const [secret, setSecret] = useState("");
  const [avatar, setAvatar] = useState(null);
  const [warn, setWarn] = useState("");
  const [note, setNote] = useState("");
  const redirect = useNavigate();

  const checkUser = (u) => {
    const pattern = /^[^@]*$/;
    return u.length >= 3 && u.length <= 100 && pattern.test(u);
  };

  const checkMail = (m) => {
    const pattern = /\S+@\S+\.\S+/;
    return m.length >= 3 && m.length <= 100 && pattern.test(m);
  };

  const checkSecret = (s) => {
    return s.length >= 8 && s.length <= 100;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!checkUser(user)) {
      setWarn("Username must be 3-100 characters and cannot contain @.");
      return;
    }
    if (!checkMail(mail)) {
      setWarn("Please enter a valid email (3-100 characters).");
      return;
    }
    if (!checkSecret(secret)) {
      setWarn("Password must be 8-100 characters.");
      return;
    }
    if (!avatar) {
      setWarn("Please upload an image.");
      return;
    }

    setWarn("");
    setNote("");

    try {
      const formData = new FormData();
      formData.append("UserName", user);
      formData.append("Email", mail);
      formData.append("Password", secret);
      formData.append("Image", avatar);

      const response = await axiosInstance.post("users/registration", formData, {
        headers: { "Content-Type": "multipart/form-data" },
      });

      if (response.status === 200 || response.status === 201) {
        setNote("Registration successful! You can now login.");
        setUser("");
        setMail("");
        setSecret("");
        setAvatar(null);
        redirect("/login");
      } else {
        setWarn("Registration failed. Try again.");
      }
    } catch (err) {
      console.error(err);
      setWarn(err.response?.data?.message || "Unexpected error.");
    }
  };

  return (
    <Container className="d-flex mt-5">
      <Row className="justify-content-center align-self-center w-100">
        <Col xs={12} sm={8} md={6} lg={4}>
          <Card>
            <Card.Body>
              <Card.Title className="text-center mb-4">Register</Card.Title>
              {warn && <Alert variant="danger">{warn}</Alert>}
              {note && <Alert variant="success">{note}</Alert>}
              <Form onSubmit={handleSubmit}>
                <Form.Group className="mb-3" controlId="formUser">
                  <Form.Label>Username</Form.Label>
                  <Form.Control
                    type="text"
                    placeholder="Enter username"
                    value={user}
                    onChange={(e) => setUser(e.target.value)}
                    required
                  />
                </Form.Group>

                <Form.Group className="mb-3" controlId="formMail">
                  <Form.Label>Email</Form.Label>
                  <Form.Control
                    type="email"
                    placeholder="Enter email"
                    value={mail}
                    onChange={(e) => setMail(e.target.value)}
                    required
                  />
                </Form.Group>

                <Form.Group className="mb-3" controlId="formSecret">
                  <Form.Label>Password</Form.Label>
                  <Form.Control
                    type="password"
                    placeholder="Enter password"
                    value={secret}
                    onChange={(e) => setSecret(e.target.value)}
                    required
                  />
                </Form.Group>

                <Form.Group className="mb-4" controlId="formAvatar">
                  <Form.Label>Profile Image</Form.Label>
                  <Form.Control
                    type="file"
                    accept="image/*"
                    onChange={(e) => setAvatar(e.target.files[0])}
                    required
                  />
                </Form.Group>

                <Button variant="primary" type="submit" className="w-100">
                  Register
                </Button>
              </Form>
            </Card.Body>
          </Card>
        </Col>
      </Row>
    </Container>
  );
}

export default Registration;
