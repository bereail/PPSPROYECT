import React, { useContext, useEffect, useState } from 'react';
import Footer from '../Footer/footer';
import { Link, Navigate, useAsyncError } from "react-router-dom";
import "./Cart.css";
import image from '../Image/Bolsa.png';
import { AuthContext } from '../Context/AuthContext';
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTrashCan } from "@fortawesome/free-solid-svg-icons";
import { faXmark } from "@fortawesome/free-solid-svg-icons";
import Navbar from '../Navbar/Navbar';
import api from '../../api';
import CreateOrder from '../../Orders/CreateOrder';


export default function Cart() {
  const { userEmail } = useContext(AuthContext);
  const [cart, setCart] = useState(null);
  const [CartPriceDiscount, SetCartPriceDiscount] = useState();
  const [CartDiscount, SetCartDiscount] = useState();
  useEffect(() => {
    const cartData = JSON.parse(window.localStorage.getItem(`Cart_${userEmail}`));
    setCart(cartData);
  }, [userEmail]);

  useEffect(() => {
    if (cart) {
      const totaldiscount = cart.products.reduce((acc, product) => {
        // Sumamos el precio de cada producto considerando el precio con descuento y la cantidad
        return acc + product.discount * product.quantity;
      }, 0);

      const discount = cart.products.reduce((acc, product) => {
        // Sumamos el precio de cada producto considerando el pr\ecio con descuento y la cantidad
        return acc + (product.price - product.discount) * product.quantity;
      }, 0);
      SetCartDiscount(discount)
      SetCartPriceDiscount(totaldiscount); // Actualizamos el estado con el precio total calculado
    }
  }, [cart]);

  const HandleDeleteProductcart = (productId) => {
    if (cart) {
      const updatedCart = {
        ...cart,
        products: cart.products.filter(product => product.id !== productId)
      };
      window.localStorage.setItem(`Cart_${userEmail}`, JSON.stringify(updatedCart));
      setCart(updatedCart);
    }
  }
  const HandleCleanCart = () => {
    window.localStorage.removeItem(`Cart_${userEmail}`);
    setCart(null);
  }
  
  const HandleCreateOrder = () => {

    const newDetails = cart.products.map(item => ({

      productId: item.id,
      productQuantity: item.quantity
    }))
    const orderDetails = { newDetails };

    CreateOrder(orderDetails)
  }
  return (
    <div style={{ paddingBottom: '500px' }}>
      <Navbar />
      {(!cart || cart.products.length === 0) && (
        <div className="Cart">
          <img src={image} alt="bolsa" className="bolsa-image" />
          <h2>Start a shopping cart!</h2>
          <Link to="/">
            <button className='Buttom-Cart'>discover products</button>
          </Link>
        </div>
      )}

      {cart && (
        <div>
          <h2>Tu Carrito</h2>
          <div className="Cart-Products">
            <div className="product-list">

              {cart.products.map((product, index) => (
                <div key={index} className="product-item">
                  <div>
                    <h3>{product.name}</h3>
                    <p>Description: {product.description}</p>
                    <p>Price: {product.price}</p>
                    <p>With discount: {product.discount}</p>
                    <p>quantity: {product.quantity}</p>
                  </div>
                  <FontAwesomeIcon icon={faTrashCan} style={{ marginLeft: '90%' }} onClick={() => { HandleDeleteProductcart(product.id) }} />
                </div>
              ))}
            </div>
            <div className="summary-section">
              <h3>Shopping Summary</h3>
              <div className="Total-Price" style={{ marginTop: '40px' }}>
                <p>Discount:</p>
                <p>${CartDiscount}</p>
              </div>
              <div className="Total-Price">
                <p>Total Price:</p>
                <p> ${CartPriceDiscount}</p>
              </div>
              <button className="Button-Cart" onClick={HandleCreateOrder}>Continue shopping</button>
            </div>
          </div>
          <button className='Button-Products' onClick={HandleCleanCart}>Clean Cart</button>
        </div>
      )}
      <Footer />
    </div>
  );
}
