import React, { useEffect, useState } from 'react';
import GetCodeSeller from '../../CodesSeller/GetCodeSeller';

const CodeSeller = () => {
  const [codeSellers, setCodeSellers] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const data = await GetCodeSeller();
        setCodeSellers(data);
      } catch (error) {
        console.error('Error fetching code sellers:', error);
      }
    };

    fetchData();
  }, []);

  const handleDeletecode = (sellerId) =>{
    alert(`Elimino a: ${sellerId}`)

  }
  return (
    <div>
      <table className='Container-Table'>
        <thead>
          <tr>
            <th>id</th>
            <th>employeeCode</th>
            <th>seller</th>
            <th>Delete</th>
          </tr>
        </thead>
        <tbody>
          {codeSellers.map((seller, index) => (
            <tr key={index} className='Value-Table'>
              <td>{seller.id}</td>
              <td>{seller.employeeCode}</td>
              <td>{seller.seller}</td>
              <td><button className= 'Button-Delete-Table' onClick={()=>{handleDeletecode(seller.id)}}>Delete</button></td>            </tr>
          ))}
        </tbody>
      </table>

    </div>
  );
};

export default CodeSeller;
