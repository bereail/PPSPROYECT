import React from 'react'
import api from '../../../api';

const GetProductBySearch = async(setProducts, setError, SearchValue, pageNumber) => {
    try {

        const response = await api.get(`/api/products`, {
            params: { 
                filterOn: 'Name',
                filterQuery: SearchValue,
                pageNumber: pageNumber
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
