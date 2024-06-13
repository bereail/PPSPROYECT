import React, { useContext, useEffect, useState } from "react";
import axios from 'axios';

import Footer from "../../Footer/footer";
import './SignUpUser.css';
import { Link } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEyeSlash, faEye } from "@fortawesome/free-solid-svg-icons";
import { Navigate } from "react-router-dom";
import api from "../../../api";
import { ThemeContext } from "../../Context/ThemeContext";
import { AuthContext } from "../../Context/AuthContext";
import Navbar from "../../Navbar/Navbar";
const SignupUser = () => {
  const { theme } = useContext(ThemeContext);
  const [showPassword, setShowPassword] = useState(false);
  const [ExistingUser, SetExistingUser] = useState(false)
  const [formData, setFormData] = useState({
    name: '',
    email: '',
    password: '',
    confirmPassword: '',
    phoneNumber: '',
    hexadecimalCode: '' 
  });
  const [errors, setErrors] = useState({
    name: false,
    phoneNumber: false,
    email: false,
    password: false,
    confirmPassword: false, 
    hexadecimalCode: false
  });
  const [Seller, SetSeller] = useState(false);
  const {user, logout, login} = useContext(AuthContext);
  const [ErrorLogin, SetErrorLogin] = useState(false);

  useEffect(()=>{
    const logged = window.localStorage.getItem('LoggedUser')
    if(logged){
    }
  },[])
  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData(prevState => ({
      ...prevState,
      [name]: value
    }));
    setErrors(prevErrors => ({
      ...prevErrors,
      [name]: value.trim() === ''
    }));
  }


  const handleSubmit = async (e) => {
   
    e.preventDefault();
 
    const { name, phoneNumber, email, password, confirmPassword, hexadecimalCode  } = formData;
   
    if (phoneNumber.trim().length > 13) {
      setErrors(prevErrors => ({
        ...prevErrors,
        phoneNumber: true
      }));
      return;
    }

    if (name.trim() === '' === '' || phoneNumber.trim() === '' || email.trim() === '' || password.trim() === '' || confirmPassword !== password) {
      setErrors({
        name: name.trim() === '',
        phoneNumber: phoneNumber.trim() === '',
        email: email.trim() === '',
        password: password.length < 8,
        confirmPassword:password !== confirmPassword,
      });
      return;
    }
    if (Seller && hexadecimalCode.trim() === '') {
      console.log("HexadecimalCode validation error");
      setErrors(prevErrors => ({
          ...prevErrors,
          hexadecimalCode: true
      }));
      return;
  }
  
    const data = {
      name: name,
      email: email,
      password: password,
      phoneNumber: phoneNumber,
    };
   
    const datalogin = {
      email: email,
      password: password
    }
    try {

      let response;
      if (Seller) {
        response = await api.post('/api/sellers', data);
        SetExistingUser(false);
      } else {
        SetErrorLogin(false)
        SetExistingUser(false);
        response = await api.post('/api/customers', data);
      }
      const Login = await api.post('/api/auth/login', datalogin);
      
      if (Login.status === 200) {
        login(Login.data)
      }

    } catch (error) {
      if (error.response && error.response.status === 409) {
        SetExistingUser(true);
      } else {
        SetErrorLogin(true)
        console.error('Error:', error.message);
      }
      
    }
    
  }

  return (
      <>
      <Navbar/> 
      <div className="signup" >
      <div className="Register">
        <div className="Welcome" style={{ backgroundColor: theme === "light" ? "" : "#a5351ca4" }}>
          <h3>¡Welcome to Family Market's online registration</h3>
          <p>Sign up now to access our wide selection of fresh, quality products. It's time to simplify your online shopping with Family Market!</p>
          <p>If you already have an account:</p>
          <Link to="/signin" type="button">Sign In</Link>
        </div>

        <div className="form-register" style={{ backgroundColor: theme === "light" ? "#f1f1f1" : "#333333" }}>
          <h2 className="account">Create an account</h2>
          <form onSubmit={handleSubmit}>
            <div className="mb-3">
              <label htmlFor="name" className="form-label">Name</label>
              <input type="text" className={`form-control ${errors.name ? 'is-invalid' : ''}`} id="name" name="name" value={formData.name} onChange={handleChange} />
              {errors.name && <p className='Error'>Set a first name</p>}
            </div>
            <div className="mb-3">
              <label htmlFor="phoneNumber" className="form-label">Phone Number</label>
              <input type="text" className={`form-control ${errors.phoneNumber ? 'is-invalid' : ''}`} id="phoneNumber" name="phoneNumber" value={formData.phoneNumber} onChange={handleChange} />
              {errors.phoneNumber && <p className='Error'>Phone number must be less than or equal to 10 digits</p>}
            </div>
            <div className="mb-3">
              <label htmlFor="email" className="form-label">Email</label>
              <input type="email" className={`form-control ${errors.email ? 'is-invalid' : ''}`} id="email" name="email" value={formData.email} onChange={handleChange} />
              {errors.email && <p className='Error'>Set an email</p>}
            </div>
            <div className="mb-3" style={{ position: 'relative' }}>
              <label htmlFor="password" className="form-label">Password</label>
              <input type={showPassword ? "text" : "password"} className={`form-control ${errors.password ? 'is-invalid' : ''}`} id="password" name="password" value={formData.password} onChange={handleChange} />
              <FontAwesomeIcon icon={showPassword ? faEye : faEyeSlash} className="icon-register" onClick={() => setShowPassword(!showPassword)} />
              {errors.password && <p className='Error'>Minimum of 8 characters</p>}
            </div>
            <div className="mb-3">
              <label htmlFor="confirmPassword" className="form-label">Confirm Password</label>
              <input type={showPassword ? "text" : "password"} className={`form-control ${errors.confirmPassword ? 'is-invalid' : ''}`} id="confirmPassword" name="confirmPassword" value={formData.confirmPassword} onChange={handleChange} />
              {errors.confirmPassword && <p className='Error'>Passwords do not match</p>}
            </div>
            {Seller && (
              <div>
                <label htmlFor="hexadecimalCode" className="form-label">Seller code</label>
                <input type="text" id="hexadecimalCode" name="hexadecimalCode"  className={`form-control ${errors.hexadecimalCode ? 'is-invalid' : ''}`} value={formData.hexadecimalCode} onChange={handleChange} />
                {errors.hexadecimalCode && <p className='Error'>Set a seller code</p>}
              </div>
            )}
            {ExistingUser && <p className="Error-Login">This user already exists</p>}
            {ErrorLogin && <p className="Error-Login">error when logging in </p>}
            <div className="mb-3 d-flex align-items-center">
              <button type="button" onClick={handleSubmit} className="Button-SignUp">Sign Up</button>
              <button type="button"  className="Buttom-Seller" onClick={() => (SetSeller(!Seller))}>
                {Seller ? 'I want to be a client' : 'I want to be a salesman'}
              </button>            
            </div>
          </form>
        </div>
      </div>
      {user ? <Navigate to="/" /> : null}
      </div>
      <Footer />
    </>
  );
}

export default SignupUser;
