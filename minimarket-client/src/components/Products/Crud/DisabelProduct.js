import React, { useContext } from 'react'
import api from '../../../api';
import removeProducstFromCarts from '../removeProducstFromCarts';
import GetProductsByCategory from './GetProducstByCategory';

const DisabelProduct = async(product) => {

    try{
      await api.delete(`/api/products/${product}`)
      removeProducstFromCarts(product)
      window.location.reload();
    }catch(error){
      console.log('Error disabel products:', error)
    }
}

export default DisabelProduct
