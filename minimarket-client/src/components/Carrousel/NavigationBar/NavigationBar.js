import React from "react";
import Container from "react-bootstrap/Container";
import Nav from "react-bootstrap/Nav";
import Navbar from "react-bootstrap/Navbar";
import { FaGithub, FaTwitter, FaLinkedin } from "react-icons/fa"; // Importar iconos de react-icons

function Navigationbar() {
  return (
    <div>
      <Navbar bg="dark" variant="dark" expand="lg" fixed="top">
        <Container>
          <Navbar.Brand href="/" style={{ fontFamily: "Oxygen" }}>
            Carousel
          </Navbar.Brand>
          <Navbar.Toggle aria-controls="basic-navbar-nav" />
          <Navbar.Collapse id="basic-navbar-nav">
            <Nav
              className="icons"
              style={{ gap: "0.5rem", alignItems: "center" }}
            >
              <FaGithub size={24} color="white" />
              <FaTwitter size={24} color="white" />
              <FaLinkedin size={24} color="white" />
            </Nav>
          </Navbar.Collapse>
        </Container>
      </Navbar>
    </div>
  );
}

export default Navigationbar;
