
import api from "../../../api";



const GetProductsByCategory = async (CategoryId, isactive, setProducts, setError, isAscendingOption, SortbydOption, pageNumber) => {
    
  try {
    const response = await api.get(`/api/categories/${CategoryId}/products`, {
        params: { 
            isActive: isactive,
            isAscending: isAscendingOption,
            sortBy: SortbydOption,
            pageNumber: pageNumber
          }
    });
    setProducts(response.data.products);
   
    setError(null);
  } catch (error) {
    setError(error)
  }
};

export default GetProductsByCategory;
