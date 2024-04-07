import React from "react";
import { Link } from "react-router-dom";
import { Button } from "react-bootstrap";
import CustomNavbar from "../Navbar/CustomNavbar";
import Footer from "../Footer/footer";
import Header  from "../Header/header";

import AccordionCard from "../Acordion/accordionCard";
import  "./Home.css";
import FilterBar from "../FilterBar/FilterBar";
import { useContext } from "react";


const Home = (props) => {

  return (
    <div classname ={props.Home}>
      <img alt="" src={props.homeImg} className={props.imgClass} />
     
      <div
       
      >
        <h1>{props.title}</h1>
        <p>{props.text}</p>
      </div>
     
      <CustomNavbar />
      
      <body>
      
      
  
     <Header />
      </body>
      <Footer />
    </div>



  );
}

export default Home;
