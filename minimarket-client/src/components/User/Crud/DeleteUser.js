import { width } from '@fortawesome/free-brands-svg-icons/fa42Group';
import React, { useContext, useState }  from 'react'
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPencil,faXmark } from "@fortawesome/free-solid-svg-icons";
import { ThemeContext } from '../../Context/ThemeContext';
import { AuthContext } from '../../Context/AuthContext';
import api from '../../../api';

export default function DeleteUser( { handleExit }) {
  const { theme } = useContext(ThemeContext);
    const [Showbottom, SetShowbottom] = useState(true);
    const {logout} = useContext(AuthContext)
    const handleDisactiveUser = async() =>{
        try{
           await api.delete(`/api/users/profile`)
          logout()
        }catch(error){

        }
    }
  return (
    <div>
        {Showbottom === true &&<div className='DeleteUser' style={{ backgroundColor: theme === "light" ? "" : "#a5351ca4" }}>
                <p>Are you sure you want to delete your account?</p>
               <div className='DeleteBotton'>
                <button onClick={()=>(SetShowbottom(false))}>Continue</button>
                <button onClick={handleExit}>Exit</button>
                </div>
            </div>
        } 

        {Showbottom === false &&
        <div className='DeleteUser' style={{ backgroundColor: theme === "light" ? "" : "#a5351ca4" }}>
            <FontAwesomeIcon icon={faXmark}  onClick={handleExit} style={{paddingLeft:'290px'}} />
            <p style={{width:'330px'}}>I'm sorry for your decision, goodbye.</p>
            <button onClick={handleDisactiveUser}>Accept</button>
          
        </div>
                  
        } 
    </div>
  )
}
