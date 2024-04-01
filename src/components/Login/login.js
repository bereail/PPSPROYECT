import React from "react";
import { Link } from 'react-router-dom';

const Login = () => {
  return (
    <div style={{ border: "1px solid #ccc", borderRadius: "10px", padding: "4.5px", width: "200px" }}>
      <div style={{ display: "flex", gap: "3px" }}>
        <Link to="/signupUser" type="button" className="btn btn-outline-primary">Registrarse</Link>
        <Link to="/signin" type="button" className="btn btn-outline-primary">Iniciar Sesión</Link>
      </div>
    </div>
  );
};

export default Login;
