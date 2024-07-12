import React, { useState, useEffect, useContext } from 'react';
import GetUserbyid from './Crud/GetUserbyid';
import DeleteUser from './Crud/DeleteUser';
import AddAdressUser from './Crud/AddAdressUser';
import Chathistory from '../ChatBot/Chathistory';
import AdminTable from './Crud/AdminTable';
import { AuthContext } from '../Context/AuthContext';
import { ThemeContext } from '../Context/ThemeContext';
import Navbar from '../Navbar/Navbar';
import Footer from '../Footer/footer';
import ModifyUser from '../User/Crud/ModifyUser';
import UserGetOrders from '../User/Crud/UserGetOrders';
import imageuser from '../Image/User.png';
import './User.css';
import { Link } from 'react-router-dom';


const User = () => {
    const { theme } = useContext(ThemeContext);
    const { role } = useContext(AuthContext);
    const [activeButton, setActiveButton] = useState('');
    const [emails, setEmails] = useState([]);
    const [chats, setChats] = useState([]);
    const [showChathistory, setShowChathistory] = useState(false); // Estado para controlar la visibilidad del historial de chat
    const user = GetUserbyid();

    useEffect(() => {
        const storedEmails = JSON.parse(localStorage.getItem('subscribedEmails')) || [];
        setEmails(storedEmails);

        if (role === 'SuperAdmin') {
            const savedChats = JSON.parse(localStorage.getItem('chats')) || [];
            setChats(savedChats);
        }
    }, [role]);

    const toggleChathistory = () => {
        setShowChathistory(!showChathistory);
        setActiveButton(showChathistory ? '' : 'showChathistory');
    };

    const handleExit = () => {
        setActiveButton('');
    };

    return (
        <div>
            <Navbar />
            <div className='User'>
            <div className={`Userdetails ${theme === 'dark' ? 'dark-theme' : ''}`}>                    <img src={imageuser} alt="User" className="user-image" />
                    <h3>Welcome {user && user.name}!</h3>

                    <button onClick={() => setActiveButton('Profile')}
                        className={activeButton === "Profile" ? "User-filter-button active" : "User-filter-button"}> Profile</button>
                    {role !== 'SuperAdmin' && role !== 'Seller' &&
                        <button onClick={() => setActiveButton('Orders')} className={activeButton === "Orders" ? "User-filter-button active" : "User-filter-button"}>Orders</button>
                    }
                    <button onClick={() => setActiveButton('Add Address')}
                        className={activeButton === "Add Address" ? "User-filter-button active" : "User-filter-button"}>Address</button>

                    {role === 'SuperAdmin' && (
                        <div>
                            <button onClick={() => setActiveButton("ShowEmails")} className={activeButton === "ShowEmails" ? "User-filter-button active" : "User-filter-button"}>
                                Show Registered Emails
                            </button>

                            <button onClick={toggleChathistory} className={activeButton === 'showChathistory' ? "User-filter-button active" : "User-filter-button"}>
                                Chat History
                            </button>

                            <button onClick={() => setActiveButton('AdminProperties')}
                                className={activeButton === "AdminProperties" ? "User-filter-button active" : "User-filter-button"}>Admin Properties</button>
                        </div>
                    )}
                    <Link to="/resetPasswordForm" className='Nav-LinkFavorite'>
                        <button>change Password</button>
                    </Link>

                    {role === 'Customer' &&
                        <button onClick={() => setActiveButton('Delete Account')} style={{ marginTop: "100px" }}
                            className={activeButton === "Delete Account" ? "User-filter-button active" : "User-filter-button"}>Delete Account</button>
                    }
            

                </div>
                <div className="user-content-container">
                    {activeButton === 'ShowEmails' && (
                        <ul className='ShowEmail'>
                            {emails.map((email, index) => (
                                <li key={index}>{email}</li>
                            ))}
                        </ul>
                    )}

                    {activeButton === 'Profile' && <ModifyUser />}
                    {activeButton === 'Orders' && role !== 'SuperAdmin' && role !== 'Seller' && <UserGetOrders />}
                    {activeButton === 'Add Address' && <AddAdressUser />}
                    {activeButton === 'showChathistory' && <Chathistory chats={chats} />}
                    {activeButton === 'AdminProperties' && role === 'SuperAdmin' && (
                        <div className="admin-properties">
                            <AdminTable />
                        </div>
                    )}
                    {activeButton === 'Delete Account' && role === 'Customer' && <DeleteUser handleExit={handleExit} />}
                </div>
            </div>
            {role !== 'SuperAdmin' && <Footer />}
        </div>
    );
};

export default User;
