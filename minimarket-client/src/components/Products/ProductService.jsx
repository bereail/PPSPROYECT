// PONER CONEXION CON EL BACK CUANDO LO INTENTE ME TIRA ERROR Y PUSE LA API DE CLASES 
//https://fakestoreapi.com/products


export const getAllProducts = async () => {
    
    const data = await fetch('');
    const products = await data.json();
    return products;

};