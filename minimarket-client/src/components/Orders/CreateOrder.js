import React from 'react'; 
import api from '../../api';  
import { toast } from 'react-toastify';


const CreateOrder = async (OrdenDetails) => {
  try {
    const response = await api.post('/api/orders', OrdenDetails);

    if (response.status === 200) {
      const orderId = response.data.id;
      toast.success('Order created successfully!');
      return orderId;
    } else {
      throw new Error(`Order creation failed with status code: ${response.status}`);
    }
  } catch(error) {
    toast.error('Failed to create order.');
  }
}

export default CreateOrder;

