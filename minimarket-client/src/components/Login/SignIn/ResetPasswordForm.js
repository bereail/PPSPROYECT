import React, { useContext, useState } from 'react';
import { useSearchParams,Link } from 'react-router-dom';
import api from "../../../api";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';
import zxcvbn from 'zxcvbn';
import './ResetPasswordForm.css';
import { AuthContext } from '../../Context/AuthContext';

const ResetPasswordForm = () => {
  const [searchParams] = useSearchParams();
  const [newPassword, setNewPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [message, setMessage] = useState('');
  const [error, setError] = useState('');
  const [passwordStrength, setPasswordStrength] = useState(null);
  const [showPassword, setShowPassword] = useState(false);
  const [showConfirmPassword, setShowConfirmPassword] = useState(false);
  const token = searchParams.get('token');
   const {logout} = useContext(AuthContext)
  const handleNewPasswordChange = (e) => {
    const password = e.target.value;
    setNewPassword(password);
    const strength = zxcvbn(password);
    setPasswordStrength(strength);
  };

  const togglePasswordVisibility = () => {
    setShowPassword(!showPassword);
  };

  const toggleConfirmPasswordVisibility = () => {
    setShowConfirmPassword(!showConfirmPassword);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');
    setMessage('');

    if (newPassword !== confirmPassword) {
      setError('Passwords do not match.');
      return;
    }

    const data = {
      password: newPassword,
      confirmPassword: confirmPassword
    };

    try {
      const response = await api.put('/api/users/profile/password', data, {
        headers: {
          'Authorization': `Bearer ${token}`
        }
      });

      if (response.status === 200) {
        setMessage('Your password has been reset successfully.');
        logout()
      } else {
        setError('Password reset failed.');
      }
    } catch (err) {
      setError('Password reset failed.');
    }
  };

  const renderPasswordStrength = () => {
    if (!passwordStrength) return null;
    const { score } = passwordStrength;
    const scoreText = ['Weak', 'Weak', 'Medium', 'Strong', 'Very Strong'];
    const scoreColor = ['red', 'orange', 'yellow', 'green', 'darkgreen'];

    return (
      <div className="password-strength-bar">
        <div className="password-strength-bar-inner" style={{ width: `${(score + 1) * 20}%`, backgroundColor: scoreColor[score] }}>
          <span>{scoreText[score]}</span>
        </div>
      </div>
    );
  };

  return (
    <div className="reset-password-form-container">
      <div className="reset-password-form-content">
        <h2>Get a new Password</h2>

        <form onSubmit={handleSubmit}>
          <div className="mb-3">
            <label htmlFor="newPassword" className="form-label">New Password</label>
            <div className="password-input">
              <input 
                type={showPassword ? "text" : "password"}
                className="form-control"
                id="newPassword"
                value={newPassword}
                onChange={handleNewPasswordChange}
              />
              <FontAwesomeIcon
                icon={showPassword ? faEyeSlash : faEye}
                onClick={togglePasswordVisibility}
                className="password-toggle-icon"
              />
            </div>
            {renderPasswordStrength()}
          </div>
          <div className="mb-3">
            <label htmlFor="confirmPassword" className="form-label">Confirm New Password</label>
            <div className="password-input">
              <input
                type={showConfirmPassword ? "text" : "password"}
                className="form-control"
                id="confirmPassword"
                value={confirmPassword}
                onChange={(e) => setConfirmPassword(e.target.value)}
              />
              <FontAwesomeIcon
                icon={showConfirmPassword ? faEyeSlash : faEye}
                onClick={toggleConfirmPasswordVisibility}
                className="password-toggle-icon"
              />
            </div>
          </div> 
          {error && <p className="error">{error}</p>}
          {message && <p className="message">{message}</p>}
          <button type="submit" className="button-reset">Submit</button>
        </form>
        <Link to="/" className="button-home">Home</Link>
      </div>
    </div>
  );
};

export default ResetPasswordForm;




