import React from 'react'
//----------------------IMPORTANTE LLAMAR AL HACER ALGUN  CAMBIO EN LOS PRODUCTOS---------------------------------------------

const removeProducstFromCarts = (productId) => {

  const carts = [];
  const cartKeys = Object.keys(localStorage).filter(key => key.startsWith('Cart_') || key.startsWith('Favorite_'));    
  cartKeys.forEach(key => {
    let cart;
    try {
      cart = JSON.parse(localStorage.getItem(key));
    } catch (error) {
      return;
    }
    
    if (key.startsWith('Cart_') && cart && Array.isArray(cart.products)) {
      const updatedProducts = cart.products.filter(item => item.id !== productId);
      cart.products = updatedProducts;
      localStorage.setItem(key, JSON.stringify(cart));
      carts.push(cart);
    } else if (key.startsWith('Favorite_') && cart && Array.isArray(cart.products)) {
      const updatedFavorites = cart.products.filter(item => item.id !== productId);
      cart.products = updatedFavorites;
      localStorage.setItem(key, JSON.stringify(cart));
      carts.push(cart);
    }
  });
};


export default removeProducstFromCarts
