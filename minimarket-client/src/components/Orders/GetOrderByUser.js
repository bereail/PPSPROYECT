import React from 'react'
import api from '../../api';

const GetOrderByUser = async() => {
    try {
        const response = await api.get('/api/users/profile/orders', {
            params: { 
                pageNumber: 3
              }
        });
        return response.data;
    } catch (error) {
        console.log('error fetching order', error);
        return null;
    }
};




export default GetOrderByUser
