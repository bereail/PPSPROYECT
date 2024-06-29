import React, { useEffect, useState } from 'react';
import GetCodeSeller from '../../CodesSeller/GetCodeSeller';
import CreateCodeSeller from '../../CodesSeller/CreateCodeSeller';
import DeleteCodeSeller from '../../CodesSeller/DeleteCodeSeller';
import ActiveCoseSeller from '../../CodesSeller/ActiveCoseSeller';
import DisabelCodeSeller from '../../CodesSeller/DisabelCodeSeller';

const CodeSeller = () => {
  const [codeSellers, setCodeSellers] = useState([]);
  const [ShowCodeSeller, SetShowCodeSeller] = useState(false);
  const [InputCodeSeller, SetInputCodeSeller] = useState();
  const [ExistingCode, SetExistingCode] = useState(false)
  const [ErrorCode, SetErrorCode] = useState(false)
  useEffect(() => {


    fetchData();
  }, []);

  const fetchData = async () => {
    try {
      const data = await GetCodeSeller();
      setCodeSellers(data);
    } catch (error) {
    }
  };

  const handleDeletecode = async(CodeId) => {
    
    try{
      const response = await DeleteCodeSeller(CodeId)

      if (response.status === 204){
        fetchData();
      }
    }catch(error){
    }
  }

  const handleDisabelcode = async(CodeId) => {
    
    try{
      const response = await DisabelCodeSeller(CodeId)
      if (response.status === 200){
        fetchData();
      }
    }catch(error){
    }
  }

   const handleActivecode = async(CodeId) =>{
  try{
    const response = await ActiveCoseSeller(CodeId)
    if (response.status === 200){
      fetchData();
    }
  }catch(error){
  }
 }

  const HandleCreateCodeSeller = async () => {
    const data = {
      employeeCode: InputCodeSeller
    };
  
    try {
      const response = await CreateCodeSeller(data);
      
      if (response && response.status === 200) {
        fetchData();
      }
    
    } catch (error) {
      if (error.response && error.response.status === 409) {
        SetExistingCode(true);
      }else{
        SetErrorCode(true)
      }
    }
  };
  return (
    <div>
      <table className='Container-Table'>
        <thead>
          <tr>
            <th>id</th>
            <th>employeeCode</th>
            <th>Status</th>
            <th>Delete</th>
          </tr>
        </thead>
        <tbody>
          {codeSellers.map((seller, index) => (
            <tr key={index} className='Value-Table' style={{ backgroundColor: seller.isActive ? '' : '#b5b8b5' }}>
              <td>{seller.id}</td>
              <td>{seller.employeeCode}</td>
              {seller.isActive ? <td><button className='Button-Delete-Table' onClick={() => { handleDisabelcode(seller.id) }}>Disable</button></td>:
              <td><button className='Button-Active-Table' onClick={()=> {handleActivecode(seller.id)}}>Active</button></td>
              } 
              {!seller.isActive && <td><button className='Button-Delete-Table' onClick={() => { handleDeletecode(seller.id) }}>Delete</button></td>}
            </tr>
          ))}
          <tfoot>
            {ShowCodeSeller && 
            <tr>
              <td>
                <input type='text' placeholder='New Code Seller' onChange={(e)=>{SetInputCodeSeller(e.target.value)}}></input>
                {ExistingCode && <p className='error'>This code already exists</p>}
                {ErrorCode && <p className='error'>The code must be between 126 and 25 characters</p> }
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
