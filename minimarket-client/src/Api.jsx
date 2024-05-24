import axios from 'axios';

const getToken = () => {
    return window.localStorage.getItem('LoggedUser'); 
};

const Api = () => {
    const token = getToken();
    const api = axios.create({
        baseURL: 'https://localhost:7191',
        headers: {
            'Authorization': `Bearer ${token}`
        }
    });
    
    return api;
};

export default Api;