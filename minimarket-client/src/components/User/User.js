import React, { useContext, useState } from 'react';
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

const User = () => {
    const { theme } = useContext(ThemeContext);
    const {role} = useContext(AuthContext)
    const [activeButton, setActiveButton] = useState('');

    const user = GetUserbyid();


    const handleExit = () => {
        setActiveButton('');
    };
  

    return (
        <div>
            <Navbar />
            <div className='User' style={{alignItems: 'center'}}>
                <div className='Userdetails' style={{ backgroundColor: theme === "light" ? "" : "#a5351ca4" }}>
                    <img src={imageuser} alt="User" className="user-image" />
                    <h3>Welcome {user && user.name}!</h3>

                    <button onClick={() => setActiveButton('Profile')}
                        className={activeButton === "Profile" ? "User-filter-button active" : ''}> Profile</button>
                    <button onClick={() => setActiveButton('Orders')}
                        className={activeButton === "Orders" ? "User-filter-button active" : ''}>Orders</button>
                    <button onClick={() => setActiveButton('Add Adress')}>Adress</button>
                    
                    {role ==='SuperAdmin'  && (
                        <button onClick={() => setActiveButton('AdminProperties')} style={{ marginTop: "200px" }}
                            className={activeButton === "AdminProperties" ? "User-filter-button active" : ''}>Admin Properties</button>
                    )}
                    
                    
                    <button onClick={() => setActiveButton('Delete Account')} style={{ marginTop: "200px" }}
                        className={activeButton === "Delete Account" ? "User-filter-button active" : ''}>Delete Account</button>
                </div>
                 <div>  
                {activeButton === 'Profile' &&
                    <ModifyUser />
                }

                {activeButton === 'Orders' &&
                    <UserGetOrders />
                }
                {activeButton === 'Add Adress'  &&<AddAdressUser></AddAdressUser> }
                
                {activeButton === 'AdminProperties' && 'SuperAdmin' && (
                        <div className="admin-properties">
                            
                            <p>Administrative settings and options go here.</p>
                            <AdminManagement></AdminManagement>
                        </div>
                    )}



                {activeButton === 'Delete Account' &&
                    <DeleteUser handleExit={handleExit} />
                }
            </div> 
            </div>
            <Footer />
        </div>
    );
};

export default User;
