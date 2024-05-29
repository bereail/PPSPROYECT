import React from 'react'
import api from '../../../api';
import GetProductsByCategory from './GetProducstByCategory';
const RestoreProducts = async(productId) => {

    try{
        await api.patch(`/api/products/${productId}`);
    }catch(error){
        console.log('Error Restaured Products', error)
    }

}

export default RestoreProducts
