import React from "react";
import { Navbar as BootstrapNavbar, Container, Nav } from "react-bootstrap";
import Search from "./Search";
import Logo from "./Logo";
import Login from "../Login/login";

const CustomNavbar = () => {
  return (
    <BootstrapNavbar className="navbar" style={{ backgroundColor: "#76a2c2" }}>
      <Container fluid>
        <Logo />
        <Nav className="flex-grow-1 d-flex justify-content-center"> {/* Utiliza flex-grow-1 para que el Search ocupe todo el espacio disponible */}
          <Search />
        </Nav>
        <Nav className="ml-auto">
          <Login />
        </Nav>
      </Container>
    </BootstrapNavbar>
  );
};

export default CustomNavbar;
