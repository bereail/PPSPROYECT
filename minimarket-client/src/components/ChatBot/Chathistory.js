import React from 'react';
import './Chathistory.css';

const Chathistory = ({ chats }) => {
    console.log('Chats received:', chats);

    
    const validChats = chats.filter(chat => chat && chat.name && chat.email && chat.query);

    console.log('Valid chats:', validChats); 

    return (
        <div className="chathistory">
            <h3>Chat History</h3>
            {validChats.length > 0 ? (
                <ul>
                    {validChats.map((chat, index) => (
                        <li key={index}>
                            <strong>Name:</strong> {chat.name}<br />
                            <strong>Email:</strong> {chat.email}<br />
                            <strong>Query:</strong> {chat.query}
                        </li>
                    ))}
                </ul>
            ) : (
                <p>No valid chat history available.</p>
            )}
        </div>
    );
};

export default Chathistory;









