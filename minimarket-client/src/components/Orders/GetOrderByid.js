import React from 'react'
import api from '../../api';

const GetOrderByid = async(orderId) => {

    try {
        const response = await api.get(`/api/orders/${orderId}`);
        return response.data;
    } catch (error) {
        throw error;
    }

}

export default GetOrderByid
