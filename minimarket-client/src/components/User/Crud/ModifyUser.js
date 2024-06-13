import React, { useContext, useState } from 'react'
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPencil, faXmark } from "@fortawesome/free-solid-svg-icons";
import { ThemeContext } from "../../Context/ThemeContext";
import GetUserbyid from './GetUserbyid';
import api from '../../../api';
export default function ModifyUser() {
  const { theme } = useContext(ThemeContext);
  const [available, Setavailable] = useState(true);
  const user = GetUserbyid();
  const [name, setName] = useState('');
  const [phoneNumber, setPhoneNumber] = useState('');

  const handleModifyUser = async (event) => {
    event.preventDefault(); 
    try {
      const editData = {
        name: name || user?.name,
        phoneNumber: phoneNumber || user?.phoneNumber, 
      };
      const response = await api.put('/api/users', editData); 
      Setavailable(true);
    } catch (error) {
      console.log('Error Modify User', error);
    }
  };


  return (
    <div className='UserProfile' style={{ backgroundColor: theme === "light" ? "" : "#a5351ca4" }}>
      <form onSubmit={handleModifyUser}>
        {available === false && (<FontAwesomeIcon icon={faXmark} onClick={() => (Setavailable(true))} />)}
        <h4>Name</h4>
        <input onChange={(e) => setName(e.target.value)} type='text' placeholder={user && user.name} disabled={available}></input>
        <h4>Phone</h4>
        <input onChange={(e) => setPhoneNumber(e.target.value)} type='text' placeholder={user && user.phoneNumber} disabled={available}></input>

        {!available && <button type='submit' className='Button-Modify-User'>Send</button>}
        <div style={{ display: 'flex', color: 'red'}} onClick={() => (Setavailable(false))}>
          Edit
          <FontAwesomeIcon icon={faPencil} style={{ marginLeft: '10px' }} />
        </div>
      </form>
    </div>

  )
}
