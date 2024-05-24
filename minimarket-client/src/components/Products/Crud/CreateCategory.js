import React, {useState } from 'react';
import '../../Products/Products.css';

import api from '../../../api';


const CreateCategory = () => {

  const [ButtonCategory, SetButtonCategory] = useState(false);
  const [ValueCategory, SetValueCategory] = useState('')
  const [CategoryError, SetCategoryError] = useState(false)


    const HandleSubmitCategory = async (event) => {
        event.preventDefault();
        try {
          if (ValueCategory != '') {
            const data = {
              categoryName: ValueCategory
            }
            console.log("Cabecera de la solicitud:", api.defaults.headers);
            const response = await api.post("/api/categories", data);
            SetCategoryError(false)
             window.location.reload();
          }
        } catch (error) {
           
          SetCategoryError(true)
          alert('error')
          console.error('Error add category:', error);
        }
      }
      return (
        <div>
          <button className='Button-Add-Category' onClick={() => SetButtonCategory(!ButtonCategory)}>Add Category</button>
          <div className={`Container-Add-Category ${ButtonCategory ? 'active' : ''}`}>
            <div> {/* Contenedor adicional para controlar la visibilidad */}
              <form onSubmit={HandleSubmitCategory}>
                <label htmlFor="name-category">Name category</label>
                <input type="text" id="name-category" placeholder='New Category' name="name-category" onChange={(e) => SetValueCategory(e.target.value)}></input>
                <button>Add</button>
              </form>
              {CategoryError && <p className='Error'>Could not add category</p>}
            </div>
          </div>
        </div>
      );
    }

export default CreateCategory
