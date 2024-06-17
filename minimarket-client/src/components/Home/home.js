import React, { useContext, useEffect, useState } from "react";
import CarrouselPage from "../Carrousel/CarrouselPage/CarrouselPage";
import Footer from "../Footer/footer";
import Header from "../Header/header";
import "./Home.css";
import Products from "../Products/Products";
import { AuthContext } from "../Context/AuthContext";
import Navbar from "../Navbar/Navbar";

const LoginPopup = ({ showPopup }) => (
  <div className={`Time-window ${showPopup ? "show" : ""}`} aria-live="polite">
    <p>Successfully logged in!</p>
  </div>
);

const Home = () => {
  const [showPopup, setShowPopup] = useState(false);
  const { user, isLoading, error } = useContext(AuthContext); // Handle loading and error states
  const [role, setRole] = useState(''); 

  useEffect(() => {
    const isFirstLogin = localStorage.getItem("isFirstLogin");
    if (user && isFirstLogin !== "false") {
      setShowPopup(true);
      localStorage.setItem("isFirstLogin", "false");
      setTimeout(() => {
        setShowPopup(false);
      }, 2000);
    }
  }, [user]);

  if (isLoading) {
    return <p>Loading...</p>; // Display loading state
  }

  if (error) {
    return <p>Error fetching user data: {error.message}</p>; // Display error message
  }

  return (
    <div className="Home">
      <Navbar />
      <div className="hh">
        <CarrouselPage />
        {showPopup && <LoginPopup showPopup={showPopup} />} {/* Render popup conditionally */}
      </div>
      <div className="content">
        <Header />
        <Products />
      </div>
      {role !== 'SuperAdmin' && <Footer />} 

    </div>
  );
};

export default Home;


