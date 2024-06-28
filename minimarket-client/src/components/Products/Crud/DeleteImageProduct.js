import React from 'react'
import api from '../../../api'

const DeleteImageProduct = ({productId, fetchProducts}) => {

    const HandleDeleteImage = async() =>{
        try{
            await api.delete(`/api/products/${productId}/images`)
            fetchProducts();

        }catch(error){
        }
    }

  return (
    <div>
      <button className="Delete-Image"onClick={HandleDeleteImage}>DeleteImage</button>
    </div>
  )
}

export default DeleteImageProduct
