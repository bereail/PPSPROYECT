import React, { useContext, useState } from 'react';
import { CategoryContext } from '../../Context/CategoryContext';
import api from '../../../api';
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPlus } from "@fortawesome/free-solid-svg-icons";
import { ThemeContext } from '../../Context/ThemeContext';


const CreaateProduct = ({ fetchProducts }) => {
  const [ButtonAddProduct, SetButtonAddProduct] = useState(false);
  const [Name, SetName] = useState('');
  const [Description, SetDescription] = useState('')
  const [Price, SetPrice] = useState()
  const [Stock, SetStock] = useState('')
  const [Discount, SetDiscount] = useState()
  const { theme } = useContext(ThemeContext);
  const { CategoryId } = useContext(CategoryContext)
  const CreateProductHandler = async (event) => {
    event.preventDefault();
    const data = {
      name: Name,
      description: Description,
      price: Price,
      stock: Stock,
      discount: Discount,
      categoryId: CategoryId
    }
    try {
      await api.post(`/api/categories/${CategoryId}/products`, data);
      fetchProducts();
    } catch (error) {
    }
  }


  return (
    <div>
      <button className={`${theme === 'dark' ? 'dark-Button-Add-Product' : 'Button-Add-Product'}`} onClick={() => SetButtonAddProduct(!ButtonAddProduct)}>
        {<FontAwesomeIcon icon={faPlus} style={{ color: "#da4d10", }} />}
      </button>
      <div className={`Container-Add-Product ${ButtonAddProduct ? 'active' : ''}`}>
        <form onSubmit={CreateProductHandler}>
          <label htmlFor='name-Product'>Name Product</label>
          <input type='text' id="name-Product" placeholder='Name Product' onChange={(e) => SetName(e.target.value)}></input>

          <label htmlFor='description-Product'>Description</label>
          <input type='text' id="description-Product" placeholder='Description' onChange={(e) => SetDescription(e.target.value)}></input>

          <label htmlFor='price-Product'>Price</label>
          <input type='number' id="price-Product" placeholder='Price' onChange={(e) => SetPrice(e.target.value)}></input>

          <label htmlFor='stock-Product' placeholder='Stock'>Stock</label>
          <input type='text' id="stock-Product" placeholder='Name Product' onChange={(e) => SetStock(e.target.value)}></input>

          <label htmlFor='discount-Product'>Discount</label>
          <input type='int' id="discount-Product" placeholder='Discount' onChange={(e) => SetDiscount(e.target.value)}></input>

          <button>Create</button>
        </form>
      </div>
    </div>
  );
}

export default CreaateProduct;
