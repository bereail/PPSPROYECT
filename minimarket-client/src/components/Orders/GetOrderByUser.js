import React from 'react'
import api from '../../api';

const GetOrderByUser = async() => {
    try {
        const response = await api.get('/api/users/profile/orders');
        return response.data;
    } catch (error) {
        console.log('error fetching order', error);
        return null;
    }
};




export default GetOrderByUser
