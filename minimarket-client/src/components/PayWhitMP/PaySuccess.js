import React, { useContext, useEffect, useState } from 'react';
import api from '../../api';
import { OrderContext } from '../Context/OrderContext';
import { useNavigate } from 'react-router-dom';
import "./PaySuccess.css"

const PaySuccess = () => {
    

    const [orderId, setOrderId] = useState(null);
    const navigate = useNavigate();


    useEffect(() => {
      const storedOrderId = window.localStorage.getItem('OrderId');
      if (storedOrderId) {
        setOrderId(storedOrderId);
      } 
    }, []);


    useEffect(() => {
      if (orderId) {
        handlePay();
      }
    }, [orderId]);

    useEffect(() => {
      if (orderId) {
        const timer = setTimeout(() => {
          navigate('/'); 
        }, 3000);
  
        return () => clearTimeout(timer); 
      }
    }, [orderId, navigate]);

  const handlePay = async () => {

  

  try {
    await api.post(`/api/orders/${orderId}/payment/success`);
  } catch (error) {
  }
};

return (
  <div className="success-container">
    <div className="success-message">
      <h2>The payment was successful</h2>
    </div>
  </div>
);
};

export default PaySuccess;
