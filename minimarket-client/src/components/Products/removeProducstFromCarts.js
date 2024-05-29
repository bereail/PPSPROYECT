import React from 'react'
//----------------------IMPORTANTE LLAMAR AL HACER ALGUN  CAMBIO EN LOS PRODUCTOS---------------------------------------------

const removeProducstFromCarts = (productId) => {

    const carts = [];
    const cartKeys = Object.keys(localStorage).filter(key => key.startsWith('Cart_'));
    
    cartKeys.forEach(key => {
      let cart;
      try {
        cart = JSON.parse(localStorage.getItem(key));
      } catch (error) {
        console.error(`Error parsing cart ${key}:`, error);
        return;
      }
      
      if (cart && Array.isArray(cart.products)) {
        const updatedProducts = cart.products.filter(item => item.id !== productId);
        cart.products = updatedProducts;
        localStorage.setItem(key, JSON.stringify(cart));
        carts.push(cart);
      }
    });
  
  };

export default removeProducstFromCarts
