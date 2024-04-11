import React from "react";
import { Link } from "react-router-dom";
import { Button } from "react-bootstrap";
import CustomNavbar from "../Navbar/CustomNavbar";
import Footer from "../Footer/footer";
import Header from "../Header/header";
//import Accordion from "../Acordion/acordion";
//import AccordionCard from "../Acordion/accordionCard";
import "./Home.css";
import FilterBar from "../FilterBar/FilterBar";
import Navigationbar from "../Navbar/NavigationBar/NavigationBar";
import CarrouselPage from "../Carrousel/CarrouselPage/CarrouselPage";

const Home = () => {
  return (
    <div classname="Home">
      <CustomNavbar />
      <CarrouselPage />
      <body>
        <h1>
          At our supermarket, we offer a wide range of fresh foods, essentials,
          personal care items, and more, all under one roof. Our friendly team
          is here to help you find what you need, and we regularly provide
          promotions to help you save on your purchases. We're your convenient
          and reliable shopping destination!
        </h1>

        <Header />
      </body>
      <Footer />
    </div>
  );
};

export default Home;
