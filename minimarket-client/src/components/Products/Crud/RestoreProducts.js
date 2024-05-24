import React from 'react'
import api from '../../../api';

const RestoreProducts = async(productId) => {
    alert('productId')
    alert(productId)
    try{
        await api.patch(`/api/products/${productId}`);
        window.location.reload();
    }catch(error){
        console.log('Error Restaured Products', error)
    }

}

export default RestoreProducts
