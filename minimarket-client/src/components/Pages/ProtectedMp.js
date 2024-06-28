import React from 'react';
import { Navigate, useLocation } from 'react-router-dom';
import queryString from 'query-string';

const ProtectedMp = ({ children }) => {
  const location = useLocation();
  const { preference_id } = queryString.parse(location.search);
  const storedPreferenceId = localStorage.getItem('PreferenceId');


  if (preference_id !== storedPreferenceId) {
    return <Navigate to="/" />;
  }

  return children;
};

export default ProtectedMp;
