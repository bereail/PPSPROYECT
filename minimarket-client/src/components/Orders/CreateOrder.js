import React from 'react'; 
import api from '../../api';  

const CreateOrder = async (OrdenDetails) => {
  try {
    const response = await api.post('/api/orders', OrdenDetails);

    if (response.status === 200) {
      const orderId = response.data.id;
      return orderId;
    } else {
      throw new Error(`Order creation failed with status code: ${response.status}`);
    }
  } catch(error) {
  }
}

export default CreateOrder;

