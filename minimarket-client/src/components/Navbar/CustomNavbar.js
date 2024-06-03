import React, { useContext, useState, useEffect } from "react";
import { Navbar as BootstrapNavbar, Container, Nav } from "react-bootstrap";
import { Link, useLocation } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCartShopping, faSun, faMoon } from "@fortawesome/free-solid-svg-icons";
import Search from "../SearhBar/Search";
import Logo from "./Logo";
import Login from "../Login/login";
import FilterBar from "../FilterBar/FilterBar";
import { ThemeContext } from "../Context/ThemeContext";
import './CustomNavbar.css'
import { GetRoleByUser } from "../../GetRoleByUser";


const CustomNavbar = () => {
  const { pathname } = useLocation();
  const [currentPage, SetCurrentPage] = useState("Home");
  const { theme, toggleTheme } = useContext(ThemeContext);
  const [RoleUser, SetRolUser] = useState('');

  useEffect(() => {
    const role = GetRoleByUser()
    SetRolUser(role);
  }, [])
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
      case "/user":
        SetCurrentPage("User");
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
      }}
    >
      <Container fluid style={{ maxHeight: "50px", padding: "0px" }}>
        <div style={{ display: "flex", alignItems: "center" }}>
          <Link to="/" style={{ textDecoration: 'none' }} onClick={() => SetCurrentPage("Home")}>
            <Logo />
          </Link>
          <button
            className="toggle-theme-button"
            onClick={toggleTheme}
            style={{
              background: 'none',
              border: 'none',
              color: theme === "light" ? "black" : "white",
            }}
          >
            <FontAwesomeIcon icon={theme === "light" ? faMoon : faSun} />
          </button>
        </div>

        <Nav className="flex-grow- d-flex justify-content-center">
          {/* Utiliza flex-grow-1 para que el Search ocupe todo el espacio disponible */}
          <div style={{ display: "flex", alignItems: "center" }}>
            {currentPage === "Home" && <FilterBar></FilterBar>}
          </div>
          <div>{currentPage === "Home" && <Search />}</div>
        </Nav>

        <Login></Login>
        {(currentPage === "Home" || currentPage === "User" || RoleUser === 'Customer' || RoleUser)  && (
          <Link
            to="/cart"
            style={{
              color: "black",
              fontSize: "25px",
              textDecoration: "none",
              backgroundColor: "#a5351ca4",
              padding: "14px",
              borderBottomRightRadius: "15%",
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
