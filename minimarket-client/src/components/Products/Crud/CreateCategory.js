import React, {useState } from 'react';
import '../../Products/Products.css';

import Api from '../../../Api';


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
            const api = Api()
            const response = await api.post("/api/categories", data);
            alert(JSON.stringify(response.data))
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
    <>
      <button className='Button-Add-Category' onClick={() => (SetButtonCategory(!ButtonCategory))}>Add Category</button>
        {ButtonCategory &&
          <div className='Container-Add-Category'>
            <form onSubmit={HandleSubmitCategory}>
              <label htmlFor="name-category">Name category</label>
              <input type="text" id="name-category" placeholder='New Category' name="name-category" onChange={(e) => (SetValueCategory(e.target.value))}></input>
              <button>Add</button>
            </form>
            {CategoryError && <p className='Error'>Could not add category</p>}
          </div>}
    </>
  )
}

export default CreateCategory
