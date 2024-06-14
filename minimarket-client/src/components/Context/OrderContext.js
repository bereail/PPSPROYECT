import React, { createContext, useEffect, useState } from 'react';

export const OrderContext = createContext();

export const OrderProvider = ({ children }) => {
  const [orderId, setOrderId] = useState();


  return (
    <OrderContext.Provider value={{ orderId, setOrderId }}>
      {children}
    </OrderContext.Provider>
  );
};