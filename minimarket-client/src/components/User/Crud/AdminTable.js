import React, { useState } from 'react'
import ProductsTable from '../Admin/ProductsTable'

const AdminTable = () => {
    const [button, setButton] = useState('')
  return (
    <div>
    <div className='Buton-Admin'>
       {button == '' && <div style={{ marginTop: '100px', display: 'flex', flexDirection: 'column'}}> 
      <button onClick={()=>{setButton('Products table')}}>Products table</button>
      <button onClick={()=>{setButton('Sales History')}}>Sales History</button>
      <button onClick={()=>{setButton('Create Admin')}}>Create Admin</button>
      </div>} 
    </div>
      {button == 'Products table' &&<>       
      <button  className= 'Return-Button'onClick={()=>{setButton('')}}>Return</button>
       <ProductsTable/></>}
    </div>
  )
}

export default AdminTable
