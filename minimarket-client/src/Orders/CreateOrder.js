import React from 'react';
import api from '../api';

const CreateOrder = async (OrdenDetails) => {
  try {
    const response = await api.post("/api/orders", OrdenDetails);
    
    if (response.status === 200) {
      const orderId = response.data.id;

      return orderId;
    }
  } catch(error) {
    console.error('Error Create Order', error);
  }

  // Aquí podrías retornar algún JSX si es necesario, aunque en tu caso actual no es necesario.
  return null;
}

export default CreateOrder;
