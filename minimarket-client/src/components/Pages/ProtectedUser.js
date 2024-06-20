import React, { useContext, useEffect, useState } from "react";
import { Navigate } from "react-router";
import { AuthContext } from "../Context/AuthContext";

const ProtectedUser = ({ children }) => {
  const { role } = useContext(AuthContext);
  const [checkingAuth, setCheckingAuth] = useState(true); 

  useEffect(() => {
    if (role === null) {
      setCheckingAuth(true);
    } else {
      setCheckingAuth(false);
    }
  }, [role]);

  if (checkingAuth) {
    return <p>Cargando...</p>;
  }

  if (!role) {
    return <Navigate to="/" />;
  } else {
    return <>{children}</>;
  }
};

export default ProtectedUser;
