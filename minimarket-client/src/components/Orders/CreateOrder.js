import React from 'react'; 
import api from '../../api';  

const CreateOrder = async (OrdenDetails) => {
  try {
    const response = await api.post('/api/orders', OrdenDetails);

    if (response.status === 200) {
      const orderId = response.data.id;
      console.log('Order created successfully!', orderId);
      return orderId;
    } else {
      
      throw new Error(`Order creation failed with status code: ${response.status}`);
    }
  } catch(error) {
    console.error('Error creating order:', error); 
  }
}

export default CreateOrder;

