import React, { useState } from 'react'
import api from '../../../api';

const CreateAdmin = () => {
    const [InputValue, setInputValue] = useState({
        name: '',
        email: '',
        password: '',
        phoneNumber: '',
      });

      const [errors, setErrors] = useState({
        name: false,
        phoneNumber: false,
        email: false,
        password: false,
      });


      const handleInputChange = (e) => {
        const { name, value } = e.target;
        setInputValue(prevState => ({
          ...prevState,
          [name]: value
        }));
        setErrors(prevErrors => ({
            ...prevErrors,
            [name]: value.trim() === ''
          }));
      }



      const HandleCreateAdmin = async(e) =>{  
        e.preventDefault();

        const { name, phoneNumber, email, password} = InputValue;

        if (phoneNumber.trim().length > 13) {
            setErrors(prevErrors => ({
              ...prevErrors,
              phoneNumber: true
            }));
            return;
          }
      
          if (name.trim() === '' === '' || phoneNumber.trim() === '' || email.trim() === '' || password.trim() === '') {
            setErrors({
              name: name.trim() === '',
              phoneNumber: phoneNumber.trim() === '',
              email: email.trim() === '',
              password: password.length < 8,
            });
            return;
          }

        if (InputValue.name !== '' && InputValue.email !== '' && InputValue.password !== '' && InputValue.phoneNumber !== null) {    
           try{
            await api.post('/api/admin',InputValue)
           }catch(error){
   
           }
        }
          
      }
      return (
        <div className="form-container">
            <form onSubmit={HandleCreateAdmin}>
                <label htmlFor="name">Name</label>
                <input type='text' id='name' name='name' onChange={handleInputChange} />
                {errors.name && <p className='Error'>Set a first name</p>}
                <label htmlFor="email">Email</label>
                <input type='email' id='email' name='email' onChange={handleInputChange} />
                {errors.email && <p className='Error'>Set an email</p>}

                <label htmlFor="password">Password</label>
                <input type='password' id='password' name='password' onChange={handleInputChange} />
                {errors.password && <p className='Error'>Minimum of 8 characters</p>}

                <label htmlFor="phoneNumber">Phone Number</label>
                <input type='tel' id='phoneNumber' name='phoneNumber' onChange={handleInputChange} />
                {errors.phoneNumber && <p className='Error'>Phone number must be less than or equal to 10 digits</p>}

                <button type="submit">Send</button>
            </form>
        </div>
    );
};

export default CreateAdmin
