import api from '../../api';

const GetOrder = async () => {
    try {
        const response = await api.get('/api/orders');
        return response.data;
    } catch (error) {
        console.log('error fetching order', error);
        return null;
    }
};

export default GetOrder;
