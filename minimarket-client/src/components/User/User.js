import React, { useContext, useState } from 'react';
import Footer from '../Footer/footer';
import imageuser from '../Image/User.png';
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPencil, faXmark } from "@fortawesome/free-solid-svg-icons";
import './User.css';
import UserGetOrders from './Crud/UserGetOrders';
import DeleteCustomer from './Crud/DeleteCustomer';
import ModifyUser from './Crud/ModifyUser';
import { ThemeContext } from '../Context/ThemeContext';
import Navbar from '../Navbar/Navbar';
import GetUserbyid from './Crud/GetUserbyid';

const User = () => {
    const { theme } = useContext(ThemeContext);
    const [activeButton, setActiveButton] = useState('');
    const user = GetUserbyid(); // Asumiendo que esta función devuelve el usuario actual

    const handleExit = () => {
        setActiveButton('');
    };

    const isAdmin = user && user.role === 'admin'; // Verifica si el usuario es administrador
    const isSeller = user && user.role === 'seller'; // Verifica si el usuario es vendedor

    return (
        <div>
            <Navbar />
            <div className='User'>
                <div className='Userdetails' style={{ backgroundColor: theme === "light" ? "" : "#a5351ca4" }}>
                    <img src={imageuser} alt="User" className="user-image" />
                    <h3>Welcome {user && user.name}!</h3>

                    <button onClick={() => setActiveButton('Profile')}
                        className={activeButton === "Profile" ? "User-filter-button active" : ''}> Profile</button>
                    <button onClick={() => setActiveButton('Orders')}
                        className={activeButton === "Orders" ? "User-filter-button active" : ''}>Orders</button>
                    {isAdmin && (
                        <button onClick={() => setActiveButton('AdminProperties')} style={{ marginTop: "200px" }}
                            className={activeButton === "AdminProperties" ? "User-filter-button active" : ''}>Admin Properties</button>
                    )}
                    {isSeller && (
                        <button onClick={() => setActiveButton('SellerProperties')} style={{ marginTop: "200px" }}
                            className={activeButton === "SellerProperties" ? "User-filter-button active" : ''}>Seller Properties</button>
                    )}
                    <button onClick={() => setActiveButton('Delete Account')} style={{ marginTop: "200px" }}
                        className={activeButton === "Delete Account" ? "User-filter-button active" : ''}>Delete Account</button>
                </div>

                {activeButton === 'Profile' &&
                    <ModifyUser />
                }

                {activeButton === 'Orders' &&
                    <UserGetOrders />
                }

                {activeButton === 'AdminProperties' && isAdmin && (
                    <div className="admin-properties">
                        {/* Aquí puedes mostrar las propiedades del administrador */}
                        <h2>Admin Properties</h2>
                        <p>Administrative settings and options go here.</p>
                    </div>
                )}

                {activeButton === 'SellerProperties' && isSeller && (
                    <div className="seller-properties">
                        {/* Aquí puedes mostrar las propiedades específicas del vendedor */}
                        <h2>Seller Properties</h2>
                        <p>Seller-specific settings and options go here.</p>
                    </div>
                )}

                {activeButton === 'Delete Account' &&
                    <DeleteCustomer handleExit={handleExit} />
                }
            </div>
            <Footer />
        </div>
    );
};

export default User;
