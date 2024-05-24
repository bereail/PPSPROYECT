// GetProductsByCategory.js

import Api from "../../../Api";



const GetProductsByCategory = async (CategoryId, isactive, setProducts) => {
    
  try {
    const api = Api();
    const response = await api.get(`/api/categories/${CategoryId}/products`, {
      params: { isActive: isactive }
    });
    setProducts(response.data.products);
  } catch (error) {
    console.error('Error fetching products category:', error);
  }
};

export default GetProductsByCategory;
