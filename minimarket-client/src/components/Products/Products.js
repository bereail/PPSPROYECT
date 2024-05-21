import React, { useEffect, useState } from 'react';
import './Products.css';
import GetProductsOffers from './GetProductsOffers';

const Products = () => {
  const [Products, setProducts] = useState([]);
  const [quantities, setQuantities] = useState({});

  useEffect(() => {
    const fetchProducts = async () => {
      try {
        const data = await GetProductsOffers();
        setProducts(data);
      } catch (error) {
        console.error('Error fetching products:', error);
      }
    };

    fetchProducts();
  }, []);
  


  useEffect(() => {
    // Inicializar las cantidades solo cuando Products cambie
    const initialQuantities = {};
    Products.forEach(product => {
      initialQuantities[product.id] = 1;
    });
    setQuantities(initialQuantities);
  }, [Products]);

  // Actualiza la cantidad solo para un Producto
  const handleQuantityChange = (productId, value) => {
    setQuantities(prevQuantities => ({
      ...prevQuantities,
      [productId]: value
    }));
  };

  //IMPLEMENTAR LOGICA DE CARRITO
  const AddCartHandler = (product) => {
    alert(`Se añadieron ${quantities[product.id]} unidades de ${product.name}`);

  }


  return (
    <div>
      <p style={{fontSize: '50px'}}>Products</p>
      <div style={{ display: 'flex', flexWrap: 'wrap' }}>
        {Products.map(product => (
          <div key={product.id}>
            <div className='Container-Products'>
              <h5 style={{ textAlign: 'center' }}>{product.name}</h5>

              <p className='Product-Price'>${product.price}</p>
              <p>{product.description}</p>
              <div className='Container-Button-Products'>
                <button onClick={() => handleQuantityChange(product.id, Math.max(quantities[product.id] - 1, 1))}>-</button>
                <input min="1" value={quantities[product.id] || 1} onChange={(e) => handleQuantityChange(product.id, parseInt(e.target.value) || 1)} />
                <button onClick={() => handleQuantityChange(product.id, quantities[product.id] + 1)}>+</button>
              </div>
              <button className='Add-Product' onClick={() => (AddCartHandler(product))}>Add</button>
            </div>
          </div>
        ))}
      </div>
    </div>
  )
}

export default Products
