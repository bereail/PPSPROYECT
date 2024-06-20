import React, { useEffect, useState } from 'react';
import GetCodeSeller from '../../CodesSeller/GetCodeSeller';
import CreateCodeSeller from '../../CodesSeller/CreateCodeSeller';

const CodeSeller = () => {
  const [codeSellers, setCodeSellers] = useState([]);
  const [ShowCodeSeller, SetShowCodeSeller] = useState(false);
  const [InputCodeSeller, SetInputCodeSeller] = useState();
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

  const handleDeletecode = (sellerId) => {
    alert(`Elimino a: ${sellerId}`)

  }

  const HandleCreateCodeSeller = async () => {
    const data = {
      employeeCode: InputCodeSeller
    };
  
    try {
      const response = await CreateCodeSeller(data);
      console.log(response);
    } catch (error) {
      console.error('Error creating code seller:', error);
    }
  };
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
              <td><button className='Button-Delete-Table' onClick={() => { handleDeletecode(seller.id) }}>Delete</button></td>
            </tr>
          ))}
          <tfoot>
            {ShowCodeSeller && 
            <tr>
              <td>
                <input type='text' placeholder='New Code Seller' onChange={()=>{SetInputCodeSeller()}}></input>
              <button className='Button-Create' onClick={HandleCreateCodeSeller}>Create</button>
              </td>
            </tr>}
          </tfoot>
        </tbody>
        <button className='Button-CreateSellerCode' onClick={() => {SetShowCodeSeller(!ShowCodeSeller)}}>Create Code Seller</button>
      </table>

    </div>
  );
};

export default CodeSeller;
