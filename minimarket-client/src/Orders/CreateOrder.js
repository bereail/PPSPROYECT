import React from 'react';
import api from '../api';

const CreateOrder = async (OrdenDetails, SetOrderid) => {
  try {
    const response = await api.post("/api/orders", OrdenDetails);
    
    if (response.status === 200) {
      alert(response.data.id)
      SetOrderid(response.data.id);
      //window.location.href = '/productMP'; // Redirigir a la página deseada después de crear la orden
    }
  } catch(error) {
    console.error('Error Create Order', error);
  }

  // Aquí podrías retornar algún JSX si es necesario, aunque en tu caso actual no es necesario.
  return null;
}

export default CreateOrder;
