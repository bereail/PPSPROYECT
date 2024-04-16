import React, { useState } from 'react'
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPencil,faXmark } from "@fortawesome/free-solid-svg-icons";
export default function ModifyUser() {
    const [avalible, Setavalible] = useState(true);

  return (
      <div className='UserProfile'>
                    {avalible === false &&(<FontAwesomeIcon icon={faXmark}  style={{paddingLeft:'400px'}} onClick={()=>(Setavalible(true))}/>)}
                    <h4>Name</h4>
                    <input type='text' placeholder='Nombre de la cuenta' disabled={avalible}></input>
                    <h4>Email</h4>
                    <input type='text' placeholder='Email de la cuenta' disabled={avalible}></input>
                    <h4>Address</h4>
                    <input type='text' placeholder='Direccion de la cuenta' disabled={avalible}></input>
                    <h4>Phone</h4>
                    <input type='text' placeholder='Numero de telefono de la cuenta' disabled={avalible}></input>
 
                    <div style={{ display:'flex', color:'red', paddingLeft: '370px'}} onClick={()=>(Setavalible(false))}>
                        Edit
                        <FontAwesomeIcon icon={faPencil}  style={{marginLeft:'10px'}}/>
                    </div>
                
    </div>
  
  )
}
