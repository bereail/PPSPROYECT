import React from 'react';
import './footer.css'
const Footer = () => {
  return (
    <footer class="footer"  >
      <div className="container">
        <div className="row">
          <div className="col-md-6">
            <h3>Contact Us</h3>
            <p>Email: info@example.com</p>
            <p>Phone: +1 234 567 890</p>
          </div>
          <div className="col-md-6">
            <h3>Connect with Us</h3>
            <ul>
              <li>Leave a Comment</li>
              <li>Follow us on Social Media</li>
              <li>Chat with Us</li>
            </ul>
          </div>
        </div>
        <div className="row">
          <div className="col-md-12">
            <p className="text-center">&copy; 2024 Your Company. All rights reserved.</p>
          </div>
        </div>
      </div>
    </footer>
  );
}

export default Footer;

