import React from 'react'
import api from '../api'

const GetOrder = async() => {
    try{
        await api.get('/api/orders')
    }catch(error){
        console.log('error fetching order',error)
    }
  return (
    <div>
      
    </div>
  )
}

export default GetOrder
