import React, { useContext, useEffect, useState } from 'react';
import './Products.css';
import { GetRoleByUser } from '../../GetRoleByUser';
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPencil, faTrashCan } from "@fortawesome/free-solid-svg-icons";
import Api from '../../Api';
import { CategoryContext } from '../Context/CategoryContext';
import { Alert } from 'react-bootstrap';
import { useAsyncError } from 'react-router-dom';
const Products = ( ) => {
  const [Products, setProducts] = useState([]);
  const [quantities, setQuantities] = useState({});
  const [RoleUser, SetRolUser] = useState('');
  const [ButtonCategory, SetButtonCategory] = useState(false);
  const [ValueCategory, SetValueCategory] = useState('')
  const [hoveredProduct, setHoveredProduct] = useState(null);
  const [CategoryError, SetCategoryError] = useState(false)
  
  const {CategoryId } = useContext(CategoryContext)
  useEffect(() => {
    const fetchProductsCategory = async () => {
      try {
        const api = Api();
        const response = await api.get(`/api/categories/${CategoryId}/products`); // Aquí utilizamos comillas 
        setProducts(response.data.products)
      } catch (error) {
        console.error('Error fetching products category:', error);
      }
    };

    fetchProductsCategory();
  
  }, [CategoryId]);

  useEffect(() => {
    const fetchProducts = async () => {
      try {
        const api = Api();
        const response = await api.get("/api/products/offers");;
        setProducts(response.data);
      } catch (error) {
        console.error('Error fetching products:', error);
      }
    };
    fetchProducts();
  }, []);

  useEffect(() => {
    const role = GetRoleByUser()
    SetRolUser(role);
  }, [])


  useEffect(() => {
    // Inicializar las cantidades solo cuando Products cambie
    const initialQuantities = {};
    Products.forEach(product => {
      initialQuantities[product.id] = 1;
    });
    setQuantities(initialQuantities);
  }, [Products]);

  // Actualiza la cantidad solo para un Producto
  const handleQuantityChange = (productId, value) => {
    setQuantities(prevQuantities => ({
      ...prevQuantities,
      [productId]: value
    }));
  };

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
      }
    } catch (error) {
      SetCategoryError(true)
      console.error('Error add category:', error);
    }
  }

  //IMPLEMENTAR LOGICA DE CARRITO
  const AddCartHandler = (product) => {
    alert(`Se añadieron ${quantities[product.id]} unidades de ${product.name}`);

  }


  return (
    <div>
      <p style={{ fontSize: '50px' }}>Products</p>
      <div style={{ display: 'flex', flexWrap: 'wrap' }}>
        {Products.map(product => (
          <div key={product.id} onMouseEnter={() => setHoveredProduct(product.id)} onMouseLeave={()=>{setHoveredProduct(null)}}>
            <div className='Container-Products'>
              <div style={{ display: 'flex', position: 'flex 1' }}>
                {RoleUser === 'Seller' && <FontAwesomeIcon icon={faPencil} style={{ marginLeft: '15px' }} />}
                <h5 style={{ textAlign: 'center', flex: 1 }}>
                  {hoveredProduct === product.id ? product.name : `${product.name.slice(0, 20)}${product.name.length > 20 ? '...' : ''}`}
                </h5>
                {RoleUser === 'Seller' && <FontAwesomeIcon icon={faTrashCan} style={{ marginLeft: '10px' }} />}
              </div>
              {hoveredProduct === product.id && <p>{product.description}</p>}
              <p className='Product-Price'>${product.price}</p>
              <p className='Product-Offer'>  {product.discount !== 0 && `With discount: ${product.price * (1 - product.discount / 100).toFixed(2)}`}</p>
              <div className='Container-Button-Products'>
                <button onClick={() => handleQuantityChange(product.id, Math.max(quantities[product.id] - 1, 1))}>-</button>
                <input min="1" value={quantities[product.id] || 1} onChange={(e) => handleQuantityChange(product.id, parseInt(e.target.value) || 1)} />
                <button onClick={() => handleQuantityChange(product.id, quantities[product.id] + 1)}>+</button>
              </div>
              <button className='Add-Product' onClick={() => (AddCartHandler(product))}>Add</button>
            </div>
          </div>
        ))}
      </div>
      {RoleUser === 'Seller' &&<div>
      <button className='Button-Add-Category' onClick={() => (SetButtonCategory(!ButtonCategory))}>Add Category</button>
      {ButtonCategory && 
        <div className='Container-Add-Category'>
          <form onSubmit={HandleSubmitCategory}>
            <label htmlFor="name-category">Name category</label>
            <input type="text" id="name-category" name="name-category" onChange={(e) => (SetValueCategory(e.target.value))}></input>
            <button>Add</button>
          </form>
         {CategoryError && <p className='Error'>Could not add category</p>}
        </div>}
        </div>
      }
      
    </div>
  )
}

export default Products
