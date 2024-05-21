//Repositorio de prueba hasta que este terminado el back
import React from "react";

const Products = [
    {
        id: 1,
        name: 'bebida',
        description: 'bebida',
        price: 20.99,
        stock: 100,
        discount: 5,
        isActive: true,
        deactivationTime: null,
        categoryId: 1,
        category: 'bebida',
        details: [{ id: 1, orderId: 1, quantity: 2 }],
      },
      {
        id: 2,
        name: 'limpieza',
        description: 'limpieza',
        price: 15.49,
        stock: 50,
        discount: 0,
        isActive: true,
        deactivationTime: null,
        categoryId: 2,
        category: 'limpieza',
        details: [{ id: 2, orderId: 2, quantity: 1 }],
      },
      {
        id: 2,
        name: 'higiene',
        description: 'higiene',
        price: 15.49,
        stock: 50,
        discount: 0,
        isActive: true,
        deactivationTime: null,
        categoryId: 2,
        category: 'higiene',
        details: [{ id: 2, orderId: 2, quantity: 1 }],
      },
      {
        id: 2,
        name: 'Produ',
        description: 'tecnologia',
        price: 15.49,
        stock: 50,
        discount: 0,
        isActive: true,
        deactivationTime: null,
        categoryId: 2,
        category: 'tecnologia',
        details: [{ id: 2, orderId: 2, quantity: 1 }],
      },
      // Agrega más productos de ejemplo según sea necesario
    ];
    
    // Repositorio ficticio de productos
    const ProductRepository = {

      getAllProducts: function () {
        return Products;
      },
    
      
    };
    
    export default ProductRepository;