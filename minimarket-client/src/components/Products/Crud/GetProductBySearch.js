import React from 'react'
import api from '../../../api';

const GetProductBySearch = async(setProducts, setError, SearchValue) => {
    try {
        const response = await api.get(`/api/products`, {
            params: { 
                filterQuery: SearchValue
              }
        });
        setProducts(response.data); 
        setError(null);
      } catch (error) {
        console.error('Error fetching products category:', error);
        setError(error)
      }
    
};
export default GetProductBySearch
