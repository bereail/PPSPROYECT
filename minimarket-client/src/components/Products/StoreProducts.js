import React, { useContext } from 'react'
import { AuthContext } from '../Context/AuthContext';

const StoreProducts = (key, product, quantities, userEmail) => {

    
    let cart;
    //if (userEmail !== null) {
      try {
        cart = JSON.parse(window.localStorage.getItem(`${key}_${userEmail}`)) || {};
      } catch (e) {
        cart = {};
      }
      if (!Array.isArray(cart.products)) {
        cart.products = [];
      }
      let productIndex = cart.products.findIndex(item => item.id === product.id);
      //Actualiza el prodcuto en el carrito
      if (productIndex !== -1) {
          cart.products[productIndex].quantity += quantities[product.id];
          cart.products[productIndex].price += product.price * quantities[product.id];
          cart.products[productIndex].discount += product.price * (1 - product.discount / 100).toFixed(2) * quantities[product.id];
      } else {
        //Crea agrega el prodcuto al carrito
          cart.products.push({
              id: product.id,
              name: product.name,
              description: product.description,
              price: product.price * quantities[product.id],
              discount: product.price * (1 - product.discount / 100).toFixed(2) * quantities[product.id],
              quantity: quantities[product.id]
          });
      }
        
        window.localStorage.setItem(`${key}_${userEmail}`, JSON.stringify(cart));
        return true;

      //} else {
     // alert('Usuario no logeado');
      //return false;
    //}
    
}

export default StoreProducts