import React, { useState, useEffect, useContext } from "react";
import axios from 'axios';
import CustomNavbar from "../../Navbar/CustomNavbar";
import Footer from "../../Footer/footer";
import "./SignIn.css"
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEyeSlash } from "@fortawesome/free-solid-svg-icons";
import { faEye } from "@fortawesome/free-solid-svg-icons";
import { Navigate } from "react-router-dom";
import api from "../../../api";
import { ThemeContext } from "../../Context/ThemeContext";
import { AuthContext } from "../../Context/AuthContext";
const Signin = () => {
  const { theme } = useContext(ThemeContext);
  const [Style, SetStyle] = useState(faEyeSlash);
  const [TypeInput, SetTypeInput] = useState("Password");
  const [email, setEmail] = useState('');
  const [pass, setPass] = useState('');
  const [Error, SetError] = useState('');
 const [ErrorLogin, SetErrorLogin] = useState(0);

  const {user,login} = useContext(AuthContext);

  
  useEffect(() => {
    SetError(0);
  }, []);

  const handleEmailChange = (e) => {
    setEmail(e.target.value);
  }

  const handlePassChange = (e) => {

    setPass(e.target.value);
  }

  const handleSubmit = async(e) => {
    e.preventDefault();
    if(email == ''){
      SetError(1);
      return;
    }
    if(pass === ''){
      SetError(1);
      return;
    }
    
    const data = {
      Email: email,
      Password: pass,
    };
    
    try {
      const response = await api.post('/api/auth/login', data);

      if (response.status === 200) {
        SetErrorLogin(0);     
        login(response.data, email) 
      }
    } catch (error) {
      SetErrorLogin(1);
      console.error('Error:', error.message);
    }


  }

  return (
    <div className="login" >
      <CustomNavbar/>
      {user ? <Navigate to="/" /> : null}
      <div className="Form-Login" style={{ backgroundColor: theme === "light" ? "#CC713D" : "#a5351ca4" }}>
        <h2>Login</h2>
      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label htmlFor="exampleInputEmail1" className="form-label">Email address</label>
          {Error === 1 && email === '' && <p className='Error'>Set a Email</p>}
          <input type="email" className="form-control" id="exampleInputEmail1" aria-describedby="emailHelp" onChange={handleEmailChange} />
        </div>
        <div className="mb-3"style={{ position: 'relative' }}>
          <label htmlFor="exampleInputPassword1" className="form-label">Password</label>
          <div>
          <input type={TypeInput} className="form-control" id="exampleInputPassword1" onChange={handlePassChange} />
          <FontAwesomeIcon icon={Style} className="icon" onClick={() => (SetTypeInput(
            Style === faEyeSlash ? "Text" : "Password"),
          SetStyle(Style === faEyeSlash ? faEye : faEyeSlash))}></FontAwesomeIcon>
                      
          </div>
          {Error === 1 && pass === '' && <p className='Error'>Set a password</p>}
        </div>
           {ErrorLogin === 1 && <p className='Error-Login'>Incorrect user or password</p>}
        <button type="submit" className="btn btn-primary">Submit</button>
      </form>
      </div>
      <Footer/>
    </div>
  );
}

export default Signin;