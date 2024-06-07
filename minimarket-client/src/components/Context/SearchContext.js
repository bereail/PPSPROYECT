import React, { createContext, useState } from "react";

export const SearchContext = createContext();

export const SearchProvider = ({ children }) => {
  const [SearchValue, SetSearchValue] = useState(null);

  return (
    <SearchContext.Provider value={{ SearchValue, SetSearchValue }}>
      {children}
    </SearchContext.Provider>
  );
};
