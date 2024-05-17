import { createBrowserRouter, RouterProvider } from "react-router-dom";
import Home from "./components/Home/home";
import Signin from "./components/Login/SignIn/SignIn";
import SignupUser from "./components/Login/SingUp/SignUpUser";
import Cart from "./components/Cart/Cart";
import User from "./components/User/User";
import Error404 from "./components/Pages/Error404"; // Import Error404 component
import './App.css';
import ProtectedUser from "./components/Pages/ProtectedUser";

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
    element: <ProtectedUser><User/></ProtectedUser>
  },
  {
    path: "*",
    element: <Error404 />, // Use Error404 for unmatched paths
  },
]);

const App = () => {
  return (
    <div className="app">
      <RouterProvider router={router} />
    </div>
  );
};

export default App;

