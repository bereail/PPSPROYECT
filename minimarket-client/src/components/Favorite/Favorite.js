import React, { useContext, useEffect, useState } from 'react';
import { AuthContext } from '../Context/AuthContext';

const Favorite = () => {
    const { userEmail } = useContext(AuthContext);
    const [favoriteProducts, setFavoriteProducts] = useState([]);

    useEffect(() => {
        const storedProducts = JSON.parse(window.localStorage.getItem(`Favorite_${userEmail}`));
        if (storedProducts && storedProducts.products) {
            setFavoriteProducts(storedProducts.products);
        }
    }, [userEmail]);

    return (
        <div>
            {favoriteProducts.length === 0 ? (
                <p>VACIO</p>
            ) : (
                <div>
                    {favoriteProducts.map((product, index) => (
                        <div key={index} className="product-item">
                            <div>
                                <h3>{product.name}</h3>
                                <p>Description: {product.description}</p>
                                <p>Price: {product.price}</p>
                                <p>With discount: {product.discount}</p>
                            </div>
                        </div>
                    ))}
                </div>
            )}
        </div>
    );
};

export default Favorite;
