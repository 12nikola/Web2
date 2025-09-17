import { Button, Navbar, Nav, Container, NavbarBrand } from "react-bootstrap";
import { Link } from "react-router-dom";

function TopBar({ items }) {
  return (
    <Navbar bg="dark" variant="dark" expand="lg" sticky="top">
      <Container fluid className="px-2">
        <NavbarBrand>Quiz Context</NavbarBrand>
        <Navbar.Toggle aria-controls="menu-nav" />
        <Navbar.Collapse id="menu-nav">
          <Nav className="ms-auto d-flex flex-column flex-lg-row">
            {items.map((entry, idx) => (
              <Button
                key={idx}
                variant="outline-light"
                className="me-lg-2 mb-2 mt-2 mb-lg-0"
                as={Link}
                to={entry.href}
                onClick={entry.onClick}
              >
                {entry.label}
              </Button>
            ))}
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
}

export default TopBar;
