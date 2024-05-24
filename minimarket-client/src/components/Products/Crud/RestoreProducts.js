import React from 'react'
import Api from '../../../Api';

const RestoreProducts = async(productId) => {
    alert('productId')
    alert(productId)
    try{
        const api = Api()
        await api.patch(`/api/products/${productId}`);
        window.location.reload();
    }catch(error){
        console.log('Error Restaured Products', error)
    }

}

export default RestoreProducts
