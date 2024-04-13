import React from "react";
import CustomNavbar from "../Navbar/CustomNavbar";
import CarrouselPage from "../Carrousel/CarrouselPage/CarrouselPage";
import Footer from "../Footer/footer";
import Header from "../Header/header";
import "./Home.css";
const Home = () => {
  return (
    <div className="Home">

      <CustomNavbar />

      <CarrouselPage />

      <div className="content">

        <Header />
      </div>
      <Footer />
    </div>
  );
};

export default Home;

