import React, { useContext, useEffect, useState } from "react";
import { Navigate } from "react-router";
import { AuthContext } from "../Context/AuthContext";

const ProtectedUser = ({ children }) => {
  const {role} = useContext(AuthContext)
  if (!role) {
    return <Navigate to="/" />;
  } else {
    return <>{children}</>;
  }
};

export default ProtectedUser;
