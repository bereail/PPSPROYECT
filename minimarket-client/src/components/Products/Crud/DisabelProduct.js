import React from 'react'
import Api from '../../../Api';

const DisabelProduct = async(product) => {
    try{
      const api = Api()
      await api.delete(`/api/products/${product}`)
      window.location.reload();
    }catch(error){
      console.log('Error disabel products:', error)
    }
}

export default DisabelProduct
