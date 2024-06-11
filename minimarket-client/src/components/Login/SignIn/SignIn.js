import React, { useState, useEffect, useContext } from "react";
import axios from 'axios';
import Footer from "../../Footer/footer";
import "./SignIn.css"
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEyeSlash } from "@fortawesome/free-solid-svg-icons";
import { faEye } from "@fortawesome/free-solid-svg-icons";
import { Navigate } from "react-router-dom";
import api from "../../../api";
import { ThemeContext } from "../../Context/ThemeContext";
import { AuthContext } from "../../Context/AuthContext";
import Navbar from "../../Navbar/Navbar";
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
    if(pass === '' || pass.length < 8  ){
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
      
      if(response.status === 401){
        SetErrorLogin(1);
      }
    } catch (error) {
      if (error.response && error.response.status === 401) {
        SetErrorLogin(1); 
      } else {
        console.error('Error:', error.message); 
      }
    }


  }

  return (
    <div  >
      <Navbar/>
      <div className="Singin-Container">
      {user ? <Navigate to="/" /> : null}
      <div className="Form-Login" style={{ backgroundColor: theme === "light" ? "#CC713D" : "#a5351ca4" }}>
        <h2>Sing In</h2>
      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label htmlFor="exampleInputEmail1" className="form-label">Email address</label>
          <input type="email" className="form-control" id="exampleInputEmail1" aria-describedby="emailHelp" onChange={handleEmailChange} />
          {Error === 1 && email === '' && <p className='Error-SingIn'>Set a Email</p>}
        </div>
        <div className="mb-3"style={{ position: 'relative' }}>
          <label htmlFor="exampleInputPassword1" className="form-label">Password</label>
          <div>
          <input type={TypeInput} className="form-control" id="exampleInputPassword1" onChange={handlePassChange} />
          <FontAwesomeIcon icon={Style} className="icon" onClick={() => (SetTypeInput(
            Style === faEyeSlash ? "Text" : "Password"),
          SetStyle(Style === faEyeSlash ? faEye : faEyeSlash))}></FontAwesomeIcon>
                      
          </div>
          {Error === 1 && (pass === '' || pass.length < 8)&& <p className='Error-SingIn'>Set a correct password</p>}
        </div>
           {ErrorLogin === 1 && <p className='Error-Login'>Incorrect user or password</p>}
        <button type="submit" className="botton-Singin">Submit</button>
      </form>
      </div>
      </div>
      <Footer/>
    </div>
  );
}

export default Signin;