//npm install jwt-decode


import { jwtDecode } from 'jwt-decode';


export const GetRoleByUser = () => {

     const token = window.localStorage.getItem('LoggedUser');
        if (token) {
            const decodedToken = jwtDecode(token).role;
            return decodedToken ;
        }
    
    return null;
}



