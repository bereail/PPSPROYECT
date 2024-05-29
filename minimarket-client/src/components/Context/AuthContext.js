import React, { createContext, useEffect, useState } from "react";

export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);
  const [userEmail, setUserEmail] = useState(null);

  const login = (token, Email) =>{
    window.localStorage.setItem('LoggedUser', token); 
    window.localStorage.setItem('Email', Email);
    setUserEmail(Email);
    setUser(token);
  }
  const logout = () => {
    window.localStorage.removeItem('LoggedUser');
    localStorage.removeItem("isFirstLogin");
    setUser(null);
  };
  useEffect(() => {
    const token = window.localStorage.getItem('LoggedUser')
    const Email = window.localStorage.getItem('Email')
    const isExpired = isTokenExpired(token);
    if (token) {
      setUser(token)
      setUserEmail(Email);
    }
    if(isExpired){
      logout()
    }
  }, [])
  


  function isTokenExpired(token) {
    if (!token) {
      return true; 
    }
  
    const tokenData = JSON.parse(atob(token.split('.')[1])); 
    const tokenExpirationTime = tokenData.exp * 1000; 
    const currentTime = Date.now(); 
  
    return currentTime > tokenExpirationTime; 
  }
  return (
    <AuthContext.Provider value={{ user, logout,login, userEmail}}>
      {children}
    </AuthContext.Provider>
  );
};
