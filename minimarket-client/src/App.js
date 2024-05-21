import React, { useContext } from "react";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import Home from "./components/Home/home";
import Signin from "./components/Login/SignIn/SignIn";
import SignupUser from "./components/Login/SingUp/SignUpUser";
import Cart from "./components/Cart/Cart";
import User from "./components/User/User";
import Error404 from "./components/Pages/Error404";
import "./App.css";
import { ThemeContext, ThemeProvider } from "./components/Context/ThemeContext";
import ProtectedUser from "./components/Pages/ProtectedUser";
import Spinner from "./components/Context/Spinner"; // Asegúrate de que esta ruta sea correcta
import ProductPage from "./components/Products/ProductPage";

const router = createBrowserRouter([
  {
    path: "/",
    element: <Home />,
  },
  {
    path: "signin",
    element: <Signin />,
  },
  {
    path: "signupUser",
    element: <SignupUser />,
  },
  {
    path: "cart",
    element: <Cart />,
  },
  {
    path: "user",
    element: (
      
        <User />
     
    ),
  },
  {
    path: "/products/All",
    element: <ProductPage />,
  },
  {
    path: "*",
    element: <Error404 />,
  },
]);

const AppContent = () => {
  const { theme, isLoading } = useContext(ThemeContext);

  return (
    <div className={`${theme === "dark" && "dark-theme"} ${isLoading && "opacity-80"}`}>
      {isLoading && <Spinner />}
      <RouterProvider router={router} />
    </div>
  );
};

const App = () => (
  <ThemeProvider>
    <AppContent />
  </ThemeProvider>
);

export default App;
