import React, { useEffect, useState } from "react";
import axios from 'axios';
import CustomNavbar from "../../Navbar/CustomNavbar";
import Footer from "../../Footer/footer";
import './SignUpUser.css';
import { Link } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEyeSlash, faEye } from "@fortawesome/free-solid-svg-icons";
import { Navigate } from "react-router-dom";
import Api from "../../../Api";
const SignupUser = () => {
  const [showPassword, setShowPassword] = useState(false);
  const [ExistingUser, SetExistingUser] = useState(false)
  const [formData, setFormData] = useState({
    name: '',
    email: '',
    password: '',
    confirmPassword: '',
    phoneNumber: '',
    address: '',
    hexadecimalCode: '' 
  });
  const [errors, setErrors] = useState({
    name: false,
    address: false,
    phoneNumber: false,
    email: false,
    password: false,
    confirmPassword: false, 
    hexadecimalCode: false
  });
  const [Seller, SetSeller] = useState(false);
  const [LoggedIn, setLoggedIn] = useState(false);


  useEffect(()=>{
    const logged = window.localStorage.getItem('LoggedUser')
    if(logged){
      setLoggedIn(true);
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
 
    const { name, address, phoneNumber, email, password, confirmPassword, hexadecimalCode  } = formData;
   
    if (phoneNumber.trim().length > 10) {
      setErrors(prevErrors => ({
        ...prevErrors,
        phoneNumber: true
      }));
      return;
    }

    if (name.trim() === '' || address.trim() === '' || phoneNumber.trim() === '' || email.trim() === '' || password.trim() === '' || confirmPassword.trim() === '') {
      setErrors({
        name: name.trim() === '',
        address: address.trim() === '',
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
      address: address
    };
   
    const datalogin = {
      email: email,
      password: password
    }
    if (Seller) {
       data.hexadecimalCode = formData.hexadecimalCode;
       console.log(data);
       try {
        const response = await Api.post('/api/sellers', data);
        SetExistingUser(false);
        console.log('Usuario:', response.data);

      } catch (error) {
        SetExistingUser(true)
        console.error('Error:', error.message);
      }
    }
    try {
      const api = Api();
      let response;
      if (Seller) {
        response = await api.post('/api/sellers', data);
      } else {
        response = await api.post('/api/customers', data);
      }
      SetExistingUser(false);
      console.log('Usuario:', response.data);
      const Login = await api.post('/api/auth/login', datalogin);

      if (Login.status === 200) {
        window.localStorage.setItem('LoggedUser', JSON.stringify(Login.data));
        setLoggedIn(true);
      }
    } catch (error) {
        SetExistingUser(true);
        console.error('Error:', error.message);
      
    }
    
  }

  return (

    <div className="signup">
      <CustomNavbar /> 
      <div className="Register">
        <div className="Welcome">
          <h3>¡Welcome to Family Market's online registration</h3>
          <p>Sign up now to access our wide selection of fresh, quality products. It's time to simplify your online shopping with (MARCA)!</p>
          <p>If you already have an account:</p>
          <Link to="/signin" type="button">Sign In</Link>
        </div>

        <div className="form-register">
          <h2 className="account">Create an account</h2>
          <form onSubmit={handleSubmit}>
            <div className="mb-3">
              <label htmlFor="name" className="form-label">Name</label>
              <input type="text" className={`form-control ${errors.name ? 'is-invalid' : ''}`} id="name" name="name" value={formData.name} onChange={handleChange} />
              {errors.name && <p className='Error'>Set a first name</p>}
            </div>
            <div className="mb-3">
              <label htmlFor="address" className="form-label">Address</label>
              <input type="text" className={`form-control ${errors.address ? 'is-invalid' : ''}`} id="address" name="address" value={formData.address} onChange={handleChange} />
              {errors.address && <p className='Error'>Set an address</p>}
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
            <div className="mb-3 d-flex align-items-center">
              <button type="submit" className="btn btn-primary">Sign Up</button>
              <p style={{ marginLeft: '120px', color: '#6893e2' }} onClick={() => (SetSeller(!Seller))}>
                {Seller ? 'I want to be a client' : 'I want to be a salesman'}
              </p>            
            </div>
          </form>
          {ExistingUser && <p className="Error">This user already exists</p>}
        </div>
      </div>
      {LoggedIn ? <Navigate to="/" /> : null}
      <Footer />
    </div>

  );
}

export default SignupUser;
