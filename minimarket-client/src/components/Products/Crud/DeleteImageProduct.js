import React from 'react'
import api from '../../../api'

const DeleteImageProduct = ({productId}) => {

    const HandleDeleteImage = async() =>{
        try{
            await api.delete(`/api/products/${productId}/images`)
        }catch(error){
            console.log("Errro delete prodcuts", error)
        }
    }

  return (
    <div>
      <button className="Delete-Image"onClick={HandleDeleteImage}>DeleteImage</button>
    </div>
  )
}

export default DeleteImageProduct
