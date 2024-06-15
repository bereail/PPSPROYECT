import React from 'react'
import api from '../../../api';

const GetProductsByOffers = async (setProducts, setError) => {


    try {
        const response = await api.get("/api/products/offers");;
        setProducts(response.data);
        setError(null)
        
    } catch (error) {
        console.error('Error fetching products:', error);
    }



}

export default GetProductsByOffers
