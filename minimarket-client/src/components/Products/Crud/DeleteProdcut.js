import React from 'react'
import api from '../../../api'

const DeleteProdcut = async(product) => {
   
    try{
        const response = await api.delete(`/api/products/${product}/erase`)
        return response
      }catch(error){
        console.log('Error disabel products:', error)
      }
}

export default DeleteProdcut
