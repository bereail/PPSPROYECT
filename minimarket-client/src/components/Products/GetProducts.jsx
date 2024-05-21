import React, { useEffect, useState } from 'react';
import { getAllProducts } from './ProductService';
import './Products.css';

const GetProducts = () => {
  const [Products, setProducts] = useState([]);
  const [quantities, setQuantities] = useState({});

  const productList = async () => {
    const products = await getAllProducts();
    const initialQuantities = {};
    products.forEach(product => {
      initialQuantities[product.id] = 1;
    });
    setProducts(products);
    setQuantities(initialQuantities);
  };

  useEffect(() => {
    productList();
  }, []);

  const handleQuantityChange = (productId, value) => {
    // Actualizar la cantidad solo para el producto específico
    setQuantities(prevQuantities => ({
      ...prevQuantities,
      [productId]: value
    }));
  };

  return (
    <div className="content">
      <h1>Productos</h1>
      <div style={{ display: 'flex', flexWrap: 'wrap' }}>
        {Products.map(product => (
          <div key={product.id}>
            <div className='Container-Products'>
              <h5 style={{ textAlign: 'center' }}>{product.title}</h5>
              
              <p className='Product-Price'>${product.price}</p>
              <p>{product.Description}</p>
              <div className='Container-Button-Products'>
                <button onClick={() => handleQuantityChange(product.id, Math.max(quantities[product.id] - 1, 1))}>-</button>
                <input min="1" value={quantities[product.id]} onChange={(e) => handleQuantityChange(product.id, parseInt(e.target.value) || 1)} />
                <button onClick={() => handleQuantityChange(product.id, quantities[product.id] + 1)}>+</button>
              </div>
              <button className='Add-Product' onClick={() => alert(`Se añadieron ${quantities[product.id]} unidades de ${product.title}`)}>Add</button>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default GetProducts;
