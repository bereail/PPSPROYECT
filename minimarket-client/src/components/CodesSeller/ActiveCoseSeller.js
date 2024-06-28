import React from 'react'
import api from '../../api';

const ActiveCoseSeller = async(codeId) => {
    try{
        const response = await api.patch(`/api/codes/${codeId}`, codeId);
        return response
     }catch(error){
        throw error;
     }
   
}

export default ActiveCoseSeller
