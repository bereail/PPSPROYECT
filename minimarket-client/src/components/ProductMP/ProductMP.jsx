import "./ProductMP.css";
import img from "../../../src/components/Image/dibujo.jpg";
import { initMercadoPago, Wallet } from '@mercadopago/sdk-react'
import axios from "axios"
import { useState } from "react";

const ProductMP = () => {
  const [preferenceId, setPreferenceId] = useState(null);
  const [productDetails, setProductDetails] = useState({
    title: "",
    quantity: 1,
    price: 0
  });

  // Inicializando (PUBLIC_KEY)
  initMercadoPago('APP_USR-70da26e9-4bcb-4a7d-b1db-835f42ae57a2', { 
    locale: "es-AR"
  });

  // Enviamos los datos del producto y crea el id de la preferencia
  const createPreference = async () => {
    try {
      const response = await axios.post("https://localhost:7191/Payment/create_preference", productDetails);
      
      const {id} = response.data;
      return id;      
    } catch (error) {
      console.log(error);
    }
  };

  // Invoca la función createPreference y guarda el id
  const handleBuy = async () => {
    const id = await createPreference();
    if(id) {
      setPreferenceId(id);
    }
  }

  // Maneja el cambio en los inputs del formulario
  const handleChange = (e) => {
    const { name, value } = e.target;
    setProductDetails({
      ...productDetails,
      [name]: value
    });
  }

  return (
    <div className="card-product-container">
      <div className="card-product">
        <div className="card">
          <img src={img} alt="Product Image" />  
          <h3>Product</h3>
          <form>
            <label>
              Title:
              <input type="text" name="title" value={productDetails.title} onChange={handleChange} />
            </label>
            <label>
              Quantity:
              <input type="number" name="quantity" value={productDetails.quantity} onChange={handleChange} />
            </label>
            <label>
              Price:
              <input type="number" name="price" value={productDetails.price} onChange={handleChange} />
            </label>
          </form>
          <p className="price">${productDetails.price}</p>
          <button onClick={handleBuy}>Buy</button>      
          {preferenceId &&  <Wallet initialization={{ preferenceId: preferenceId}}  customization={{ texts:{ valueProp: 'smart_option'}}} /> }            
        </div>
      </div>
    </div>
  );
};

export default ProductMP;
