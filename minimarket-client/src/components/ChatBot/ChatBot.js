import React, { useState, useEffect } from 'react';
import ChatBot from 'react-simple-chatbot';
import { ThemeProvider } from 'styled-components';
import './ChatBot.css';

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

  const toggleChatbot = () => {
    setIsOpen(!isOpen);
  };

  const validateEmail = (value) => {
    const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return regex.test(value);
  };

  const steps = [
    {
      id: '1',
      message: "Hey, welcome to Family Market. What's your name?",
      trigger: 'get_name',
    },
    {
      id: 'get_name',
      user: true,
      trigger: ({ value }) => {
        return 'greet_user';
      },
    },
    {
      id: 'greet_user',
      message: ({ previousValue }) =>
        `Nice to meet you, ${previousValue}! Can I have your email address for further assistance?`,
      trigger: 'get_email',
    },
    {
      id: 'get_email',
      user: true,
      validator: (value) => {
        if (validateEmail(value)) {
          return true;
        } else {
          return 'Please enter a valid email address.';
        }
      },
      trigger: 'ask_help',
    },
    {
      id: 'ask_help',
      message: 'How can I help you today?',
      trigger: 'get_help',
    },
    {
      id: 'get_help',
      user: true,
      trigger: 'end_chat',
    },
    {
      id: 'end_chat',
      message:
        'Thank you for contacting us. We learn every day to offer you a better service! Thank you for your trust in Family Market.',
      end: true,
      trigger: () => {
        const timestamp = new Date().toISOString();
        const chatItem = { id: Date.now(), values: chatHistory, timestamp };
        const updatedChatHistory = [...chatHistory, chatItem];
        setChatHistory(updatedChatHistory);
        localStorage.setItem('chatHistory', JSON.stringify(updatedChatHistory));
      },
    },
  ];

  return (
    <div className='chatbot-container'>
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
    </div>
  );
};

export default CustomChatbot;





