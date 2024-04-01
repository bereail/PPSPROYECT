import React, { useState, useEffect } from "react";
import axios from 'axios';
import CustomNavbar from "../../Navbar/CustomNavbar";
import Footer from "../../Footer/footer";
import "./SignIn.css"
const Signin = () => {
  const [email, setEmail] = useState('');
  const [pass, setPass] = useState('');
  const [Error, SetError] = useState('')

  useEffect(() => {
    SetError(0);
  }, []);
  const handleEmailChange = (e) => {

    setEmail(e.target.value);
  }

  const handlePassChange = (e) => {

    setPass(e.target.value);
  }

  const handleSubmit = (e) => {
    
  
    e.preventDefault();
    const data = {
      Email: email,
      Pass: pass,
    };
    //LA VALIDACION VA ANTES QUE SE MANDE A LA BASE DE DATOS VER DONDE
    if(email == ''){
      SetError(1);
      return;
    }
    if(pass === ''){
      SetError(1);
      return;
    }
    
    const url = ""; // Add your API endpoint URL for login
    axios.post(url, data)
      .then((result) => {
        alert('Login successful');
      })
      .catch((error) => {
        alert('Error logging in.');
      });
  }

  return (
    <div className="login" >
      <CustomNavbar/>
      <div className="Form-Login">
        <h2>Login</h2>
      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label htmlFor="exampleInputEmail1" className="form-label">Email address</label>
          {Error === 1 && email === '' && <p className='Error'>Set a Email</p>}
          <input type="email" className="form-control" id="exampleInputEmail1" aria-describedby="emailHelp" onChange={handleEmailChange} />
          <div id="emailHelp" className="form-text">We'll never share your email with anyone else.</div>
        </div>
        <div className="mb-3">
          <label htmlFor="exampleInputPassword1" className="form-label">Password</label>
          <input type="password" className="form-control" id="exampleInputPassword1" onChange={handlePassChange} />
          {Error === 1 && pass === '' && <p className='Error'>Set a password</p>}
        </div>
        <div className="mb-3 form-check">
          <input type="checkbox" className="form-check-input" id="exampleCheck1" />
          <label className="form-check-label" htmlFor="exampleCheck1">Check me out</label>
        </div>
        <button type="submit" className="btn btn-primary">Submit</button>
      </form>
      </div>
      <Footer/>
    </div>
  );
}

export default Signin;