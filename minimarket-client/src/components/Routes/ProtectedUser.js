import React from "react";
import { Navigate } from "react-router";
import { GetRoleByUser } from "../../GetRoleByUser";

const ProtectedUser = ({ children }) => {

  const userType = GetRoleByUser();
  console.log(GetRoleByUser());
  if (userType === null ) {
    return <Navigate to="/" />;
  } else {
    return <>{children}</>;
  }
};

export default ProtectedUser;