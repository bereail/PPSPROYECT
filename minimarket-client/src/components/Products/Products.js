import React, { useContext, useEffect, useState } from 'react';
import './Products.css';
import { GetRoleByUser } from '../../GetRoleByUser';
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPencil, faTrashCan, faTrashCanArrowUp, faHeart } from "@fortawesome/free-solid-svg-icons";
import api from '../../api';
import { CategoryContext } from '../Context/CategoryContext';
import CreateCategory from './Crud/CreateCategory';
import CreaateProduct from './Crud/CreaateProduct';
import DisabelProduct from './Crud/DisabelProduct';
import RestoreProducts from './Crud/RestoreProducts';
import GetProductsByCategory from './Crud/GetProducstByCategory';
import iconerror from '../Image/Icon-product-error.png'
import { AuthContext } from '../Context/AuthContext';
import StoreProducts from './StoreProducts';
import GetProductsFavorite from '../Favorite/GetProductsFavorite';
const Products = () => {
  const [Products, setProducts] = useState([]);
  const [quantities, setQuantities] = useState({});
  const [RoleUser, SetRolUser] = useState('');
  const [hoveredProduct, setHoveredProduct] = useState(null);
  const [isActive, SetisActive] = useState();
  const [error, setError] = useState(null);
  const [SortbydOption, SetSortbydOption] = useState('');
  const [isAscendingOption, SetisAscendingOption] = useState();
  const { CategoryId } = useContext(CategoryContext);
  const {user, userEmail} = useContext(AuthContext);
  const [showCartUpdate, setShowCartUpdate] = useState(false);
    useEffect(() => {
    const fetchProducts = async () => {
      try {
        const response = await api.get("/api/products/offers");;
        setProducts(response.data);
        setError(null)
      } catch (error) {
        console.error('Error fetching products:', error);
      }
    };
    fetchProducts();
  }, []);
  useEffect(() => {
    if(CategoryId !== null){
    GetProductsByCategory(CategoryId, isActive, setProducts, setError, isAscendingOption,SortbydOption ); // Utiliza el nuevo componente
    }
    
  }, [CategoryId, isActive, isAscendingOption]);
 
  useEffect(() => {
    SetisAscendingOption('');
  }, [SortbydOption]);



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
  const favoriteHandler = (product) => {
    StoreProducts('Favorite', product, quantities, userEmail);
}
  const AddCartHandler = (product) => {  
    const success = StoreProducts('Cart', product, quantities, userEmail);
    if (success) {
      setShowCartUpdate(true);
      setTimeout(() => {
        setShowCartUpdate(false);
      }, 2000); 
    } 
  };

  
  return (
    <div>
        {showCartUpdate && <div className="cart-update-notification">Cart updated successfully!</div>} 

      <div className='Select-order'>
        <p style={{ fontSize: '50px', marginLeft: '50px' }}>Products</p>
        {CategoryId !== null && <>
        <select id="opciones" name="opciones" value={SortbydOption} onChange={(e)=>{SetSortbydOption(e.target.value)}}>
          <option value='' disabled selected>Sort by:</option>
          <option value="Name">Name</option>
          <option value="Price">Price</option>
          <option value="Discount">Discount</option>
        </select>
        {SortbydOption && 
        <select id="opciones" name="opciones"  value={isAscendingOption} onChange={(e)=>{SetisAscendingOption(e.target.value)}}>
          <option value='' disabled selected>Sort order</option>
          <option value={true}>Ascending</option>
          <option value={false}>descending</option>
        </select>
        }
        </>}
        
      </div>
      
      {RoleUser === 'Seller' && CategoryId !== null && <button className='Button-Desactive-Product' onClick={() => { SetisActive(!isActive) }}>Diabel Products</button>}

      <div style={{ display: 'flex', flexWrap: 'wrap' }}>

        {!error && Products.map(product => (
          <div key={product.id} onMouseEnter={() => setHoveredProduct(product.id)} onMouseLeave={() => { setHoveredProduct(null) }}>
            <div className={product.isActive === false ? 'Container-Products-Disabel' : 'Container-Products'}>
              <div style={{ display: 'flex', position: 'flex 1' }}>
                {RoleUser === 'Seller' && <FontAwesomeIcon icon={faPencil} style={{ marginLeft: '15px' }} />}
                <h5 className="Product-Name"  style={{ textAlign: 'center', flex: 1 }}>
                  {hoveredProduct === product.id ? product.name : `${product.name.slice(0, 20)}${product.name.length > 20 ? '...' : ''}`}
                </h5>
                {RoleUser === 'Customer' && <GetProductsFavorite product={product} userEmail={userEmail} favoriteHandler={favoriteHandler} />}                
                {RoleUser === 'Seller' && product.isActive && <FontAwesomeIcon icon={faTrashCan} style={{ marginLeft: '10px' }} onClick={() => (DisabelProduct(product.id))} />}
                {RoleUser === 'Seller' && !product.isActive && <FontAwesomeIcon icon={faTrashCanArrowUp} style={{ marginLeft: '10px' }} onClick={() => (RestoreProducts(product.id))} />}
              </div>
              {hoveredProduct === product.id && <p>{product.description}</p>}
              <p className='Product-Price'>US${product.price}</p>
              <p className='Product-Offer'>{product.discount !== 0 && `You take it for US$${product.price * (1 - product.discount / 100).toFixed(2)}`}</p>
              {(RoleUser === 'Customer' || !RoleUser) && <>
              <div className='Container-Button-Products'>
                <button onClick={() => handleQuantityChange(product.id, Math.max(quantities[product.id] - 1, 1))}>-</button>
                <input min="1" value={quantities[product.id] || 1} onChange={(e) => handleQuantityChange(product.id, parseInt(e.target.value) || 1)} />
                <button onClick={() => handleQuantityChange(product.id, quantities[product.id] + 1)}>+</button>
              </div>
             <button className='Add-Product' onClick={() => AddCartHandler(product)}>Add</button>
             </>}
            </div>
          </div>
        ))}

      </div>
      {error && <div>
        <p className='Error-Products'>There are no products in this category.</p>
        <img style={{ width: '350px' }} src={iconerror}></img>
      </div>}

      {RoleUser === 'Seller' && <div className='Products-Seller'>
        {CategoryId !== null && <CreaateProduct></CreaateProduct>}
        <CreateCategory></CreateCategory>
      </div>}
    </div>
  );
};

export default Products;