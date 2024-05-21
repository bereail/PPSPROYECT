import React, { useEffect, useState } from 'react';
import ProductRepository from './ProductRepository';
import GetProducts from './GetProducts';

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
            const filtered = products.filter (
                (product) => product.category == selectedCategory
            );
            setFilteredProducts(filtered);
        }
    }, [products, selectedCategory]);

 return (
    <div>
        <div className='product-list'>
             {products.map(product => (
                <div key={product.id} className='product-item'>
                    <h3>{product.name}</h3>
                    <p>{product.description}</p>
                    <p>Price: ${product.price}</p>
                </div>
             ))}
        </div>
    </div>
 )
}
export default ProductPage;
