import React, { useContext, useState } from 'react'
import Footer from '../Footer/footer'
import imageuser from '../Image/User.png'
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPencil,faXmark } from "@fortawesome/free-solid-svg-icons";
import './User.css';
import UserGetOrders from './Crud/UserGetOrders';
import DeleteCustomer from './Crud/DeleteCustomer';
import ModifyUser from './Crud/ModifyUser';
import { ThemeContext } from '../Context/ThemeContext';
import Navbar from '../Navbar/Navbar';
import GetUserbyid from './Crud/GetUserbyid';
const User = () => {

    const { theme } = useContext(ThemeContext);
    const [activeButton, SetActiveButton] = useState('');
    const user = GetUserbyid();
    const handleExit = () => {
        SetActiveButton('');
    };
    return (
        <div>
            <Navbar></Navbar>
            <div className='User' >
                <div className='Userdetails'style={{ backgroundColor: theme === "light" ? "" : "#a5351ca4" }}>
                    <img src={imageuser} alt="User" className="user-image" />
                    <h3>Welcome {user && user.name}!</h3>

                    <button onClick={() => (SetActiveButton('Profile'))}
                        className={activeButton === "Profile" ? "User-filter-button active" : ''}> Profile</button>
                    <button onClick={() => (SetActiveButton('Orders'))}
                        className={activeButton === "Orders" ? "User-filter-button active" : ''}>Orders</button>
                    <button onClick={() => (SetActiveButton('Delete Account'))} style={{ marginTop: "200px" }}
                        className={activeButton === "Delete Account" ? "User-filter-button active" : ''}>Delete Account</button>
                </div>
                {activeButton === 'Profile'&&
                        <ModifyUser></ModifyUser>
                }
                
                {activeButton === 'Orders' &&  <UserGetOrders/>}
                {activeButton === 'Delete Account' &&  <DeleteCustomer handleExit={handleExit} />}
                
               
            </div>        
            <Footer></Footer>      
        </div>
    )
}
export default User;