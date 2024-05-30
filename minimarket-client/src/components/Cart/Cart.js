import React, { useContext, useEffect, useState } from 'react';
import CustomNavbar from '../Navbar/CustomNavbar';
import Footer from '../Footer/footer';
import { Link } from "react-router-dom";
import "./Cart.css";
import image from '../Image/Bolsa.png';
import { AuthContext } from '../Context/AuthContext';
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTrashCan } from "@fortawesome/free-solid-svg-icons";

export default function Cart() {
  const {userEmail} = useContext(AuthContext);
  const [cart, setCart] = useState(null);

  useEffect(() => {
    const cartData = JSON.parse(window.localStorage.getItem(`Cart_${userEmail}`));
    setCart(cartData);
  }, [userEmail]);

const HandleDeleteProductcart = (productId) =>{
  if (cart) {
    const updatedCart = {
      ...cart,
      products: cart.products.filter(product => product.id !== productId)
    };
    window.localStorage.setItem(`Cart_${userEmail}`, JSON.stringify(updatedCart));
    setCart(updatedCart);
  }
}
const HandleCleanCart  = () =>{
  window.localStorage.removeItem(`Cart_${userEmail}`);
  setCart();
}
  return (
    <div style={{ paddingBottom: '500px' }}>
      <CustomNavbar />
      {cart.products.length === 0 && (
        <div className="Cart">
          <img src={image} alt="bolsa" className="bolsa-image" />
          <h2>¡Comienza un carrito de compras!</h2>
          <Link to="/">
            <button className='Buttom-Cart'>descubre productos</button>
          </Link>
        </div>
      )}

      {cart&& (
        <div className="Cart-Products">
        <h2>Tu Carrito</h2>
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
              <FontAwesomeIcon icon={faTrashCan} style={{ marginLeft: '90%'}} onClick={()=>{HandleDeleteProductcart(product.id)}}/>
            </div>
          ))}
        </div>
        <button onClick={HandleCleanCart}>Clean Cart</button>
      </div>
      )}
      <Footer />
    </div>
  );
}
