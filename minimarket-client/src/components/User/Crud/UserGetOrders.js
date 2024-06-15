import React, { useContext, useState } from 'react'
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {faXmark } from "@fortawesome/free-solid-svg-icons";
import { ThemeContext } from '../../Context/ThemeContext';
import api from '../../../api';

export default function UserGetOrders() {
    const { theme } = useContext(ThemeContext);

    const GetOrdersUser = async()=>{
        try{
           

        }catch(error){
            console.log("Error Get Orders", error)
        }
    }
    return (
        <div className='UserOrder' style={{ backgroundColor: theme === "light" ? "" : "#a5351ca4" }}>
           
            <div className='UserOrder-tittle'>
                <p>Product</p>
                <p>Quantity</p>
                <p>Price</p>
            </div>
            <div className='UserOrder-Details'>
                <div className='UserOrder-prodcut'>

                </div>
                <div className='UserOrder-Quantity'>

                </div>
                <div className='UserOrder-Price'>

                </div>
            </div>

            <div className='Order-Price'>
                <p>Discount</p>
                <p>Total value</p>
            </div>
        </div>
    )
}
