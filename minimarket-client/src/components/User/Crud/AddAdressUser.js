import React, { useState } from 'react'
import api from '../../../api'
import GetAddress from './GetAddress'
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPencil, faXmark } from "@fortawesome/free-solid-svg-icons";
const AddAdressUser = () => {
    const [Provice, SetProvice] = useState()
    const [City, Setcity] = useState()
    const [Street, SetStreet] = useState()
    const [Floor, SetFloor] = useState()
    const [Apartament, SetApartament] = useState()
    const [EditAddress, SetEditAdress] = useState(true);
    const [EmptyAddress, SetEmptyAddress] = useState(false)

    const HandleFormAdress = async(event) =>{
        event.preventDefault();
        try{
            const data = {
                province: Provice,
                city: City,
                street: Street,
                floor: Floor,
                apartment: Apartament
            }
            
             await api.put('/api/users/addresses',data)
             window.location.reload();
        }catch(error){
            
        }
    }
    const handleEmptyAddress = (isEmpty) => {
        SetEmptyAddress(isEmpty);
    };
    
    return (
    
    <div className='Form-Adrres'>
        {EditAddress && !EmptyAddress && 
        <div className='Address-Profile'>
        <GetAddress handleEmptyAddress={handleEmptyAddress}></GetAddress>
        <div style={{ display: 'flex', color: 'red'}}>
        Edit
        <FontAwesomeIcon icon={faPencil}onClick={()=>{SetEditAdress(false)}} style={{ marginLeft: '10px' }} /> </div>
        </div>}
        {(!EditAddress|| EmptyAddress)  &&<>
        <form onSubmit={HandleFormAdress} className='form-Address' >
        {(!EditAddress && !EmptyAddress) && <FontAwesomeIcon icon={faXmark} style={{width: '20px'}}  onClick={() => (SetEditAdress(true))} />}
        <label>Provice:</label>
        <input type='text' name='Provice'onChange={(e)=>{SetProvice(e.target.value)}}></input>
        <label>City</label>
        <input type='text' name='Provice'onChange={(e)=>{Setcity(e.target.value)}}></input>
        <label>Street</label>
        <input type='text' name='Street'onChange={(e)=>{SetStreet(e.target.value)}}></input>
        <label>Floor</label>
        <input type='text' name='Floor'onChange={(e)=>{SetFloor(e.target.value)}}></input>
        <label>Apartament</label>
        <input type='text' name='Apartament'onChange={(e)=>{SetApartament(e.target.value)}}></input>
        <button>Send</button>
        </form>
        </>
        }

    </div>
  )
}

export default AddAdressUser
