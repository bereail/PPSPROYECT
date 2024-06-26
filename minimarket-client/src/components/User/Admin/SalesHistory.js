import React, { useEffect, useState } from 'react';
import GetOrder from '../../Orders/GetOrder';
import './AdminTable.css';
import GetOrderByid from '../../Orders/GetOrderByid';
import usePagination from '../../CustomHook/usePagination';
import useProductFilters from '../../CustomHook/useProductFilters'; // Importa el custom hook de filtros de productos

const SalesHistory = () => {
    const [Orders, SetOrders] = useState([]);
    const [ShowOrders, SetShowOrders] = useState(false);
    const [OrderDetails, SetOrderDetails] = useState(null);

    const { pageNumber, PaginationButtons } = usePagination();

    useEffect(() => {

        fetchOrders();
    }, [pageNumber]);

    const fetchOrders = async () => {
        const orders = await GetOrder(pageNumber);
        if (orders) {
            SetOrders(orders);
        } else {
            console.error('Error fetching orders');
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

    return (
        <div>
            {!ShowOrders && (
                <>
                    {Orders.length > 0 ? (
                        <>
                            <table className='Container-Table'>
                                <thead>
                                    <tr>
                                        <th>id</th>
                                        <th>Final Price</th>
                                        <th>Order Time</th>
                                        <th>Status</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {Orders.map((order, index) => (
                                        <tr key={index} className='Value-Table' onClick={() => handleRowClick(order.id)}>
                                            <td>{order.id}</td>
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
                    <p><strong>ID:</strong> {OrderDetails.id}</p>
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
                </div>
            )}
        </div>
    );
};

export default SalesHistory;
