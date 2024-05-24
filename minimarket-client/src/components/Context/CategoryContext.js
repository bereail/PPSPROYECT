import React, { createContext, useState } from "react";

export const CategoryContext = createContext();

export const CategoryProvider = ({ children }) => {
  const [CategoryId, SetCategoryId] = useState(null);

  return (
    <CategoryContext.Provider value={{ CategoryId, SetCategoryId }}>
      {children}
    </CategoryContext.Provider>
  );
};
