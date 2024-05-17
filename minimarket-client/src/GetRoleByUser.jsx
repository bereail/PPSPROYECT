//npm install jwt-decode


import React, { useEffect, useState } from 'react'
import { jwtDecode } from 'jwt-decode';

export const GetRoleByUser = () => {

     const token = window.localStorage.getItem('LoggedUser');
        if (token) {
            const decodedToken = jwtDecode(token).role;
            return decodedToken ;
        }
    
    return null;
}



