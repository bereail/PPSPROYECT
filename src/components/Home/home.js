import React from "react";
import { Link } from "react-router-dom";
import { Button } from "react-bootstrap";
import CustomNavbar from "../Navbar/CustomNavbar";
import Footer from "../Footer/footer";
import Header  from "../Header/header";
import Accordion from "../Acordion/acordion";
import AccordionCard from "../Acordion/accordionCard";

const Home = () => {
  return (
    <div>
      <CustomNavbar />
        <AccordionCard />
      <h1>Home Page</h1>
      <Header />
      <Footer />
    </div>
  );
}

export default Home;
