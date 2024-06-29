import React, { useContext, useEffect, useState } from 'react'
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faXmark } from "@fortawesome/free-solid-svg-icons";
import { ThemeContext } from '../../Context/ThemeContext';
import api from '../../../api';
import GetOrderByUser from '../../Orders/GetOrderByUser';
import '../User.css'
import GetOrderByid from '../../Orders/GetOrderByid';
import PayWhitMP from '../../PayWhitMP/PayWhitMP';
import { OrderContext } from '../../Context/OrderContext';
import usePagination from '../../CustomHook/usePagination';
export default function UserGetOrders() {
    const [Orders, SetOrders] = useState([]);
    const [ShowOrders, SetShowOrders] = useState(false);
    const [OrderDetails, SetOrderDetails] = useState(null);
    const [ButtonMp, SetButtonMp] = useState(false)
    const { setOrderId} = useContext(OrderContext);
    const { pageNumber, PaginationButtons } = usePagination();

    useEffect(() => {


        fetchData(pageNumber);
    }, [pageNumber]);

    
    const fetchData = async (pageNumber) => {
        try {
            const data = await GetOrderByUser(pageNumber);
            SetOrders(data)
        } catch (error) {
            console.error('Error fetching code sellers:', error);
        }
    };
    const handleRowClick = async (orderId) => {
        SetShowOrders(true);
        const orderDetails = await GetOrderByid(orderId);
        if (orderDetails) {
            SetOrderDetails(orderDetails);
        } else {
            console.error('Error fetching order details');
        }
    };


    const PayWhitMp = async(orderId) =>{
        
            SetButtonMp(true)
            setOrderId(orderId)
            window.localStorage.setItem('OrderId', orderId); 
          
    }
    return (
        <div>
        {!ShowOrders && (
            <>
                { Orders && Orders.length > 0 ? (
                    <>
                        <table className='Container-Table'>
                            <thead>
                                <tr>
                                    <th>Final Price</th>
                                    <th>Order Time</th>
                                    <th>Status</th>
                                </tr>
                            </thead>
                            <tbody>
                                {Orders.map((order, index) => (
                                    <tr key={index} className='Value-Table' onClick={() => handleRowClick(order.id)}>
                                        <td>{order.finalPrice}</td>
                                        <td>{order.orderTime}</td>
                                        <td>{order.status}</td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                        <PaginationButtons />

                    </>
                ) : (
                    <div className="no-products">there are no orders</div>
                )}
            </>
        )}

{ShowOrders && OrderDetails && (
            <div className='OrderDetails'>
                <h2>Order Details</h2>
                <p><strong>Final Price:</strong> {OrderDetails.finalPrice}</p>
                <p><strong>Order Time:</strong> {OrderDetails.orderTime}</p>
                <p><strong>Status:</strong> {OrderDetails.status}</p>
                <p><strong>Expiration Time:</strong> {OrderDetails.expirationTime}</p>
                <p><strong>Finish Time:</strong> {OrderDetails.finishTime}</p>
                
                <h3>Delivery Address</h3>
                <p><strong>Province:</strong> {OrderDetails.deliveryAddress.province}</p>
                <p><strong>City:</strong> {OrderDetails.deliveryAddress.city}</p>
                <p><strong>Street:</strong> {OrderDetails.deliveryAddress.street}</p>
                <p><strong>Floor:</strong> {OrderDetails.deliveryAddress.floor}</p>
                <p><strong>Apartment:</strong> {OrderDetails.deliveryAddress.apartment}</p>

                <h3>Order Items</h3>
                {OrderDetails.details.map((item, index) => (
                    <div key={index} className='OrderItem'>
                        <p><strong>Product Name:</strong> {item.productName}</p>
                        <p><strong>Product Quantity:</strong> {item.productQuantity}</p>
                        <p><strong>Detail Price:</strong> {item.detailPrice}</p>
                    </div>
                ))}
                <button className='BackButton' onClick={() => SetShowOrders(false)}>Back to Orders</button>
                <button className='Pay-Button' onClick={() => PayWhitMp(OrderDetails.id)}>Pay the order</button>

                {ButtonMp && <> <PayWhitMP/> </>}

            
            </div>
        )}
    </div>
);
};
