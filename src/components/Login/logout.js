import React from "react";
import { useNavigate } from 'react-router-dom';

const Logout = () => {
  const navigate = useNavigate(); // Declara la variable navigate utilizando useNavigate

  const handleLogout = () => {
    navigate("/sigin");
  };

  return (
    <div className="logout">
      <h1>Logout</h1>
      <button onClick={handleLogout}>Logout</button>
    </div>
  );
};

export default Logout;
