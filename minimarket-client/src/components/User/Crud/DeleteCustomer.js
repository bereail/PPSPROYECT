import { width } from '@fortawesome/free-brands-svg-icons/fa42Group';
import React, { useState }  from 'react'
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPencil,faXmark } from "@fortawesome/free-solid-svg-icons";

export default function DeleteCustomer( { handleExit }) {
    const [Showbottom, SetShowbottom] = useState(true);
  return (
    <div>
        {Showbottom === true &&<div className='DeleteUser'>
                <p>Are you sure you want to delete your account?</p>
               <div className='DeleteBotton'>
                <button onClick={()=>(SetShowbottom(false))}>Continue</button>
                <button onClick={handleExit}>Exit</button>
                </div>
            </div>
        } 

        {Showbottom === false &&
        <div className='DeleteUser'>
            <FontAwesomeIcon icon={faXmark}  onClick={handleExit} style={{paddingLeft:'290px'}} />
            <p style={{width:'300px'}}>I'm sorry for your decision, goodbye.</p>
            <p style={{marginBottom: '-10px', textAlign: 'justify'}}>Password</p>
            <div className='Password-Delete'> 
            <input type='password' style={{width: '200px', height: '30px'}}></input>
            <button>Accept</button>
            </div>
            
        </div>
                  
        } 
    </div>
  )
}
