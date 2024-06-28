import React, { useContext, useEffect, useState } from "react";
import CarrouselPage from "../Carrousel/CarrouselPage/CarrouselPage";
import Footer from "../Footer/footer";
import { ThemeContext } from '../Context/ThemeContext';
import Header from "../Header/header";
import "./Home.css";
import Products from "../Products/Products";
import { AuthContext } from "../Context/AuthContext";
import Navbar from "../Navbar/Navbar";
import { useLocation } from 'react-router-dom';
import TemporaryMessage from "./TemporaryMessage";
import LoginPopup from "./LoginPopup";

const Home = () => {
  const [showPopup, setShowPopup] = useState(false);
  const { user, isLoading, error } = useContext(AuthContext); 
  const { theme } = useContext(ThemeContext);
  const [role, setRole] = useState('');
  const [errorMessage, setErrorMessage] = useState('');
  const location = useLocation();

  useEffect(() => {
    const isFirstLogin = localStorage.getItem("isFirstLogin");
    if (user && isFirstLogin !== "false") {
      setShowPopup(true);
      localStorage.setItem("isFirstLogin", "false");
      setTimeout(() => {
        setShowPopup(false);
      }, 2000);
    }
    
    const searchParams = new URLSearchParams(location.search);
    const message = searchParams.get('message');
    if (message) {
      setErrorMessage(message);
      setTimeout(() => {
        setErrorMessage('');
      }, 5000); // Tiempo en milisegundos para mostrar el mensaje
    }
  }, [user, location.search]);

  useEffect(() => {
    document.body.className = theme; 
  }, [theme]);

  if (isLoading) {
    return <p>Loading...</p>; 
  }

  if (error) {
    return <p>Error fetching user data: {error.message}</p>; 
  }

  return (
    <div className="Home">
      <Navbar />
    
      <div className="hh">
        <CarrouselPage />
        {showPopup && <LoginPopup showPopup={showPopup} />} 
      </div>
      
      <div className="hh">
        {errorMessage && (
          <TemporaryMessage message={errorMessage} />
        )}
      </div>

      <div className="content">
        <Header />
        <Products />
      </div>
      
      <Footer /> 

    </div>
  );
};

export default Home;
