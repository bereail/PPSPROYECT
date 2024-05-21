import React, { useEffect, useState } from 'react';
import './Products.css';
import { GetRoleByUser } from '../../GetRoleByUser';
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPencil, faTrashCan } from "@fortawesome/free-solid-svg-icons";
import Api from '../../Api';
const Products = () => {
  const [Products, setProducts] = useState([]);
  const [quantities, setQuantities] = useState({});
  const [RoleUser, SetRolUser] = useState('');
  const [ButtonCategory, SetButtonCategory] = useState(false);
  const [ValueCategory, SetValueCategory] = useState('')
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
        alert(response.data)
      }
    } catch (error) {
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
          <div key={product.id}>
            <div className='Container-Products'>
              <div style={{ display: 'flex', position: 'flex 1' }}>
                {RoleUser === 'Seller' && <FontAwesomeIcon icon={faPencil} style={{ marginLeft: '15px' }} />}
                <h5 style={{ textAlign: 'center' }}>{product.name}</h5>
                {RoleUser === 'Seller' && <FontAwesomeIcon icon={faTrashCan} style={{ marginLeft: '10px' }} />}
              </div>
              <p className='Product-Price'>${product.price}</p>
              <p>{product.description}</p>
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

      <button className='Button-Add-Category' onClick={() => (SetButtonCategory(!ButtonCategory))}>Add Category</button>
      {ButtonCategory &&
        <div className='Container-Add-Category'>
          <form onSubmit={HandleSubmitCategory}>
            <label htmlFor="name-category">Name category</label>
            <input type="text" id="name-category" name="name-category" onChange={(e) => (SetValueCategory(e.target.value))}></input>
            <button>Add</button>
          </form>
        </div>}
    </div>
  )
}

export default Products
