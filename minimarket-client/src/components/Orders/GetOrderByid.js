import React from 'react'
import api from '../../api';

const GetOrderByid = async(orderId) => {

    try {
        const response = await api.get(`/api/orders/${orderId}`);
        console.log(response.data)
        return response.data;
    } catch (error) {
        console.log('error fetching order', error);
        return null;
    }

}

export default GetOrderByid
