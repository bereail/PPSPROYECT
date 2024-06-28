import React from 'react'
import api from '../../api';

const CreateCodeSeller = async(data) => {
    try {
        
        const response = await api.post('/api/codes',data);
        return response;
    } catch (error) {
        throw error;
    }
}

export default CreateCodeSeller
