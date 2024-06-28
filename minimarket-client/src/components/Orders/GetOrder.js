import api from '../../api';

const GetOrder = async (pageNumber, isAscendingOption,SortbydOption) => {
    try {
        const response = await api.get('/api/orders', {
            params: { 
                pageNumber: pageNumber
              }
        });
        return response.data;
    } catch (error) {
        throw error;
    }
};

export default GetOrder;
