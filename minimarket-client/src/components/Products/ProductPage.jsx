import React, { useEffect, useState } from 'react';
import ProductRepository from './ProductRepository';
import Layout from '../Layout/Layout';

const ProductPage = () => {
  const [products, setProducts] = useState([]);
  const [filteredProducts, setFilteredProducts] = useState([]);
  const [selectedCategory, setSelectedCategory] = useState(null);

  const getAllProducts = () => {
    const allProducts = ProductRepository.getAllProducts();
    setProducts(allProducts);
  };

  useEffect(() => {
    getAllProducts();
  }, []);

  useEffect(() => {
    if (selectedCategory) {
      const filtered = products.filter(
        product => product.category === selectedCategory
      );
      setFilteredProducts(filtered);
    } else {
      setFilteredProducts(products); // Display all products if no category is selected
    }
  }, [products, selectedCategory]);

  const productList = (productsToRender) => (
    <div className='product-list'>
      {productsToRender.map(product => (
        <div key={product.id} className='product-item'>
          <h3>{product.name}</h3>
          <p>{product.description}</p>
          <p>Price: ${product.price}</p>
        </div>
      ))}
    </div>
  );

  return (
    <Layout> 
      {productList(filteredProducts)} 
    </Layout>
  );
};

export default ProductPage;