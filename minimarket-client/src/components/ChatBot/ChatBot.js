import React, { useState } from 'react';
import ChatBot from 'react-simple-chatbot';
import { ThemeProvider } from 'styled-components';
import './ChatBot.css';

const theme = {
    background: '#f5f8fb',
    headerBgColor: '#915036',
    headerFontColor: '#fff',
    headerFontSize: '20px',
    botBubbleColor: '#915036',
    botFontColor: '#000', // Texto del bot en negro
    userBubbleColor: '#99776a',
    userFontColor: '#000', // Texto del usuario en negro
};

const CustomChatbot = () => {
    const steps = [
        { id: "1", message: "Hey welcome to Family Market", trigger: "2" },
        { id: "2", user: true, trigger: "3" },
        { id: "3", message: "¡{previousValue}! How can I help you today?", end: true }
    ];

    const [isOpen, setIsOpen] = useState(false);

    const toggleChatbot = () => {
        setIsOpen(!isOpen);
    };

    return (
        <div className='chatbot-container'>
            <a className={`chatbot-toggle-button ${isOpen ? 'open' : ''}`} onClick={toggleChatbot}>
                <svg height="24" viewBox="0 0 24 24" width="24" xmlns="http://www.w3.org/2000/svg">
                    <path d="M20 2H4c-1.1 0-2 .9-2 2v18l4-4h14c1.1 0 2-.9 2-2V4c0-1.1-.9-2-2-2z"></path>
                    <path d="M0 0h24v24H0z" fill="none"></path>
                </svg>
            </a>
            {isOpen && (
                <ThemeProvider theme={theme}>
                    <ChatBot
                        headerTitle="ChatBot"
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
