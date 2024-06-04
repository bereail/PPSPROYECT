import React, { useContext, useEffect, useState } from "react";
import CustomNavbar from "../Navbar/CustomNavbar";
import CarrouselPage from "../Carrousel/CarrouselPage/CarrouselPage";
import Footer from "../Footer/footer";
import Header from "../Header/header";
import "./Home.css";
import Products from "../Products/Products";
import { AuthContext } from "../Context/AuthContext";
import Navbar from "../Navbar/Navbar";
const Home = () => {
  const [showPopup, setShowPopup] = useState(false);
  const {user} = useContext(AuthContext);

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

  return (
    <div className="Home">

      <Navbar/>
      <div className="hh">
      <CarrouselPage />

      <div className={`Time-window ${showPopup ? 'show' : ''}`}>
          <p>successfully logged in!</p>
      </div>
   
      <div className="content">

        <Header />
        <Products/>
      </div>
      </div>
      <Footer />
    </div>
  );
};

export default Home;

