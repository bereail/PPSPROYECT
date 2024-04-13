import React from 'react'
import CustomNavbar from '../Navbar/CustomNavbar'
import Footer from '../Footer/footer'
import { Link } from "react-router-dom";
import "./Cart.css"
import image from '../Image/Bolsa.png'
export default function Cart() {
  return (
    <div  style={{paddingBottom: '500px'}}>
        <CustomNavbar></CustomNavbar>
        <div className = "Cart">
          <img src={image} alt="bolsa" className="bolsa-image"/>
          <h2>Start a shopping cart!</h2>
          <Link to="/">
          <button className='Buttom-Cart'>discover products</button>
          </Link>
        </div>
        <Footer></Footer>
    </div>
  )
}
