import React from 'react';
import './Chathistory.css';

const Chathistory = ({ chats }) => {

    const validChats = chats.filter(chat => chat && chat.name && chat.email && chat.query);

    return (
        <div className="chathistory">
            <h3>Chat History</h3>
            {validChats.length > 0 ? (
                <ul>
                    {validChats.map((chat, index) => (
                        <li key={index} className="chat-item">
                            <div className="chat-header">
                                <p><strong>Name:</strong> {chat.name}</p>
                                <p><strong>Email:</strong> {chat.email}</p>
                            </div>
                            <div className="chat-query">
                                <p><strong>Query:</strong> {chat.query}</p>
                            </div>
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










