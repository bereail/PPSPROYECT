import React, { useState, useEffect } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEdit, faTrash, faPlus } from '@fortawesome/free-solid-svg-icons';

const AdminManagement = () => {
  // Estado para almacenar la lista de administradores
  const [admins, setAdmins] = useState([]);

  // Estado para manejar el formulario de creación/modificación
  const [formData, setFormData] = useState({
    name: '',
    email: '',
    role: 'admin', // Por defecto, el rol es admin para los nuevos registros
  });

  // Estado para controlar si se está editando un administrador o no
  const [editMode, setEditMode] = useState(false);
  const [editAdminId, setEditAdminId] = useState(null);

  // Función para obtener la lista de administradores
  const fetchAdmins = async () => {
    try {
      // Aquí realizar la llamada a API para obtener la lista de administradores
      // Supongamos que la API devuelve una lista de objetos con propiedades id, name, email, role, etc.
      const response = await fetch('/api/admins');
      const data = await response.json();
      setAdmins(data); // Actualizar el estado con la lista de administradores
    } catch (error) {
      console.error('Error fetching admins:', error);
      // Aquí manejarías el error de acuerdo a tu lógica de manejo de errores
    }
  };

  // Función para manejar cambios en los campos del formulario
  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  // Función para enviar el formulario de creación/modificación
  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      if (editMode) {
        // Modo de edición: enviar solicitud PUT para actualizar el administrador existente
        const response = await fetch(`/api/admins/${editAdminId}`, {
          method: 'PUT',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(formData),
        });
        const updatedAdmin = await response.json();
        // Actualizar la lista de administradores después de la modificación
        setAdmins(admins.map(admin => (admin.id === updatedAdmin.id ? updatedAdmin : admin)));
      } else {
        // Modo de creación: enviar solicitud POST para crear un nuevo administrador
        const response = await fetch('/api/admins', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(formData),
        });
        const newAdmin = await response.json();
        // Agregar el nuevo administrador a la lista actual
        setAdmins([...admins, newAdmin]);
      }
      // Limpiar el formulario después de la operación exitosa
      setFormData({
        name: '',
        email: '',
        role: 'admin',
      });
      // Salir del modo de edición
      setEditMode(false);
      setEditAdminId(null);
    } catch (error) {
      console.error('Error submitting form:', error);
      // Aquí manejar el error de acuerdo a lógica de manejo de errores
    }
  };

  // Función para eliminar un administrador
  const handleDelete = async (adminId) => {
    if (window.confirm('¿Estás seguro de que quieres eliminar este administrador?')) {
      try {
        // Aquí realizar la llamada a tu API para eliminar el administrador
        await fetch(`/api/admins/${adminId}`, {
          method: 'DELETE',
        });
        // Actualizar la lista de administradores después de la eliminación
        setAdmins(admins.filter(admin => admin.id !== adminId));
      } catch (error) {
        console.error('Error deleting admin:', error);
        // Aquí manejar el error de acuerdo a lógica de manejo de errores
      }
    }
  };

  // Función para editar un administrador
  const handleEdit = (admin) => {
    // Establecer los valores actuales del administrador en el formulario de edición
    setFormData({
      name: admin.name,
      email: admin.email,
      role: admin.role,
    });
    setEditMode(true);
    setEditAdminId(admin.id);
  };

  // Efecto para cargar la lista de administradores al cargar el componente
  useEffect(() => {
    fetchAdmins();
  }, []);

  return (
    <div>
      <h1>Admin Management</h1>

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
        {editMode ? (
          <button type="submit">Update Admin</button>
        ) : (
          <button type="submit">Add Admin</button>
        )}
      </form>

      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Name</th>
            <th>Email</th>
            <th>Role</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {admins.map(admin => (
            <tr key={admin.id}>
              <td>{admin.id}</td>
              <td>{admin.name}</td>
              <td>{admin.email}</td>
              <td>{admin.role}</td>
              <td>
                <button onClick={() => handleEdit(admin)}><FontAwesomeIcon icon={faEdit} /></button>
                <button onClick={() => handleDelete(admin.id)}><FontAwesomeIcon icon={faTrash} /></button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default AdminManagement;
