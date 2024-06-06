import { useEffect, useState } from 'react';
import api from '../../../api';

const GetUserbyid = () => {
  const [user, setUser] = useState(null);

  useEffect(() => {
    const fetchUser = async () => {
      try {
        const response = await api.get(`/api/users/profile`);
        setUser(response.data);
      } catch (error) {
        console.log("Error fetching user", error);
      }
    };

    fetchUser();
  }, []);

  return user;
};

export default GetUserbyid;
