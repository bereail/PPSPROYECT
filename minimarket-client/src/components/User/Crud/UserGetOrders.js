import React, { useState } from 'react'
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {faXmark } from "@fortawesome/free-solid-svg-icons";

export default function UserGetOrders() {

    return (
        <div className='UserOrder'>
           
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
