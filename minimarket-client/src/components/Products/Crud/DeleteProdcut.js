import React from 'react'
import api from '../../../api'

const DeleteProdcut = async(product) => {
   
    try{
        const response = await api.delete(`/api/products/${product}/erase`)
        return response
      }catch(error){
        throw error;
      }
}

export default DeleteProdcut
