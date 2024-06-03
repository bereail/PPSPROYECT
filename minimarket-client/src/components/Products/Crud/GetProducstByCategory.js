
import api from "../../../api";



const GetProductsByCategory = async (CategoryId, isactive, setProducts, setError, isAscendingOption, SortbydOption) => {
    
  try {
    const response = await api.get(`/api/categories/${CategoryId}/products`, {
        params: { 
            isActive: isactive,
            isAscending: isAscendingOption,
            sortBy: SortbydOption
          }
    });
    setProducts(response.data.products);
   
    setError(null);
  } catch (error) {
    console.error('Error fetching products category:', error);
    setError(error)
  }
};

export default GetProductsByCategory;
