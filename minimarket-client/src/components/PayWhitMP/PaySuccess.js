import React, { useContext, useEffect } from 'react';
import api from '../../api';
import { OrderContext } from '../Context/OrderContext';


const PaySuccess = () => {
    const {orderId} = useContext(OrderContext);

    useEffect(() => {
    const handlePay = async () => {
        alert(orderId)
      const data = {
        orderId: orderId,
        userId: "F815E676-F474-4A97-A397-A2D969A1F594"
      };
      
      try {
        const response = await api.post(`/api/orders/${orderId}/payment/success`, data);
        console.log(response.data);
      } catch (error) {
        console.error('Error making API request:', error);
      }
    };

    handlePay();
  }, [orderId]);

  return (
    <div style={{ background: 'green', textAlign: 'center', fontSize: '100px' }}>
      EXITO
    </div>
  );
};

export default PaySuccess;
