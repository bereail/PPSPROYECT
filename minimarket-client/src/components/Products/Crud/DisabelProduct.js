import React from 'react'
import api from '../../../api';

const DisabelProduct = async(product) => {
    try{
      await api.delete(`/api/products/${product}`)
      window.location.reload();
    }catch(error){
      console.log('Error disabel products:', error)
    }
}

export default DisabelProduct
