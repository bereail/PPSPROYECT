import React from 'react'
import api from '../../../api';


const ModifyProducts = async(InputValue, prodcutid) => {
  
  if (InputValue.name !== '' && InputValue.description !== '' && InputValue.price !== null && InputValue.discount !== null) {
    try{
      await api.put(`/api/products/${prodcutid}`,InputValue)   
      window.location.reload();
    }catch(error){
      console.log('Error Modify Prodcuts', error)
    }
}
}
export default ModifyProducts
