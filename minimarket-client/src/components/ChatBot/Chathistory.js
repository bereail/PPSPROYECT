import React from 'react';

const Chathistory = ({ userName, userEmail, userQuery }) => {
    return (
        <div>
            <h2>Chat History</h2>
            <p>Name: {userName}</p>
            <p>Email: {userEmail}</p>
            <p>User Query: {userQuery}</p>
            {/**/}
            {/* Por ejemplo: */}
            <ul>
                <li>Mensaje 1</li>
                <li>Mensaje 2</li>
                
            </ul>
        </div>
    );
};

export default Chathistory;


