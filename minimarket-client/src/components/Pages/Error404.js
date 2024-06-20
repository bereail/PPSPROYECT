import React from "react";
import { Link } from "react-router-dom";
import "./Error404.css"; 

const Error404 = () => {
  return (
    <div className="error-container">
      <h1>Error, page not found. Please go back to the homepage.</h1>
      <Link to="/"><button>
        Go back to the homepage
      </button>
      </Link>
    </div>
  );
};

export default Error404;
