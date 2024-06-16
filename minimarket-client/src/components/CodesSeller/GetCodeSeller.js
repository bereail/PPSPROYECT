import React from 'react'
import api from '../../api';

const GetCodeSeller = async() => {
    try {
        const response = await api.get('/api/codes');
        console.log(response.data)
        return response.data;
    } catch (error) {
        console.log('error fetching codes', error);
        return null;
    }


}

export default GetCodeSeller
