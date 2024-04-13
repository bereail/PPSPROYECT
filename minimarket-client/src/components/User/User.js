import React, { useState } from 'react'
import CustomNavbar from '../Navbar/CustomNavbar'
import Footer from '../Footer/footer'
import imageuser from '../Image/User.png'
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPencil,faXmark } from "@fortawesome/free-solid-svg-icons";
import './User.css';
import UserGetOrders from './Crud/UserGetOrders';
import DeleteCustomer from './Crud/DeleteCustomer';
const User = () => {
    const [UserName, SetUsername] = useState('(NOMBRE)')
    const [activeButton, SetActiveButton] = useState('');
    const [avalible, Setavalible] = useState(true);

    const handleExit = () => {
        Setavalible(false);
        alert('hola');
    };
    return (
        <div>
            <CustomNavbar></CustomNavbar>
            <div className='User'>
                <div className='Userdetails'>
                    <img src={imageuser} alt="User" className="user-image" />
                    <h3>Welcome {UserName}!</h3>

                    <button onClick={() => (SetActiveButton('Profile'),Setavalible(true))}
                        className={activeButton === "Profile" && "User-filter-button active"}> Profile</button>
                    <button onClick={() => (SetActiveButton('Orders'),Setavalible(true))}
                        className={activeButton === "Orders" && "User-filter-button active"}>Orders</button>
                    <button onClick={() => (SetActiveButton('Change Password'),Setavalible(true))} 
                        className={activeButton === "Change Password" && "User-filter-button active"}>Change Password</button>
                    <button onClick={() => (SetActiveButton('Delete Account'),Setavalible(true))} style={{ marginTop: "200px" }}
                        className={activeButton === "Delete Account" && "User-filter-button active"}>Delete Account</button>
                </div>
                {activeButton === 'Profile'&&
                <div className='UserProfile'>
                    {avalible === false &&(<FontAwesomeIcon icon={faXmark}  style={{paddingLeft:'400px'}} onClick={()=>(Setavalible(true))}/>)}
                    <h4>Name</h4>
                    <input type='text' placeholder='Nombre de la cuenta' disabled={avalible}></input>
                    <h4>Email</h4>
                    <input type='text' placeholder='Email de la cuenta' disabled={avalible}></input>
                    <h4>Address</h4>
                    <input type='text' placeholder='Direccion de la cuenta' disabled={avalible}></input>
                    <h4>Phone</h4>
                    <input type='text' placeholder='Numero de telefono de la cuenta' disabled={avalible}></input>

                    <div style={{ display:'flex', color:'red', paddingLeft: '370px'}} onClick={()=>(Setavalible(false))}>
                        Edit
                        <FontAwesomeIcon icon={faPencil}  style={{marginLeft:'10px'}}/>
                    </div>
                
                </div>
                }
                
                {activeButton === 'Orders' &&  <UserGetOrders/>}
                {activeButton === 'Delete Account' && avalible === true &&  <DeleteCustomer handleExit={handleExit} />}
                
               
            </div>
           
            <Footer></Footer>
       
        </div>
    )
}
export default User;