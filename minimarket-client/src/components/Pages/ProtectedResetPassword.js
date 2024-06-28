import React, { useContext, useEffect, useState } from 'react';
import { Navigate, useLocation } from 'react-router-dom';
import { AuthContext } from '../Context/AuthContext';
import queryString from 'query-string';

const ProtectedResetPassword = ({ children }) => {
  const { role } = useContext(AuthContext);
  const location = useLocation();
  const { token } = queryString.parse(location.search);
  const [checkingAuth, setCheckingAuth] = useState(true);

  useEffect(() => {
    if (role === null) {
      setCheckingAuth(true);
    } else {
      setCheckingAuth(false);
    }
  }, [role]);

  const hasToken = token !== undefined && token !== null;

  if (checkingAuth) {
    return <p>Cargando...</p>;
  }

  if (!role && !hasToken) {
    return <Navigate to="/" />;
  }

  return <>{children}</>;
};

export default ProtectedResetPassword;
