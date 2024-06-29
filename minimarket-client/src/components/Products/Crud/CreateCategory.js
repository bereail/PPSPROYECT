import React, { useContext, useState } from 'react';
import '../../Products/Products.css';

import api from '../../../api';
import { ThemeContext } from '../../Context/ThemeContext';


const CreateCategory = ({ fetchProducts }) => {

  const [ButtonCategory, SetButtonCategory] = useState(false);
  const [ValueCategory, SetValueCategory] = useState('')
  const [CategoryError, SetCategoryError] = useState(false)
  const [CategoryExisting, SetCategoryExisting] = useState(false)
  const { theme } = useContext(ThemeContext);



  const HandleSubmitCategory = async (event) => {
    event.preventDefault();
    try {
      if (ValueCategory != '') {
        const data = {
          categoryName: ValueCategory
        }
        const response = await api.post("/api/categories", data);
        
        SetCategoryError(false)
        window.location.reload();
      }
    } catch (error) {
      if (error.response && error.response.status === 409) {
        SetCategoryExisting(true);
      }else {
        SetCategoryError(true);
      }
  
    }
  }
  return (
    <div>
      <button
        className={`Button-Add-Category ${theme === 'dark' ? 'dark-theme' : ''}`}
        onClick={() => SetButtonCategory(!ButtonCategory)}
      >
        Add Category
      </button>          <div className={`Container-Add-Category ${ButtonCategory ? 'active' : ''}`}>
        <div>
          <form onSubmit={HandleSubmitCategory}>
            <label htmlFor="name-category">Name category</label>
            <input type="text" id="name-category" placeholder='New Category' name="name-category" onChange={(e) => SetValueCategory(e.target.value)}></input>
            <button className={`Button-Add ${theme === 'dark' ? 'dark-theme' : ''}`}>Add</button>
          </form>
          {CategoryError && <p className='Error'>Could not add category</p>}
          {CategoryExisting && <p className='Error'> existing category</p>}
        </div>
      </div>
    </div>
  );
}

export default CreateCategory
