import "bootstrap/dist/css/bootstrap.min.css";
import React, { useState,useContext } from 'react';
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faFacebook,
  faTwitter,
  faInstagram,
} from "@fortawesome/free-brands-svg-icons";
import { useNavigate } from 'react-router-dom';
import { Link } from 'react-router-dom';
import "./footer.css";
import { AuthContext } from '../Context/AuthContext';


const Footer = () => {
  const navigate = useNavigate();
  const [email, setEmail] = useState('');
  const [subscribedEmails, setSubscribedEmails] = useState(
    JSON.parse(localStorage.getItem('subscribedEmails')) || []
  );
  const { role } = useContext(AuthContext); 

  const handleFAQsClick = () => {
    navigate('/faqs');
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if (email) {
      const updatedEmails = [...subscribedEmails, email];
      setSubscribedEmails(updatedEmails);
      localStorage.setItem('subscribedEmails', JSON.stringify(updatedEmails));
      setEmail('');
    }
  };

  return (
    <footer className="footer mt-auto py-3">
      {role !== 'SuperAdmin' && (

      <div className="container">
        <div className="row">
          <div className="col-md-6 d-flex flex-column justify-content-center align-items-center mb-3 mb-md-0">
            <div className="d-flex flex-column align-items-start mb-4">
              <span className="mb-2">Find us:</span>
              <a href="#" className="social-link">
                <FontAwesomeIcon
                  icon={faFacebook}
                  className="social-icon"
                  size="2x"
                />
                <span className="social-name">Facebook</span>
              </a>
              <a href="#" className="social-link">
                <FontAwesomeIcon
                  icon={faTwitter}
                  className="social-icon"
                  size="2x"
                />
                <span className="social-name">Twitter</span>
              </a>
              <a href="#" className="social-link">
                <FontAwesomeIcon
                  icon={faInstagram}
                  className="social-icon"
                  size="2x"
                />
                <span className="social-name">Instagram</span>
              </a>
            </div>
          </div>

          <div className="faqs-button">
            <Link to="/faqs" className="faqs-link">FAQs</Link>
          </div>
          <div className="newsletter">
            <h3>Subscribe to our newsletter</h3>
            <form onSubmit={handleSubmit}>
              <input
                type="email"
                placeholder="Enter your email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                required
              />
              <button type="submit">Subscribe</button>
            </form>
          </div>

          <div className="col-md-6 d-flex flex-column justify-content-center align-items-center">
            <div className="w-100">
              <span className="d-block mb-2">Maps:</span>
              <iframe
                title="Google Maps"
                src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d26051.4642513549!2d-60.66689535066436!3d-32.94404496793378!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x95b43d0403eb0c61%3A0xa6180c65b9cb41fd!2sRosario%2C%20Santa%20Fe!5e0!3m2!1sen!2sar!4v1649247419818!5m2!1sen!2sar"
                width="100%"
                height="200"
                style={{ border: 0 }}
                allowFullScreen=""
                loading="lazy"
              ></iframe>
            </div>
          </div>
        </div>
        <div className="row">
          <div className="col">
            <div className="text-muted text-center mt-3">
              © 2024 Family Market
            </div>
          </div>
        </div>
      </div>
       )}
       </footer>
      
  );
};

export default Footer;

