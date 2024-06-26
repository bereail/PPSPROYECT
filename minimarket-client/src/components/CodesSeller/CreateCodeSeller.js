import React from 'react'
import api from '../../api';

const CreateCodeSeller = async(data) => {
    try {
        
        const response = await api.post('/api/codes',data);
        return response.data;
    } catch (error) {
        console.log('error fetching codes', error);
        return null;
    }
}

export default CreateCodeSeller
