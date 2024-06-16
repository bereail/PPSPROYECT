import React, { useEffect, useState } from 'react'
import GetProductBySearch from '../../Products/Crud/GetProductBySearch'
import './ProductTable.css'
import ModifyProducts from '../../Products/Crud/ModifyProducts'
const ProductsTable = () => {
    const [error, setError] = useState()
    const [Products, setProducts] = useState([])
    const [EditProductId, setEditProductId] = useState(null);
    const [pageNumber, SetpageNumer] = useState(1)
    const [InputValue, setInputValue] = useState({
        name: '',
        description: '',
        price: null,
        stock: null,
        discount: null
      });
      useEffect(() => {
        GetProductBySearch(setProducts, setError, pageNumber)

    }, [pageNumber])
      const handleInputChange = (e) => {
        const { name, value } = e.target;
        setInputValue(prevState => ({
          ...prevState,
          [name]: value
        }));
      }
      const handleProductEdit = (productId) =>{
        setEditProductId(productId)
      }

      const handleModifyProduct = (productId) =>{  
        if (InputValue.name !== '' && InputValue.description !== '' && InputValue.price !== null && InputValue.stock !== null && InputValue.discount !== null) {    
            ModifyProducts(InputValue, productId);
        }
          
      }
      const handleDeleteProduct = (productid) =>{
        alert(`LLamar funcion endpoint cuando este definido para: ${productid}`)
      }


    return (
        <div className="products-table">
            {Products.length > 0 ? (<>
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
                        </tr>
                    </thead>
                    <tbody>
                        {Products.map((product, index) => (
                           
                            <tr key={index} className='Value-Table'>
                                 
                                <td><input type="text" placeholder={product.name} disabled={EditProductId !== product.id} onChange={(e) => handleInputChange(e, product.id)} name="name"/></td>
                                <td>
                                <input type="text" placeholder={product.description}  disabled={EditProductId !== product.id} onChange={(e) => handleInputChange(e, product.id)} name="description"/></td>
                                <td>
                                <input type="text" placeholder={product.price}  disabled={EditProductId !== product.id} onChange={(e) => handleInputChange(e, product.id)} name="price"/></td>
                                <td><input type="text" placeholder={product.stock}  disabled={EditProductId !== product.id} onChange={(e) => handleInputChange(e, product.id)}  name="stock"/></td>
                                <td><input type="text" placeholder={product.discount}  disabled={EditProductId !== product.id} onChange={(e) => handleInputChange(e, product.id)} name="discount"/></td>
                                {EditProductId !== product.id && <button  className= 'Button-Edit-Table' onClick={() => handleProductEdit(product.id)}>Edit</button>}
                                {EditProductId === product.id && <button  className= 'Button-Edit-Table'  onClick={()=>{handleModifyProduct(product.id)}}>Send</button>}
                                <td><button className= 'Button-Delete-Table' onClick={()=>{handleDeleteProduct(product.id)}}>Delete</button></td>
                            </tr>
                            
                        ))}
                    </tbody>
                </table>
                {pageNumber != 1 && <button className='Page-button' onClick={()=>{SetpageNumer(pageNumber - 1)}}>previous page</button>}
                { <button className='Page-button' onClick={()=>{SetpageNumer(pageNumber + 1)}}>Next page</button>}
                </>
            ) : (
                <div className="no-products">No hay productos</div>
            )}
        </div>
    );
}

export default ProductsTable
