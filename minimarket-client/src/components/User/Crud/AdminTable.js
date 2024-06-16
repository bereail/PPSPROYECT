import React, { useState } from 'react'
import ProductsTable from '../Admin/ProductsTable'
import SalesHistory from '../Admin/SalesHistory'
import CreateAdmin from '../Admin/CreateAdmin'
import CodeSeller from '../Admin/CodeSeller'

const AdminTable = () => {
    const [button, setButton] = useState('')
  return (
    <div>
    <div className='Buton-Admin'>
       {button == '' && <div style={{ marginTop: '100px', display: 'flex', flexDirection: 'column'}}> 
      <button onClick={()=>{setButton('Products table')}}>Products table</button>
      <button onClick={()=>{setButton('Sales History')}}>Sales History</button>
      <button onClick={()=>{setButton('Create Admin')}}>Create Admin</button>
      <button onClick={()=>{setButton('Create Code')}}>Code Seller</button>

      </div>} 
    </div>
      {button == 'Products table' &&<>       
      <button  className= 'Return-Button'onClick={()=>{setButton('')}}>Return</button>
       <ProductsTable/></>}
       {button == 'Sales History' && <>
          <button  className= 'Return-Button'onClick={()=>{setButton('')}}>Return</button>
          <SalesHistory/>
       </>}
       {button == 'Create Admin' && <>
          <button  className= 'Return-Button'onClick={()=>{setButton('')}}>Return</button>
          <CreateAdmin/>
       </>}
       {button == 'Create Code' && <>
          <button  className= 'Return-Button'onClick={()=>{setButton('')}}>Return</button>
          <CodeSeller/>
       </>}
    </div>
  )
}

export default AdminTable
