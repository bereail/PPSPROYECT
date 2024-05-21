
import React, { useContext, useState } from 'react'
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faL, faPencil,faXmark } from "@fortawesome/free-solid-svg-icons";
import { ThemeContext } from '../../Context/ThemeContext';

export default function ModifyPassword() {
  const { theme } = useContext(ThemeContext);
    const [avalible, Setavalible] = useState(true);
  return (

           <div className='ModifyPassword'  style={{ backgroundColor: theme === "light" ? "" : "#a5351ca4" }} >
                    {avalible === false &&(<FontAwesomeIcon icon={faXmark}  style={{paddingLeft:'400px'}} onClick={()=>(Setavalible(true))}/>)}
                   
                    {avalible == true && <>
                    <h4>Password</h4>
                    <p>*********</p>
                    </>
                    }   

                   
                    {avalible == false && <>
                    <h4>Current password</h4>
                    <input type='Password'></input>
                    <h4>NewPassword</h4>
                    <input type='Password'></input>
                   
                    <button>Save Changes</button>
                    </>
                    }
 
                    <div style={{ display:'flex', color:'red'}} onClick={()=>(Setavalible(false))}>
                        Change Password
                        <FontAwesomeIcon icon={faPencil}  style={{marginLeft:'10px'}}/>
                    </div>
                
    </div>
  )
}
