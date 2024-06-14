import React, { useContext, useEffect, useState } from 'react';
import './Products.css';
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPencil, faTrashCan, faTrashCanArrowUp, faHeart } from "@fortawesome/free-solid-svg-icons";
import api from '../../api';
import FotoMensaje from '../Image/FotoMensaje.png'
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
import GetProductsByOffers from './Crud/GetProductsByOffers';
import { SearchContext } from '../Context/SearchContext';
import GetProductBySearch from './Crud/GetProductBySearch';
import ModifyProducts from './Crud/ModifyProducts';
import CreateImageProduct from './Crud/CreateImageProduct';
import DeleteImageProduct from './Crud/DeleteImageProduct';
const Products = () => {
  const [Products, setProducts] = useState([]);
  const [quantities, setQuantities] = useState({});
  const [hoveredProduct, setHoveredProduct] = useState(null);
  const [isActive, SetisActive] = useState();
  const [error, setError] = useState(null);
  const [SortbydOption, SetSortbydOption] = useState('');
  const [isAscendingOption, SetisAscendingOption] = useState();
  const { CategoryId } = useContext(CategoryContext);
  const { user, userEmail, role } = useContext(AuthContext);
  const [showCartUpdate, setShowCartUpdate] = useState(false);
  const { SearchValue } = useContext(SearchContext)
  const [editingProductId, setEditingProductId] = useState(null);
  const [InputValue, setInputValue] = useState({
    name: '',
    description: '',
    price: null,
    stock: 100,
    discount: null
  });
  const [pageNumber, SetpageNumer] = useState(1)
  useEffect(() => {
    GetProductsByOffers(setProducts, setError)

  }, []);


  useEffect(() => {
    if (CategoryId !== null) {
      GetProductsByCategory(CategoryId, isActive, setProducts, setError, isAscendingOption, SortbydOption, pageNumber); 
    }

  }, [CategoryId, isActive, isAscendingOption, pageNumber]);

  useEffect(() => {
    if (SearchValue) {
      GetProductBySearch(setProducts, setError, SearchValue)
    }
  }, [SearchValue])

  useEffect(() => {
    SetisAscendingOption('');
  }, [SortbydOption]);


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

  //Editar producto
  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setInputValue(prevState => ({
      ...prevState,
      [name]: value
    }));
  }

  const handleModifyProduct = (productId) =>{   
      ModifyProducts(InputValue, productId);
  }

  return (
    <div>
      {showCartUpdate && <div className="cart-update-notification">Cart updated successfully!</div>}

      <div className='Select-order'>
        <img src={FotoMensaje} className='Image-MensageProducts'></img>
        {CategoryId !== null && <>
          <select id="opciones" name="opciones" value={SortbydOption} onChange={(e) => {SetSortbydOption(e.target.value)}}>
            <option value='' disabled selected>Sort by:</option>
            <option value="Name">Name</option>
            <option value="Price">Price</option>
            <option value="Discount">Discount</option>
          </select>
          {SortbydOption &&
            <select id="opciones" name="opciones" value={isAscendingOption} onChange={(e) => { SetisAscendingOption(e.target.value) }}>
              <option value='' disabled selected>Sort order</option>
              <option value={true}>Ascending</option>
              <option value={false}>descending</option>
            </select>
          }
        </>}

      </div>
      {role === 'Seller' && CategoryId !== null && <button className='Button-Desactive-Product' onClick={() => { SetisActive(!isActive) }}>Diabel Products</button>}

      <div style={{ display: 'flex', flexWrap: 'wrap' }}>

        {!error && Products.map(product => (
          <div key={product.id} onMouseEnter={() => setHoveredProduct(product.id)} onMouseLeave={() => { setHoveredProduct(null) }}>
            <div className={product.isActive === false ? 'Container-Products-Disabel' : 'Container-Products'}>
              {editingProductId !== product.id ?
                <div style={{ display: 'flex', position: 'flex 1' }}>
                  {role === 'Seller' && <FontAwesomeIcon icon={faPencil} style={{ marginLeft: '15px' }} onClick={(() => { setEditingProductId(editingProductId === product.id ? null : product.id) })} />}
                  <h5 className="Product-Name" style={{ textAlign: 'center', flex: 1 }}>
                    {hoveredProduct === product.id ? product.name : `${product.name.slice(0, 20)}${product.name.length > 20 ? '...' : ''}`}
                  </h5>
                  {role === 'Customer' && <GetProductsFavorite product={product} userEmail={userEmail} favoriteHandler={favoriteHandler} />}
                  {role === 'Seller' && product.isActive && <FontAwesomeIcon icon={faTrashCan} style={{ marginLeft: '10px' }} onClick={() => (DisabelProduct(product.id))} />}
                  {role === 'Seller' && !product.isActive && <FontAwesomeIcon icon={faTrashCanArrowUp} style={{ marginLeft: '10px' }} onClick={() => (RestoreProducts(product.id))} />}
                </div>
                : <div>
                  <input type='text' className="Product-Edit" name='name' placeholder={product.name} onChange={handleInputChange}></input>
                </div>
              }


              {editingProductId !== product.id ? (
                <>
                  {product.image && <img src={product.image.imageUrl} alt={product.name} className='Product-Image' />}
                  {hoveredProduct === product.id && <p>{product.description}</p>}
                  <p className='Product-Price'>US${product.price}</p>
                  <p className='Product-Offer'>{product.discount !== 0 && `You take it for US$${product.price * (1 - product.discount / 100).toFixed(2)}`}</p>
                </>
              ) : (<>
                <input type="text" className='Product-Edit' name='description' placeholder={product.description} onChange={handleInputChange}/>
                <input type="number" className='Product-Edit' name='price' placeholder={product.price} onChange={handleInputChange}/>
                <input type="number"  className='Product-Edit' name='discount' placeholder={product.discount} onChange={handleInputChange}/>
                <input type="number"  className='Product-Edit'name='stock' placeholder="Stock" onChange={handleInputChange}/>
                <button className='Product-Edit-button' onClick={()=>{handleModifyProduct(product.id)}}>Enviar</button>
                {!product.image ? <CreateImageProduct productId={product.id} />: <DeleteImageProduct productId={product.id} ></DeleteImageProduct>}
                </>)}
              
              {(role === 'Customer' || !role) && <>
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
        {CategoryId !== null && <button onClick={()=>{SetpageNumer(pageNumber - 1)}}>Volver pagina</button>}
        {CategoryId !== null && <button onClick={()=>{SetpageNumer(pageNumber + 1)}}>Pasar pagina</button>}
         {role === 'Seller' &&<> {CategoryId !== null && <CreaateProduct></CreaateProduct>}</>}
      </div>
      {error && <div>
        <p className='Error-Products'>There are no products in this category.</p>
        <img style={{ width: '350px' }} src={iconerror}></img>
      </div>}

      {role === 'Seller' && <div className='Products-Seller'>
        <CreateCategory></CreateCategory>
      </div>}
    </div>
  );
};

export default Products;