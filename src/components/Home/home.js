import React from "react";
import { Link } from "react-router-dom";
import { Button } from "react-bootstrap";
import CustomNavbar from "../Navbar/CustomNavbar";
import Footer from "../Footer/footer";
import Header  from "../Header/header";
import Accordion from "../Acordion/acordion";
import AccordionCard from "../Acordion/accordionCard";
import  "./Home.css";
import FilterBar from "../FilterBar/FilterBar";

const Home = () => {
  return (
    <div classname = 'Home'>
      <CustomNavbar />
      
      <body>
      <AccordionCard />
      <h1>Home Page</h1>
    
      <Header />
      </body>
      <Footer />
    </div>
  );
}

export default Home;
