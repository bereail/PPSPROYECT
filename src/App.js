import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";
import Home from "./components/Home/home";
import Logout from "./components/Login/logout";
import Signin from "./components/Login/SignIn/SignIn";
import SignupUser from "./components/Login/SingUp/SignUpUser";
import SignupEmployee from "./components/Login/SingUp/SignUpEmployee";



const App = () => {
  const router = createBrowserRouter([

    {
      path: "signin", element: <Signin />
    },
    {
      path: "/", element: <Home />
    },
    {
      path: "/logout", element: <Logout />
    }
    ,
    {
      path: "/signupUser", element: <SignupUser />
    },
    {
      path: "/signupEmployee", element: <SignupEmployee />
    }
  ])
  return( 
    <div>
      <RouterProvider router={router}></RouterProvider>
    </div>
  );
}

export default App;
