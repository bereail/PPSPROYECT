import React, { useContext, useEffect, useState } from 'react';
import './Products.css';
import { GetRoleByUser } from '../../GetRoleByUser';
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPencil, faTrashCan, faTrashCanArrowUp } from "@fortawesome/free-solid-svg-icons";
import Api from '../../Api';
import { CategoryContext } from '../Context/CategoryContext';
import CreateCategory from './Crud/CreateCategory';
import CreaateProduct from './Crud/CreaateProduct';
import DisabelProduct from './Crud/DisabelProduct';
import RestoreProducts from './Crud/RestoreProducts';
import GetProductsByCategory from './Crud/GetProducstByCategory';

const Products = () => {
  const [Products, setProducts] = useState([]);
  const [quantities, setQuantities] = useState({});
  const [RoleUser, SetRolUser] = useState('');
  const [hoveredProduct, setHoveredProduct] = useState(null);
  const [isActive, SetisActive] = useState(true);
  

  const { CategoryId } = useContext(CategoryContext);
  
  useEffect(() => {
    GetProductsByCategory(CategoryId, isActive, setProducts); // Utiliza el nuevo componente
 
  }, [CategoryId, isActive]);

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




  const AddCartHandler = (product) => {
    alert(`Se añadieron ${quantities[product.id]} unidades de ${product.name}`);
  };

  return (
    <div>
      <div style={{ alignItems: 'center'}}>
        <p style={{ fontSize: '50px', marginLeft: '50px' }}>Products</p>
        {RoleUser === 'Seller' && CategoryId !== null && <button className='Button-Desactive-Product' onClick={()=>{SetisActive(!isActive)}}>Diabel Products</button>}
      </div>
      <div style={{ display: 'flex', flexWrap: 'wrap' }}>
        {Products.map(product => (
          <div key={product.id} onMouseEnter={() => setHoveredProduct(product.id)} onMouseLeave={() => { setHoveredProduct(null) }}>
            <div className={product.isActive  === false ? 'Container-Products-Disabel': 'Container-Products'}>
              <div style={{ display: 'flex', position: 'flex 1' }}>
                {RoleUser === 'Seller' && <FontAwesomeIcon icon={faPencil} style={{ marginLeft: '15px' }}  />}
                <h5 style={{ textAlign: 'center', flex: 1 }}>
                  {hoveredProduct === product.id ? product.name : `${product.name.slice(0, 20)}${product.name.length > 20 ? '...' : ''}`}
                </h5>
                {RoleUser === 'Seller'&&  product.isActive && <FontAwesomeIcon icon={faTrashCan} style={{ marginLeft: '10px' }} onClick={()=>(DisabelProduct(product.id))} />}
                {RoleUser === 'Seller'&&  !product.isActive && <FontAwesomeIcon icon={faTrashCanArrowUp} style={{ marginLeft: '10px' }} onClick={()=>(RestoreProducts(product.id))} />}
              </div>
              {hoveredProduct === product.id && <p>{product.description}</p>}
              <p className='Product-Price'>${product.price}</p>
              <p className='Product-Offer'>  {product.discount !== 0 && `With discount: ${product.price * (1 - product.discount / 100).toFixed(2)}`}</p>
              <div className='Container-Button-Products'>
                <button onClick={() => handleQuantityChange(product.id, Math.max(quantities[product.id] - 1, 1))}>-</button>
                <input min="1" value={quantities[product.id] || 1} onChange={(e) => handleQuantityChange(product.id, parseInt(e.target.value) || 1)} />
                <button onClick={() => handleQuantityChange(product.id, quantities[product.id] + 1)}>+</button>
              </div>
              <button className='Add-Product' onClick={() => AddCartHandler(product)}>Add</button>
            </div>
          </div>
        ))}
      </div>
      {RoleUser === 'Seller' && <div className='Products-Seller'>
        {CategoryId !== null &&<CreaateProduct></CreaateProduct>}
        <CreateCategory></CreateCategory>
      </div>}
    </div>
  );
};

export default Products;
