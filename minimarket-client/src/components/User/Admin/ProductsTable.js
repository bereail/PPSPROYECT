import React, { useEffect, useState } from 'react';
import GetProductBySearch from '../../Products/Crud/GetProductBySearch';
import './AdminTable.css';
import ModifyProducts from '../../Products/Crud/ModifyProducts';
import DisabelProduct from '../../Products/Crud/DisabelProduct';
import RestoreProducts from '../../Products/Crud/RestoreProducts';
import usePagination from '../../CustomHook/usePagination'; 
import DeleteProdcut from '../../Products/Crud/DeleteProdcut';
import { toast } from 'react-toastify';

const ProductsTable = () => {
  const SearchValue = '';
   
  const [error, setError] = useState();
  const [successMessage, setSuccessMessage] = useState('');
  const [Products, setProducts] = useState([]);
  const [EditProductId, setEditProductId] = useState(null);
  const { pageNumber, PaginationButtons } = usePagination(); 
  const [InputValue, setInputValue] = useState({
    name: '',
    description: '',
    price: null,
    stock: null,
    discount: null
  });


  const fetchData = async () => {
    GetProductBySearch(setProducts, setError, SearchValue, pageNumber);
  };

  useEffect(() => {
    fetchData();
  }, [pageNumber]);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setInputValue(prevState => ({
      ...prevState,
      [name]: value
    }));
  };

  const handleProductEdit = (productId) => {
    setEditProductId(productId);
  };

  const handleModifyProduct = async(productId) => {
     try {
      if (InputValue.name !== '' && InputValue.description !== '' && InputValue.price !== null && InputValue.stock !== null && InputValue.discount !== null) {
        const response = await ModifyProducts(InputValue, productId);
      if (response && response.status === 200) {
        fetchData();
      
        toast.success('Product edit successfuly');
      }
    }
    } catch (error) {
      toast.error('Error, please try again');
     
    }
  };

  const handleDeleteProduct = async(productid) => {
    try {
      const response = await DeleteProdcut(productid);
      if (response && response.status === 204) {
        fetchData();
        toast.success('Product deletd');
      }
    } catch (error) {
      toast.error('Error, please try again');
    }
  };

  const handleDisabelProduct = async (productid) => {
    try {
      const response = await DisabelProduct(productid);
      if (response && response.status === 200) {
        fetchData();
        toast.success('Product desabled');
      }
    } catch (error) {
      toast.error('Error try again');
    }
  };

  const handleActiveProduct = async (productid) => {
    try {
      const response = await RestoreProducts(productid);
      if (response && response.status === 200) {
        fetchData();
        toast.success('Product activated');
      }
    } catch (error) {
      toast.error('Error try again');
      
    }
  };

  return (
    <div className="products-table">
      {successMessage && <div className="success-message">{successMessage}</div>}
      {Products.length > 0 ? (
        <>
          <table className='Container-Table'>
            <thead>
              <tr>
                <th>Product name</th>
                <th>Description</th>
                <th>Price</th>
                <th>Stock</th>
                <th>Discount</th>
                <th>Edit</th>
                <th>Delete</th>
                <th>Disable</th>
              </tr>
            </thead>
            <tbody>
              {Products.map((product, index) => (
                <tr key={index} className='Value-Table' style={{ backgroundColor: product.isActive ? '' : '#b5b8b5' }}>
                  <td><input type="text" placeholder={product.name} disabled={EditProductId !== product.id} onChange={(e) => handleInputChange(e, product.id)} name="name" /></td>
                  <td><input type="text" placeholder={product.description} disabled={EditProductId !== product.id} onChange={(e) => handleInputChange(e, product.id)} name="description" /></td>
                  <td><input type="text" placeholder={product.price} disabled={EditProductId !== product.id} onChange={(e) => handleInputChange(e, product.id)} name="price" /></td>
                  <td><input type="text" placeholder={product.stock} disabled={EditProductId !== product.id} onChange={(e) => handleInputChange(e, product.id)} name="stock" /></td>
                  <td><input type="text" placeholder={product.discount} disabled={EditProductId !== product.id} onChange={(e) => handleInputChange(e, product.id)} name="discount" /></td>
                  {EditProductId !== product.id && <td><button className='Button-Edit-Table' onClick={() => handleProductEdit(product.id)}>Edit</button></td>}
                  {EditProductId === product.id && <td><button className='Button-Edit-Table' onClick={() => { handleModifyProduct(product.id) }}>Send</button></td>}
                  {product.isActive ? <td><button className='Button-Disabel-Table' onClick={() => { handleDisabelProduct(product.id) }}>Disable</button></td> : <td><button className='Button-Active-Table' onClick={() => { handleActiveProduct(product.id) }}>Active</button></td>}
                  <td><button className='Button-Delete-Table' onClick={() => { handleDeleteProduct(product.id) }}>Delete</button></td>
                </tr>
              ))}
            </tbody>
          </table>
          <PaginationButtons /> 
        </>
      ) : (
        <div className="no-products">No hay productos</div>
      )}
    </div>
  );
};

export default ProductsTable;
