import axios from 'axios';


const api = axios.create({
    baseURL: 'https://localhost:7191',
    headers: {
        'Content-Type': 'application/json',
    }
});

api.interceptors.request.use(config => {
    const token = localStorage.getItem('LoggedUser');
    console.log(token);
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
}, error => {
    return Promise.reject(error);
});



// const getToken = () => {
//     return window.localStorage.getItem('LoggedUser'); 
// };

// const   api = () => {
//     const token = getToken();
//     const api = axios.create({
//         baseURL: 'https://localhost:7191',
//         headers: {
//             Authorization: `Bearer ${token}`
//         }
//     });
    
//     return api;
// };

export default api;