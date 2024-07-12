import React, { useState } from 'react';
import './ResetPassword.css';
import api from "../../../api";
import { toast } from 'react-toastify';

const ResetPassword = () => {
  const [email, setEmail] = useState('');
  const [confirmEmail, setConfirmEmail] = useState('');
 

  const handleEmailChange = (e) => {
    setEmail(e.target.value);
  };

  const handleConfirmEmailChange = (e) => {
    setConfirmEmail(e.target.value);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
   

    if (!email || !confirmEmail) {
      toast.error('Please enter your email address and confirm it.');
      return;
    }

    if (email !== confirmEmail) {
      toast.error('Email addresses do not match. Please try again.');
      return;
    }

    const data = {
      email: email,
      confirmEmail:confirmEmail
    };

    try {
      const response = await api.post('/api/auth/recovery', data);
      console.log( response);
      if (response.status === 200) {
        toast.success('A link to reset your password has been sent to your email.');
      } else if (response.status === 404) {
        toast.error('Email not found. Please check your email address and try again.');
      }
    } catch (error) {
      if (error.response && error.response.status === 404) {
        toast.error('Email not found. Please check your email address and try again.');
      } else {
        toast.error('An error occurred. Please try again.');
      }
    };
  };

  return (
    <div className="reset-password-modal">
      <div className="reset-password-content">
        <h2>Reset Password</h2>
        <form onSubmit={handleSubmit}>
          <div className="mb-3">
            <label htmlFor="resetEmail" className="form-label">Email address</label>
            <input
              type="email"
              className="form-control"
              id="resetEmail"
              value={email}
              onChange={handleEmailChange}
              required
            />
          </div>
          <div className="mb-3">
            <label htmlFor="confirmEmail" className="form-label">Confirm Email address</label>
            <input
              type="email"
              className="form-control"
              id="confirmEmail"
              value={confirmEmail}
              onChange={handleConfirmEmailChange}
              required
            />
          </div>
          
          <button type="submit" className="button-reset">Submit</button>
        </form>
        <button className="button-close" onClick={() => window.location.reload()}>Close</button>
      </div>
    </div>
  );
};

export default ResetPassword;


