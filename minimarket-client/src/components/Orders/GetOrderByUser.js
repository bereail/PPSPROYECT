import React from 'react'
import api from '../../api';

const GetOrderByUser = async(pageNumber) => {
    try {
        const response = await api.get('/api/users/profile/orders', {
            params: { 
                pageNumber: pageNumber
              }
        });
        return response.data;
    } catch (error) {
        throw error;
    }
};




export default GetOrderByUser
