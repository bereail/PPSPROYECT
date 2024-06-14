import React, { useContext, useState, useEffect } from 'react';
import Footer from '../Footer/footer';
import imageuser from '../Image/User.png';
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPencil, faXmark } from "@fortawesome/free-solid-svg-icons";
import './User.css';
import UserGetOrders from './Crud/UserGetOrders';
import ModifyUser from './Crud/ModifyUser';
import { ThemeContext } from '../Context/ThemeContext';
import Navbar from '../Navbar/Navbar';
import GetUserbyid from './Crud/GetUserbyid';
import DeleteUser from './Crud/DeleteUser';
import { AuthContext } from '../Context/AuthContext';
import AddAdressUser from './Crud/AddAdressUser';
import AdminManagement from '../Admin/AdminManagement';
import Chathistory from '../ChatBot/Chathistory';

const User = () => {
    const { theme } = useContext(ThemeContext);
    const { role } = useContext(AuthContext);
    const [activeButton, setActiveButton] = useState('');
    const [emails, setEmails] = useState([]);
    const [showEmails, setShowEmails] = useState(false);
    const [showChathistory, setShowChathistory] = useState(false);
    const user = GetUserbyid();

    useEffect(() => {
        const storedEmails = JSON.parse(localStorage.getItem('subscribedEmails')) || [];
        setEmails(storedEmails);
    }, []); 

    useEffect(() => {
        const storedShowChathistory = localStorage.getItem('showChathistory') === 'true';
        setShowChathistory(storedShowChathistory);
    }, []); 

    useEffect(() => {
        localStorage.setItem('showChathistory', JSON.stringify(showChathistory));
    }, [showChathistory]);

    const handleExit = () => {
        setActiveButton('');
    };

    return (
        <div>
            <Navbar />
            <div className='User' style={{ alignItems: 'center' }}>
                <div className='Userdetails' style={{ backgroundColor: theme === "light" ? "" : "#a5351ca4" }}>
                    <img src={imageuser} alt="User" className="user-image" />
                    <h3>Welcome {user && user.name}!</h3>

                    <button onClick={() => setActiveButton('Profile')}
                        className={activeButton === "Profile" ? "User-filter-button active" : ''}> Profile</button>
                    <button onClick={() => setActiveButton('Orders')}
                        className={activeButton === "Orders" ? "User-filter-button active" : ''}>Orders</button>
                    <button onClick={() => setActiveButton('Add Address')}>Address</button>

                    {role === 'SuperAdmin' && (
                        <>
                            <h2>Subscribed</h2>
                            <button onClick={() => setShowEmails(!showEmails)}>
                                {showEmails ? 'Hide Emails' : 'Show Registered Emails'}
                            </button>
                            {showEmails && (
                                <ul>
                                    {emails.map((email, index) => (
                                        <li key={index}>{email}</li>
                                    ))}
                                </ul>
                            )}
                            <button onClick={() => setShowChathistory(!showChathistory)} style={{ marginTop: "20px" }}>
                                {showChathistory ? 'Hide Chat History' : 'Show Chat History'}
                            </button>
                            {showChathistory && (
                                <div className="chat-history-container">
                                    <Chathistory /> {/* Aquí renderiza el componente de historial de chat */}
                                </div>
                            )}

                            <button onClick={() => setActiveButton('AdminProperties')} style={{ marginTop: "20px" }}
                                className={activeButton === "AdminProperties" ? "User-filter-button active" : ''}>Admin Properties</button>
                        </>
                    )}

                    {role !== 'SuperAdmin' && ( // Renderiza el botón de Chatbot solo si no es SuperAdmin
                        <button onClick={() => setShowChathistory(!showChathistory)} style={{ marginTop: "20px" }}>
                            {showChathistory ? 'Hide Chat History' : 'Show Chat History'}
                        </button>
                    )}

                    <button onClick={() => setActiveButton('Delete Account')} style={{ marginTop: "20px" }}
                        className={activeButton === "Delete Account" ? "User-filter-button active" : ''}>Delete Account</button>
                </div>
                <div>
                    {activeButton === 'Profile' && <ModifyUser />}
                    {activeButton === 'Orders' && <UserGetOrders />}
                    {activeButton === 'Add Address' && <AddAdressUser />}
                    {activeButton === 'AdminProperties' && role === 'SuperAdmin' && (
                        <div className="admin-properties">
                            <p>Administrative settings and options go here.</p>
                            <AdminManagement />
                        </div>
                    )}
                    {activeButton === 'Delete Account' && <DeleteUser handleExit={handleExit} />}
                </div>
            </div>
            {role !== 'SuperAdmin' && <Footer />} {/* Renderiza el footer solo si no es SuperAdmin */}
        </div>
    );
};

export default User;
