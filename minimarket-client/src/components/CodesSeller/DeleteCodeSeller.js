import React from 'react'
import api from '../../api'

const DeleteCodeSeller = async(codeId) => {
  
    try{
       const response = await api.delete(`/api/codes/${codeId}/erase`, codeId);
       return response
    }catch(error){
        throw error;
    }
  

}

export default DeleteCodeSeller
