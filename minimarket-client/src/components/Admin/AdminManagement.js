import React, { useState, useEffect } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEdit, faTrash } from '@fortawesome/free-solid-svg-icons';
import './AdminManagement.css'; 


const AdminManagement = () => {
  const [admin, setAdmin] = useState([]);
  const [products, setProducts] = useState([]);
  const [orders, setOrders] = useState([]);
  const [formData, setFormData] = useState({
    name: '',
    email: '',
    role: 'SuperAdmin',
  });
  const [editMode, setEditMode] = useState(false);
  const [editId, setEditId] = useState(null);

  useEffect(() => {
    fetchAdmin();
    fetchProducts();
    fetchOrders();
  }, []);

  const fetchAdmin = async () => {
    try {
      const response = await fetch('/api/admin');
      const data = await response.json();
      setAdmin(data);
    } catch (error) {
      console.error('Error fetching admins:', error);
    }
  };

  const fetchProducts = async () => {
    try {
      const response = await fetch('/api/products');
      const data = await response.json();
      setProducts(data);
    } catch (error) {
      console.error('Error fetching products:', error);
    }
  };

  const fetchOrders = async () => {
    try {
      const response = await fetch('/api/orders');
      const data = await response.json();
      setOrders(data);
    } catch (error) {
      console.error('Error fetching orders:', error);
    }
  };

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const endpoint = editMode
        ? `/api/${getEndpoint(editId)}/${editId}`
        : `/api/${getEndpoint(editId)}`;
      const method = editMode ? 'PUT' : 'POST';
      const response = await fetch(endpoint, {
        method,
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(formData),
      });

      const updatedData = await response.json();
      const updateFunction = getUpdateFunction(editId);

      if (editMode) {
        updateFunction(data => data.map(item => (item.id === updatedData.id ? updatedData : item)));
      } else {
        updateFunction(data => [...data, updatedData]);
      }

      setFormData({ name: '', email: '', role: 'SuperAdmin' });
      setEditMode(false);
      setEditId(null);
    } catch (error) {
      console.error('Error submitting form:', error);
    }
  };

  const handleDelete = async (id) => {
    if (window.confirm('Are you sure you want to delete this item?')) {
      try {
        const endpoint = `/api/${getEndpoint(id)}/${id}`;
        await fetch(endpoint, { method: 'DELETE' });

        const deleteFunction = getDeleteFunction(id);
        deleteFunction(data => data.filter(item => item.id !== id));
      } catch (error) {
        console.error('Error deleting item:', error);
      }
    }
  };

  const handleEdit = (item) => {
    setFormData({
      name: item.name,
      email: item.email,
      role: item.role || '',
      description: item.description || '',
      price: item.price || '',
      quantity: item.quantity || '',
      status: item.status || '',
    });
    setEditMode(true);
    setEditId(item.id);
  };

  const getEndpoint = (id) => {
    if (admin.some(admin => admin.id === id)) {
      return 'SuperAdmin';
    } else if (products.some(product => product.id === id)) {
      return 'products';
    } else if (orders.some(order => order.id === id)) {
      return 'orders';
    }
    return '';
  };

  const getUpdateFunction = (id) => {
    if (admin.some(admin => admin.id === id)) {
      return setAdmin;
    } else if (products.some(product => product.id === id)) {
      return setProducts;
    } else if (orders.some(order => order.id === id)) {
      return setOrders;
    }
    return () => {};
  };

  const getDeleteFunction = (id) => {
    if (admin.some(admin => admin.id === id)) {
      return setAdmin;
    } else if (products.some(product => product.id === id)) {
      return setProducts;
    } else if (orders.some(order => order.id === id)) {
      return setOrders;
    }
    return () => {};
  };

  return (
    <div className="admin-management">
      <h1>Other Admin and Seller </h1>
      <form onSubmit={handleSubmit}>
        <input
          type="text"
          name="name"
          placeholder="Name"
          value={formData.name}
          onChange={handleInputChange}
          required
        />
        <input
          type="email"
          name="email"
          placeholder="Email"
          value={formData.email}
          onChange={handleInputChange}
          required
        />
        <input
          type="text"
          name="role"
          placeholder="Role"
          value={formData.role}
          onChange={handleInputChange}
        />
       
        <button type="submit">{editMode ? 'Update' : 'Add'}</button>
      </form>


      <h2>Products</h2>
      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Name</th>
            <th>Description</th>
            <th>Price</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {products.map(product => (
            <tr key={product.id}>
              <td>{product.id}</td>
              <td>{product.name}</td>
              <td>{product.description}</td>
              <td>{product.price}</td>
              <td>
                <button onClick={() => handleEdit(product)}>
                  <FontAwesomeIcon icon={faEdit} />
                </button>
                <button onClick={() => handleDelete(product.id)}>
                  <FontAwesomeIcon icon={faTrash} />
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      <h2>Orders</h2>
      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Product</th>
            <th>User</th>
            <th>Quantity</th>
            <th>Status</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {orders.map(order => (
            <tr key={order.id}>
              <td>{order.id}</td>
              <td>{order.product}</td>
              <td>{order.user}</td>
              <td>{order.quantity}</td>
              <td>{order.status}</td>
              <td>
                <button onClick={() => handleEdit(order)}>
                  <FontAwesomeIcon icon={faEdit} />
                </button>
                <button onClick={() => handleDelete(order.id)}>
                  <FontAwesomeIcon icon={faTrash} />
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default AdminManagement;


