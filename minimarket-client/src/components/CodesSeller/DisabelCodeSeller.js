import React from 'react'
import api from '../../api';

const DisabelCodeSeller = async(codeId) => {
    try{
        const response = await api.delete(`/api/codes/${codeId}`, codeId);
        return response
     }catch(error){
        throw error;
     }
   
}

export default DisabelCodeSeller
