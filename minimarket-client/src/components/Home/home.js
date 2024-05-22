import React from "react";
import Header from "../Header/header";
import Layout from "../Layout/Layout";
import CarrouselPage from "../Carrousel/CarrouselPage/CarrouselPage";
import ProductsOffers from "../Products/ProductsOffers";
import "./Home.css"; 

const Home = () => {
  const headerContent = <Header />; 
  
  return (
    <Layout headerContent={headerContent} >
      <CarrouselPage />
      <div className="content">
        <ProductsOffers />
      </div>
    </Layout>
  );
};

export default Home;