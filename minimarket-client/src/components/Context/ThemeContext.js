import React, { createContext, useState } from "react";
export const ThemeContext = createContext();

export const ThemeProvider = ({ children }) => {
  const [theme, setTheme] = useState("light");
  const [isLoading, setIsLoading] = useState(false);

  const toggleTheme = () => {
    setTheme((prevTheme) => (prevTheme === "light" ? "dark" : "light"));
  };

  return (
    <ThemeContext.Provider value={{ theme, toggleTheme, isLoading, setIsLoading }}>
      {children}
    </ThemeContext.Provider>
  );
};

