import React, { useState, useEffect, useContext } from 'react';
import ChatBot from 'react-simple-chatbot';
import { ThemeProvider } from 'styled-components';
import './ChatBot.css';
import { AuthContext } from '../Context/AuthContext'; // Import AuthContext

const theme = {
  background: '#f5f8fb',
  headerBgColor: '#eb3449',
  headerFontColor: '#fff',
  headerFontSize: '20px',
  botBubbleColor: '#eb3449',
  botFontColor: '#000',
  userBubbleColor: '#0cb3c9',
  userFontColor: '#000',
};

const CustomChatbot = () => {
  const [isOpen, setIsOpen] = useState(false);
  const [chatHistory, setChatHistory] = useState([]);
  const { role } = useContext(AuthContext); // Access role from AuthContext

  const toggleChatbot = () => {
    setIsOpen(!isOpen);
  };

  const validateEmail = (value) => {
    const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return regex.test(value);
  };

  const steps = [
    // ... your existing chatbot steps
  ];

  // Conditionally render the chatbot based on user role
  return (
    <div className='chatbot-container'>
      {role !== 'SuperAdmin' && (
        <>
          <button
            className={`chatbot-toggle-button ${isOpen ? 'open' : ''}`}
            onClick={toggleChatbot}
          >
            <svg
              height='24'
              viewBox='0 0 24 24'
              width='24'
              xmlns='http://www.w3.org/2000/svg'
            >
              <path d='M20 2H4c-1.1 0-2 .9-2 2v18l4-4h14c1.1 0 2-.9 2-2V4c0-1.1-.9-2-2-2z'></path>
              <path d='M0 0h24v24H0z' fill='none'></path>
            </svg>
          </button>
          {isOpen && (
            <ThemeProvider theme={theme}>
              <ChatBot
                headerTitle='Chat With Us'
                recognitionEnable={true}
                recognitionThreshold={0.5}
                steps={steps}
                floating={true}
              />
            </ThemeProvider>
          )}
        </>
      )}
    </div>
  );
};

export default CustomChatbot;






