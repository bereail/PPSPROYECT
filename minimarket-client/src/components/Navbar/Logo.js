import React from "react";
import './Logo.css';
import logoImage from '../Image/Logo.png';



const Logo = () => {
  return (
    <div className="Container-logo">
      <img src={logoImage} alt="Logo" className="logo-image"/>
      <div>
      <p>Family</p>
      <p>Market</p>
      </div>
    </div>
      
  );
}
export default Logo;