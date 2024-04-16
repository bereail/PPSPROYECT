import React, { useState } from 'react'
import CustomNavbar from '../Navbar/CustomNavbar'
import Footer from '../Footer/footer'
import imageuser from '../Image/User.png'
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPencil,faXmark } from "@fortawesome/free-solid-svg-icons";
import './User.css';
import UserGetOrders from './Crud/UserGetOrders';
import DeleteCustomer from './Crud/DeleteCustomer';
import ModifyUser from './Crud/ModifyUser';
import ModifyPassword from './Crud/ModifyPassword';
const User = () => {
    const [UserName, SetUsername] = useState('(NOMBRE)')
    const [activeButton, SetActiveButton] = useState('');
    const handleExit = () => {
        SetActiveButton('');
    };
    return (
        <div>
            <CustomNavbar></CustomNavbar>
            <div className='User'>
                <div className='Userdetails'>
                    <img src={imageuser} alt="User" className="user-image" />
                    <h3>Welcome {UserName}!</h3>

                    <button onClick={() => (SetActiveButton('Profile'))}
                        className={activeButton === "Profile" && "User-filter-button active"}> Profile</button>
                    <button onClick={() => (SetActiveButton('Orders'))}
                        className={activeButton === "Orders" && "User-filter-button active"}>Orders</button>
                    <button onClick={() => (SetActiveButton('Change Password'))} 
                        className={activeButton === "Change Password" && "User-filter-button active"}>Change Password</button>
                    <button onClick={() => (SetActiveButton('Delete Account'))} style={{ marginTop: "200px" }}
                        className={activeButton === "Delete Account" && "User-filter-button active"}>Delete Account</button>
                </div>
                {activeButton === 'Profile'&&
                        <ModifyUser></ModifyUser>
                }
                
                {activeButton === 'Orders' &&  <UserGetOrders/>}
                {activeButton === 'Change Password' &&  <ModifyPassword handleExit={handleExit} />}
                {activeButton === 'Delete Account' &&  <DeleteCustomer handleExit={handleExit} />}
                
               
            </div>
           
            <Footer></Footer>
       
        </div>
    )
}
export default User;