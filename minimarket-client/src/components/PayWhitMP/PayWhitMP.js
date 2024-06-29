import React, { useContext, useEffect, useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import api from '../../api';
import { initMercadoPago, Wallet } from '@mercadopago/sdk-react'
import { OrderContext } from '../Context/OrderContext';

const PayWhitMP = () => {
 
  const navigate = useNavigate();
  const [preferenceId, SetpreferenceId] = useState()

  const {orderId} = useContext(OrderContext);
             

  useEffect(() => {
    window.localStorage.removeItem('PreferenceId');
    GetPreferenceId()
  }, [orderId]);

  useEffect(() => {
    // Solo inicializa MercadoPago si preferenceId está disponible
    if (preferenceId) {
      initMercadoPago('APP_USR-70da26e9-4bcb-4a7d-b1db-835f42ae57a2', { 
        locale: "es-AR"
      });
    }
  }, [preferenceId]);

  const GetPreferenceId = async () => {
    try {
        const response = await api.post(`/api/orders/${orderId}/payment`);
        SetpreferenceId(response.data.preferenceId)
        window.localStorage.setItem('PreferenceId', response.data.preferenceId); 

    } catch (error) {
    
        console.error('Error making API request:', error);  
        
        navigate(`/?message=The%20payment%20order%20has%20been%20canceled`);
      }
    }


  return (
    <div>
      {preferenceId &&  <Wallet initialization={{ preferenceId: preferenceId}}  customization={{ texts:{ valueProp: 'smart_option'}}} /> }       
    </div>
  );
}

export default PayWhitMP;
