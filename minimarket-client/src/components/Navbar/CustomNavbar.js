import React from "react";
import { useState, useEffect } from "react";
import { Navbar as BootstrapNavbar, Container, Nav } from "react-bootstrap";
import Search from "../../SearhBar/Search";
import Logo from "./Logo";
import Login from "../Login/login";
import { Link, useLocation } from "react-router-dom";

import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCartShopping } from "@fortawesome/free-solid-svg-icons";
import FilterBar from "../FilterBar/FilterBar";

const CustomNavbar = () => {
  const { pathname } = useLocation();
  const [currentPage, SetCurrentPage] = useState("Home");

  useEffect(() => {
    switch (pathname) {
      case "/":
        SetCurrentPage("Home");
        break;
      case "/signupUser":
        SetCurrentPage("Register");
        break;
      case "/signin":
        SetCurrentPage("Login");
        break;
      case "/cart":
        SetCurrentPage("cart");
        break;
      default:
        SetCurrentPage("");
    }
  }, [pathname]);

  return (
    <BootstrapNavbar
      className="navbar"
      style={{
        backgroundImage: "linear-gradient(to right, #a5351ca4, #a5351ca4)",
        borderBottomLeftRadius: "15%",
        borderBottomRightRadius: "15%",
      }}
    >
      <Container fluid style={{ maxHeight: "50px", padding: "0px" }}>
        <Link to="/" onclick={() => SetCurrentPage("Home")}>
          {" "}
          <Logo />{" "}
        </Link>
        <Link
          to="/"
          onclick={() => SetCurrentPage("Home")}
          type="button"
          className="btn btn-outline-primary"
        >
          {" "}
          Home{" "}
        </Link>
        <Nav className="flex-grow-1 d-flex justify-content-center">
          {" "}
          {/* Utiliza flex-grow-1 para que el Search ocupe todo el espacio disponible */}
          <div style={{ display: "flex", alignItems: "center" }}>
            {currentPage === "Home" && <FilterBar></FilterBar>}
          </div>
          <div>{currentPage === "Home" && <Search />}</div>
        </Nav>

        <Login></Login>
        {currentPage === "Home" && (
          <Link
            to="/cart"
            style={{
              color: "black",
              fontSize: "25px",
              textDecoration: "none",
              backgroundColor: "#a5351ca4",
              padding: "14px",
              borderBottomRightRadius: "15%",
              marginLeft: "30px",
            }}
          >
            My cart
            <FontAwesomeIcon
              icon={faCartShopping}
              style={{ fontSize: "30px", marginLeft: "5px" }}
            />
          </Link>
        )}
      </Container>
    </BootstrapNavbar>
  );
};

export default CustomNavbar;
