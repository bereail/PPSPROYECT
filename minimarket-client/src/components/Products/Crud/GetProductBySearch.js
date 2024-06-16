import React from 'react'
import api from '../../../api';

const GetProductBySearch = async(setProducts, setError, SearchValue, pageNumber) => {
    try {
        console.log(SearchValue)
        const response = await api.get(`/api/products`, {
            params: { 
                filterQuery: SearchValue,
                pageNumber: pageNumber
              }
        });
        console.log(response.data)
        setProducts(response.data); 
        setError(null);
      } catch (error) {
        console.error('Error fetching products category:', error);
        setError(error)
      }
    
};
export default GetProductBySearch
