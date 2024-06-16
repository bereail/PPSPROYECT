import React, { useEffect, useState } from 'react';
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faHeart } from "@fortawesome/free-solid-svg-icons";

const GetProductsFavorite = ({ product, userEmail, favoriteHandler }) => {
    const [isFavorite, setIsFavorite] = useState(false);

    useEffect(() => {
        const storedFavorites = JSON.parse(window.localStorage.getItem(`Favorite_${userEmail}`));
        if (storedFavorites && storedFavorites.products) {
            setIsFavorite(storedFavorites.products.some(favorite => favorite.id === product.id));
        }
    }, [userEmail, product]);

    const handleFavoriteClick = () => {
        if (isFavorite) {
            removeFromFavorites();
        } else {
            addToFavorites();
        }
    };

    const addToFavorites = () => {
        setIsFavorite(true);
        favoriteHandler(product);
    };

    const removeFromFavorites = () => {
        setIsFavorite(false);
        const storedFavorites = JSON.parse(window.localStorage.getItem(`Favorite_${userEmail}`));
        const updatedFavorites = storedFavorites.products.filter(favorite => favorite.id !== product.id);
        window.localStorage.setItem(`Favorite_${userEmail}`, JSON.stringify({ products: updatedFavorites }));
    };

    return (
        <div>
            <FontAwesomeIcon
                icon={faHeart}
                className={isFavorite ? 'icon-favorite' : 'icon-heart'}
                onClick={handleFavoriteClick} 
            />
            
        </div>
    );
}

export default GetProductsFavorite;
