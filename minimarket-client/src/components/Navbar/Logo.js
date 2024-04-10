import React from "react";
import './Logo.css';
import logoImage from '../Image/Logo.jpg';


const Logo = () => {
  return (
      <div className="container-fluid">
         <img src={logoImage} alt="Logo" className="logo-image" />
        

      </div>
  );
}
export default Logo;