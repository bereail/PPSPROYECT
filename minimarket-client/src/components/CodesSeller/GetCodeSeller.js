import React from 'react'
import api from '../../api';

const GetCodeSeller = async() => {
    try {
        const response = await api.get('/api/codes');
        return response.data;
    } catch (error) {
        throw error;
    }


}

export default GetCodeSeller
