import React from 'react'
import api from '../../../api';
import GetProductsByCategory from './GetProducstByCategory';
const RestoreProducts = async(productId) => {

    try{
        const response = await api.patch(`/api/products/${productId}`);
        return response
    }catch(error){
        throw error;
    }

}

export default RestoreProducts
