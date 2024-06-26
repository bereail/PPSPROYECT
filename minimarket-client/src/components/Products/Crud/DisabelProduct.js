import React, { useContext } from 'react'
import api from '../../../api';
import removeProducstFromCarts from '../removeProducstFromCarts';
import GetProductsByCategory from './GetProducstByCategory';

const DisabelProduct = async(product) => {

    try{
      const response = await api.delete(`/api/products/${product}`)
      removeProducstFromCarts(product)
      return response
    }catch(error){
      throw error;
    }
}

export default DisabelProduct
