import React, { useState, useContext, useEffect } from 'react';
import ChatBot from 'react-simple-chatbot';
import { AuthContext } from '../Context/AuthContext'; 
import './ChatBot.css';


const CustomChatbot = () => {
  const initialSteps = [
    {
      id: '1',
      message: 'Hello, what is your name?',
      trigger: 'name',
    },
    {
      id: 'name',
      user: true,
      trigger: '3',
    },
    {
      id: '3',
      message: 'Hi {previousValue}! How can we assist you today?',
      trigger: 'query',
    },
    {
      id: 'query',
      user: true,
      trigger: 'email',
    },
    {
      id: 'email',
      message: 'Could you please provide your email address so we can follow up?',
      trigger: 'user_email',
    },
    {
      id: 'user_email',
      user: true,
      validator: (value) => {
        const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (regex.test(value)) {
          return true;
        } else {
          return 'Please enter a valid email address.';
        }
      },
      trigger: 'thank_you',
    },
    {
      id: 'thank_you',
      message: 'Thank you for contacting us! We will follow up with your query shortly.',
      end: true,
    },
  ];

  const [isOpen, setIsOpen] = useState(false);
  const [steps, setSteps] = useState(initialSteps);
  const [chatData, setChatData] = useState({});
  const [chats, setChats] = useState([]);
  const { role } = useContext(AuthContext);

  useEffect(() => {
    const savedChats = JSON.parse(localStorage.getItem('chats')) || [];
    setChats(savedChats);
  }, []);

  const handleEnd = ({ steps, values }) => {
    const chatData = {
      name: values[0],
      query: values[1],
      email: values[2],
      date: new Date().toLocaleString(),
    };
    saveChatToLocalStorage(chatData);
  };

  const saveChatToLocalStorage = (chatData) => {
    const updatedChats = [...chats, chatData];
    localStorage.setItem('chats', JSON.stringify(updatedChats));
    setChats(updatedChats);  
  };

  const toggleChatbot = () => {
    if (isOpen) {
      setSteps(initialSteps); 
    }
    setIsOpen(!isOpen);
  };

  if (role === 'SuperAdmin') {
    return null; // Si el usuario es SuperAdmin, no se renderiza el componente
  }
  return (
    <div className='chatbot-container'>

      
      <button
        className={`chatbot-toggle-button ${isOpen ? 'open' : ''}`}
        onClick={toggleChatbot}
        aria-label="Toggle chatbot"
      >
        <svg height="24" viewBox="0 0 24 24" width="24" xmlns="http://www.w3.org/2000/svg">
          <path d="M20 2H4c-1.1 0-2 .9-2 2v18l4-4h14c1.1 0 2-.9 2-2V4c0-1.1-.9-2-2-2z"></path>
          <path d="M0 0h24v24H0z" fill="none"></path>
        </svg>
      </button>
      {isOpen && (
        <ChatBot
          key={isOpen ? 'open' : 'closed'} 
          headerTitle="ChatBot"
          recognitionEnable={true}
          recognitionThreshold={0.5}
          steps={steps}
          handleEnd={handleEnd} // Agrega handleEnd para capturar los datos del chat al finalizar
          floating={true}
        />
      )}

      {/* Mostrar historial de chat */}
      
    </div>
  );
};

export default CustomChatbot;


