import React from "react";

const LoginPopup = ({ showPopup }) => (
  <div className={`Time-window ${showPopup ? "show" : ""}`} aria-live="polite">
    <p>Successfully logged in!</p>
  </div>
);

export default LoginPopup;
