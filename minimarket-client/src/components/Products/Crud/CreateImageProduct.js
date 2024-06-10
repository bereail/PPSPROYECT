import React from 'react'
import api from '../../../api';

const CreateImageProduct = async() => {
    const data= {

    }
    try {    
        await api.post(`/api/products/${prodcutid}/images`, data);
        window.location.reload();
    } catch (error) {
      console.error('Error add Image:', error);
    }
  return (
    <div>
      
    </div>
  )
}

export default CreateImageProduct
