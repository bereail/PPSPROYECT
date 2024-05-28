import React, { useContext } from "react";
import './Login.css'
import { Button, Navbar as Nav } from "react-bootstrap";
import { useState, useEffect } from "react";
import { Link, Navigate, useLocation } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faUser } from "@fortawesome/free-solid-svg-icons"
import { AuthContext } from "../Context/AuthContext";

const Login = () => {
  const [isExpanded, setIsExpanded] = useState(false);
  const { pathname } = useLocation();
  const [currentPage, SetCurrentPage] = useState("Home");
  const {user, logout} = useContext(AuthContext);


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
      case "/User":
        SetCurrentPage("User");
        break;
      default:
        SetCurrentPage("");
    }
  }, [pathname]);

  const handleLogout = () => {
    logout();
  };


  return (
    <div >
      {currentPage !== "Login" && currentPage !== "Register" && currentPage !== "User" && (
        <div
          style={{ marginRight: currentPage === "cart" && "200px" }}
          className="Count-container"
        >
          <button
            className="Count"
            onMouseEnter={() => setIsExpanded(true)}
            onMouseLeave={() => setIsExpanded(false)}
          >
          <div style={{marginRight: "-40px"}}>
            <p style={{ margin: '0px' }}>My Account</p>
            <FontAwesomeIcon icon={faUser} />
          </div>
          </button>
          {isExpanded && !user && (
            <div
              className="Count-expanded"
              onMouseEnter={() => setIsExpanded(true)}
              onMouseLeave={() => setIsExpanded(false)}
              style={{
                backgroundColor: "white",
                border: "1px solid #ccc",
                boxShadow: "0 2px 4px rgba(0,0,0,0.1)",
                padding: "10px",
              }}>

              <Nav className="ml-auto">
                <div
                  style={{ marginRight: "5px", width: "100px" }}
                  onClick={() => SetCurrentPage("Register")}>

                  <Link
                    to="/signupUser"
                    type="button"
                    className="btn btn-outline-primary">
                    Sign Up
                  </Link>

                </div>
                <div
                  style={{ width: "80px" }}
                  onClick={() => SetCurrentPage("Login")}>
                  <Link
                    to="/signin"
                    type="button"
                    className="btn btn-outline-primary">
                    Sign In
                  </Link>

                </div>
              </Nav>
            </div>
          )}

          {isExpanded && user && (
            <div
              className="Count-expanded"
              onMouseEnter={() => setIsExpanded(true)}
              onMouseLeave={() => setIsExpanded(false)}
              style={{
                backgroundColor: "white",
                border: "1px solid #ccc",
                boxShadow: "0 2px 4px rgba(0,0,0,0.1)",
                padding: "10px",
              }}>

              <Nav className="ml-auto">
                <div
                  style={{ marginRight: "5px", width: "120px" }}
                  onClick={() => SetCurrentPage("User")}>

                  <Link
                    to="/User"
                    type="button"
                    className="btn btn-outline-primary">
                    My Account
                  </Link>

                </div>
                <button
                  style={{ width: "80px" }}
                  onClick={handleLogout}
                  className="btn btn-outline-primary">
                  Exit
                </button>
              </Nav>
            </div>
          )}
        </div>
      )}

      {currentPage == "Login" && (
        <Link
          to="/signupUser"
          type="button"
          className="btn btn-outline-primary"
        >
          Sign Up
        </Link>
      )}
      {currentPage == "Register" && (
        <Link to="/signin" type="button" className="btn btn-outline-primary">
          Sign In
        </Link>
      )}
    </div>
  );
};

export default Login;
