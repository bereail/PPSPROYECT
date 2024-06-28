import React, { useContext, useEffect, useState } from 'react';
import { AuthContext } from '../Context/AuthContext';
import Navbar from '../Navbar/Navbar';
import Footer from '../Footer/footer';
import './Favorite.css'; // Crear e importar un archivo CSS para estilos
import FavoriteEmpty from './../Image/FavoriteEmpty.png';
import StoreProducts from '../Products/StoreProducts';
import { ThemeContext } from '../Context/ThemeContext';

const Favorite = () => {
    const { userEmail } = useContext(AuthContext);
    const [favoriteProducts, setFavoriteProducts] = useState([]);
    const [quantities, setQuantities] = useState({});
    const { theme } = useContext(ThemeContext);
    useEffect(() => {
        const storedProducts = JSON.parse(window.localStorage.getItem(`Favorite_${userEmail}`));
        if (storedProducts && storedProducts.products) {
            setFavoriteProducts(storedProducts.products);
            const initialQuantities = {};
            storedProducts.products.forEach(product => {
                initialQuantities[product.id] = 1;
            });
            setQuantities(initialQuantities);
        }
    }, [userEmail]);

    const handleCleanCart = () => {
        window.localStorage.removeItem(`Favorite_${userEmail}`);
        setFavoriteProducts([]);
    }

    const addCartHandler = (product) => {
        const data = {
            id: product.id,
            name: product.name,
            description: product.description,
            price: product.price,
            discount:  ((product.price - product.discount) / product.price) * 100
        };
        StoreProducts('Cart', data, quantities, userEmail);
    };

    const handleQuantityChange = (productId, value) => {
        setQuantities(prevQuantities => ({
            ...prevQuantities,
            [productId]: value
        }));
    };

    return (
        <div>
            <Navbar />

            {favoriteProducts.length === 0 ? (
                <div className="no-favorites">
                    <img src={FavoriteEmpty} alt="No favorites" className="no-favorites-image" />
                    <h1>You don't have favorite products</h1>
                    <p>It looks like you haven't added any products to your favorites yet.</p>
                    <button className="browse-products-button" onClick={() => window.location.href = '/'}>
                    Explore products
                    </button>
                </div>
            ) : (
                <div className="favorites-list">
                    {favoriteProducts.map((product, index) => (
                    <div key={index} className={`${theme === "dark" ? "dark-product-item" : "product-item "}`}>
                            <div>
                                <h3>{product.name}</h3>
                                <p>Description: {product.description}</p>
                                <p>Price: {product.price}</p>
                                <p>With discount: {product.discount}</p>
                            </div>

                            <div className="Container-Button-Favorite">
                                <button onClick={() => handleQuantityChange(product.id, Math.max(quantities[product.id] - 1, 1))}>-</button>
                                <input min="1" value={quantities[product.id] || 1} onChange={(e) => handleQuantityChange(product.id, parseInt(e.target.value) || 1)} />
                                <button onClick={() => handleQuantityChange(product.id, (quantities[product.id] || 1) + 1)}>+</button>
                            </div>
                            <button className="Quantities-Favorite" onClick={() => addCartHandler(product)}>Add to cart</button>
                        </div>
                    ))}
                    <button className="Button-Products" onClick={handleCleanCart}>Clear Favorites</button>
                </div>
            )}

            <Footer />
        </div>
    );
};

export default Favorite;
