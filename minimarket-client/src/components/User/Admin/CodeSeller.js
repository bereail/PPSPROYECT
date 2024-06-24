import React, { useEffect, useState } from 'react';
import GetCodeSeller from '../../CodesSeller/GetCodeSeller';
import CreateCodeSeller from '../../CodesSeller/CreateCodeSeller';
import DeleteCodeSeller from '../../CodesSeller/DeleteCodeSeller';

const CodeSeller = () => {
  const [codeSellers, setCodeSellers] = useState([]);
  const [ShowCodeSeller, SetShowCodeSeller] = useState(false);
  const [InputCodeSeller, SetInputCodeSeller] = useState();
  useEffect(() => {


    fetchData();
  }, []);

  const fetchData = async () => {
    try {
      const data = await GetCodeSeller();
      setCodeSellers(data);
    } catch (error) {
      console.error('Error fetching code sellers:', error);
    }
  };

  const handleDeletecode = async(CodeId) => {
    
    try{
      const response = await DeleteCodeSeller(CodeId)
      alert(response.status )
      if (response.status === 204){
        fetchData();
      }
    }catch(error){
      console.error('Error delete code seller:', error);
    }
  }

  const HandleCreateCodeSeller = async () => {
    const data = {
      employeeCode: InputCodeSeller
    };
  
    try {
      const response = await CreateCodeSeller(data);
      if (response.status  === 200){
        fetchData();
      }
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
                <input type='text' placeholder='New Code Seller' onChange={(e)=>{SetInputCodeSeller(e.target.value)}}></input>
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
