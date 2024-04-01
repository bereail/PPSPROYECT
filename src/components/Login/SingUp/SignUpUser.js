import React, { useState, useEffect } from "react";


import axios from 'axios';
import CustomNavbar from "../../Navbar/CustomNavbar";
import Footer from "../../Footer/footer";
import './SignUpUser.css'
import { Link } from "react-router-dom";


const SignupUser = () => {

  const [formData, setFormData] = useState({
    firstName: '',
    lastName: '',
    email: '',
    password: '',
    confirmPassword: ''
  });
  const [Error, SetError] = useState(1);
  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData(prevState => ({
      ...prevState,
      [name]: value
    }));
    
  }

  useEffect(() => {
    SetError(0);
  }, []);
  const handleSubmit = (e) => {
    e.preventDefault();
    const { firstName, lastName, email, password, confirmPassword } = formData;
  
    //LA VALIDACION VA ANTES QUE CARGUE A LA ABSE DE DATOS NOC DONDE SE HACE
    switch (true) {
      case password === '':
        SetError(1);
        return;
      case firstName === '':
        SetError(1);
        return;
      case firstName !== confirmPassword:
        SetError(1);
        return;
      case lastName === '':
        SetError(1);
        return;
      case email === '':
        SetError(1);
        return;
      default:

        break;
    }
    const data = {
      firstName: firstName,
      lastName: lastName,
      email: email,
      password: password
    };
    const url = ""; 
    axios.post(url, data)
      .then((result) => {
        alert('Registration successful');
      })
      .catch((error) => {
        alert('Error registering user.');
      });
  }

  return (
    <div className="signup">
      <CustomNavbar></CustomNavbar>
    
      <div className="Register">
      <div className="Welcome">
      <h3 >¡Welcome to (MARCA)'s online registration</h3>
      <p>Sign up now to access our wide selection of fresh, quality products. It's time to simplify your online shopping with (MARCA)!</p>
      <p>If you already have an account:</p>
      <Link to="/signin" type="button" >Sing In</Link>      
      </div>

      
      <div className="form-register">
      <h2 className="account">Create an account</h2>

      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label htmlFor="firstName" className="form-label">First Name</label>
          <input type="text" className="form-control" id="firstName" name="firstName" value={formData.firstName} onChange={handleChange} />
          {Error === 1 && formData.firstName == '' &&<p className='Error'>Set a firstName</p>}
        </div>
        <div className="mb-3">
          <label htmlFor="lastName" className="form-label">Last Name</label>
          <input type="text" className="form-control" id="lastName" name="lastName" value={formData.lastName} onChange={handleChange} />
          {Error ===1 && formData.lastName == '' &&<p className='Error'>Set a LastName</p>}
        </div>
        <div className="mb-3">
          <label htmlFor="email" className="form-label">Email</label>
          <input type="email" className="form-control" id="email" name="email" value={formData.email} onChange={handleChange} />
          {Error === 1 && formData.email =='' &&<p className='Error'>Set a Email</p>}
       </div>
        <div className="mb-3">
          <label htmlFor="password" className="form-label">Password</label>
          <input type="password" className="form-control" id="password" name="password" value={formData.password} onChange={handleChange} />
          {Error === 1 && formData.password =='' &&<p className='Error'>Set a password</p>}
          {Error === 1 && formData.password !== formData.confirmPassword && formData.password !== '' &&<p className='Error'>Passwords do not match</p>}
        </div>
        <div className="mb-3">
          <label htmlFor="confirmPassword" className="form-label">Confirm Password</label>
          <input type="password" className="form-control" id="confirmPassword" name="confirmPassword" value={formData.confirmPassword} onChange={handleChange} />
        
        </div>
        <button type="submit" className="btn btn-primary">Sign Up</button>
      </form>
      </div>
      </div>
      <Footer/>
    </div>
  );
}

export default SignupUser;
