import React from 'react'
import api from '../../../api';


const ModifyProducts = async(InputValue, prodcutid) => {
  
  if (InputValue.name !== '' && InputValue.description !== '' && InputValue.price !== null && InputValue.discount !== null) {
    try{
      const response = await api.put(`/api/products/${prodcutid}`,InputValue)   
      return response
    }catch(error){
      console.log('Error Modify Prodcuts', error)
    }
}
}
export default ModifyProducts
