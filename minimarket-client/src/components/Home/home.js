import React from "react";
import CustomNavbar from "../Navbar/CustomNavbar";
import CarrouselPage from "../Carrousel/CarrouselPage/CarrouselPage";
import Footer from "../Footer/footer";
import Header from "../Header/header";
import "./Home.css";
import FilterBar from "../FilterBar/FilterBar";

const Home = () => {
  return (
    <div className="Home">

      <CustomNavbar />

      <CarrouselPage />

      <div className="content">
        <h1>
        
        </h1>
        <Header />
        <FilterBar />
      </div>
      <Footer />
    </div>
  );
};

export default Home;

