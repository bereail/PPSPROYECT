import React, { useContext, useEffect, useState } from 'react';
import api from '../../api';
import { OrderContext } from '../Context/OrderContext';
import { useNavigate } from 'react-router-dom';
import "./PaySuccess.css"
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const PaySuccess = () => {
    

    const [orderId, setOrderId] = useState(null);
    const navigate = useNavigate();
    const [isPaid, setIsPaid] = useState(false);

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
        const response = await api.post(`/api/orders/${orderId}/payment/success`);
        if (response.status === 200) {
          toast.success('The payment was successful!');
          setIsPaid(true);
        }
      } catch (error) {
        toast.error('Failed to process the payment.');
      }
  
};

return (
  <div className="success-container">
    <div className="success-message">
      <h2>The payment was successful</h2>
    </div>
    {!isPaid && <button onClick={handlePay}>Pay with Mercado Pago</button>}
  </div>
);
};

export default PaySuccess;
