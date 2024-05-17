import React from "react";
import CustomNavbar from "../Navbar/CustomNavbar";
import CarrouselPage from "../Carrousel/CarrouselPage/CarrouselPage";
import Footer from "../Footer/footer";
import Header from "../Header/header";
import "./Home.css";
import Products from "../Products/Products";
const Home = () => {
  return (
    <div className="Home">

      <CustomNavbar />

      <CarrouselPage />
      
      <div className="content">

        <Header />
        <Products/>
      </div>
      <Footer />
    </div>
  );
};

export default Home;

