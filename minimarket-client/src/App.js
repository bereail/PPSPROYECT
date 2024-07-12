import React, { useContext } from "react";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import Home from "./components/Home/home";
import Signin from "./components/Login/SignIn/SignIn";
import SignupUser from "./components/Login/SingUp/SignUpUser";
import Cart from "./components/Cart/Cart";
import User from "./components/User/User";
import Error404 from "./components/Pages/Error404";
import "./App.css";
import 'bootstrap/dist/css/bootstrap.min.css'
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { ThemeContext, ThemeProvider } from "./components/Context/ThemeContext";
import ProtectedUser from "./components/Pages/ProtectedUser";
import Spinner from "./components/Context/Spinner"; 
import CustomChatbot from "./components/ChatBot/ChatBot";
import { CategoryProvider } from "./components/Context/CategoryContext";
import { AuthProvider } from "./components/Context/AuthContext";
import Favorite from "./components/Favorite/Favorite";
import Navbar from "./components/Navbar/Navbar";
import { SearchProvider } from "./components/Context/SearchContext";
import FaQs from "./components/Footer/FaQs";
import { OrderProvider } from "./components/Context/OrderContext";
import PaySuccess from "./components/PayWhitMP/PaySuccess";
import ProtectedMp from "./components/Pages/ProtectedMp";
import ResetPassword from "./components/Login/SignIn/ResetPassword";
import ResetPasswordForm from './components/Login/SignIn/ResetPasswordForm';
import ProtectedResetPassword from "./components/Pages/ProtectedResetPassword";

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
    path: "navbar",
    element: <Navbar />,
  },
  {
    path: "/Favorite",
    element: <ProtectedUser>
      <Favorite />,
    </ProtectedUser>
  },
  {
    path: "user",
    element: 
      <ProtectedUser>
        <User />
      </ProtectedUser>
    
  },
  {
    path: "FaQs",
    element: <FaQs />
  },
  {
    path: "*",
    element: <Error404 />,
  },
  {
    path: "/PaySucess",
    element: <ProtectedMp>
      <ProtectedUser>
        <PaySuccess />
      </ProtectedUser>
    </ProtectedMp>,
  },
  {
    path: "reset-password",
    element: <ResetPassword />,
  },
  {
    path: "ResetPasswordForm",
    element:<ProtectedResetPassword>
      <ResetPasswordForm />
      </ProtectedResetPassword> ,
  },

]);

const AppContent = () => {
  const { theme, isLoading } = useContext(ThemeContext);

  return (
    <div className={`${theme === "dark" && "dark-theme"} ${isLoading && "opacity-50"}`}>
      {isLoading && <Spinner />}
      <RouterProvider router={router} />
    </div>
  );
};

const App = () => (
  <ThemeProvider>

  <OrderProvider>
      <AuthProvider>
        <CategoryProvider>
          <SearchProvider>
            <AppContent />
            <ToastContainer />
          </SearchProvider>
          <CustomChatbot />
        </CategoryProvider>
      </AuthProvider>
  </OrderProvider>
  </ThemeProvider>

);

export default App;
