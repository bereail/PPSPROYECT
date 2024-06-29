import React, { useEffect, useState } from 'react'
import api from '../../../api'

const GetAddress = ({ handleEmptyAddress }) => {
    const [Adress, SetAdress] = useState([])

    useEffect(() => {
        const GetAddressUser = async () => {
            try {
                const response = await api.get('/api/users/addresses');
                const addresses = Array.isArray(response.data) ? response.data : [response.data];
                SetAdress(addresses);
            } catch (error) {
           
                handleEmptyAddress(true)
            }
        };

        GetAddressUser();
    }, []);

    return (
        <div className='Get-address'>
            {Adress.map((address, index) => (
                <div key={index} style={{ marginBottom: '20px' }}>
                    <p><strong>Province:</strong> {address.province}</p>
                    <p><strong>City:</strong> {address.city}</p>
                    <p><strong>Street:</strong> {address.street}</p>
                    <p><strong>Floor:</strong> {address.floor}</p>
                    <p><strong>Apartment:</strong> {address.apartment}</p>
                </div>
            ))}
        </div>
    );
};

export default GetAddress
