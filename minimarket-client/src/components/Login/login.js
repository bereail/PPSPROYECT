import React from "react";
import { Navbar as Nav } from "react-bootstrap";
import { useState, useEffect } from "react";
import { Link, useLocation } from "react-router-dom";

const Login = () => {
  const [isExpanded, setIsExpanded] = useState(false);
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
    <div>
      {currentPage !== "Login" && currentPage !== "Register" && (
        <div
          style={{ marginRight: currentPage === "cart" && "100px" }}
          className="filter-bar-container"
        >
          <button
            className="icon-category"
            onMouseEnter={() => setIsExpanded(true)}
            onMouseLeave={() => setIsExpanded(false)}
          >
            {" "}
            My Count
          </button>
          {isExpanded && (
            <div
              className="filter-bar expanded"
              onMouseEnter={() => setIsExpanded(true)}
              onMouseLeave={() => setIsExpanded(false)}
              style={{
                backgroundColor: "white",
                border: "1px solid #ccc",
                boxShadow: "0 2px 4px rgba(0,0,0,0.1)",
                padding: "10px",
              }}
            >
              <Nav className="ml-auto">
                <div
                  style={{ marginRight: "5px", width: "100px" }}
                  onClick={() => SetCurrentPage("Register")}
                >
                  {currentPage !== "Register" && (
                    <Link
                      to="/signupUser"
                      type="button"
                      className="btn btn-outline-primary"
                    >
                      {" "}
                      Sign Up
                    </Link>
                  )}
                </div>
                <div
                  style={{ width: "80px" }}
                  onClick={() => SetCurrentPage("Login")}
                >
                  {currentPage !== "Login" && (
                    <Link
                      to="/signin"
                      type="button"
                      className="btn btn-outline-primary"
                    >
                      Sign In
                    </Link>
                  )}
                </div>
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
          {" "}
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
