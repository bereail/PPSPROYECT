import React from 'react'
import api from '../../api'

const DeleteCodeSeller = async(codeId) => {
  
    try{
       const response = await api.delete(`/api/codes/${codeId}/erase`, codeId);
       return response
    }catch(error){
        console.error("Error Delete Code", error)
    }
  

}

export default DeleteCodeSeller
