import React from "react";
import { useState, useEffect } from "react";
import { Navbar as BootstrapNavbar, Container, Nav } from "react-bootstrap";
import Search from "../../SearhBar/Search";
import Logo from "./Logo";
import Login from "../Login/login";
import { Link, useLocation } from 'react-router-dom';

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import{faCartShopping} from '@fortawesome/free-solid-svg-icons'
import FilterBar from "../FilterBar/FilterBar";

const CustomNavbar = () => {
  const { pathname } = useLocation(); 
  const [currentPage, SetCurrentPage] = useState('Home');
  
  useEffect(() => {
    switch (pathname) {
      case '/':
        SetCurrentPage('Home');
        break;
      case '/signupUser':
        SetCurrentPage('Register');
        break;
      case '/signin':
        SetCurrentPage('Login');
        break;
      default:
        SetCurrentPage('');
    }
  }, [pathname]);
  const handlePageChange =(pageName) =>{
    SetCurrentPage(pageName)
  }

  return (
    <BootstrapNavbar className="navbar" style={{ backgroundImage: "linear-gradient(to right,#507657, #3f7449)", borderBottomLeftRadius: "5%", borderBottomRightRadius: "5%"}}>
  
      <Container fluid>

     
        <Link to='/' onclick={()=>handlePageChange('Home')}>  <Logo /> </Link> 
        <Link to='/' onclick={()=>handlePageChange('Home')} type="button" className="btn btn-outline-primary">  Home </Link> 
        <Nav className="flex-grow-1 d-flex justify-content-center"> {/* Utiliza flex-grow-1 para que el Search ocupe todo el espacio disponible */}
        <div>
        <div style={{ display: 'inline-flex', alignItems: 'center' }}>{currentPage === 'Home' && <Search  />}
    
        {currentPage === 'Home' && (
         <Link to="/cart" style={{ color: 'blue' }}>
            <FontAwesomeIcon icon={faCartShopping} style={{ fontSize: '30px', marginLeft: '5px' }} />
          </Link>
          
        )}
        
        
        </div>
        
        {/* {currentPage === 'Home' && (
         <FilterBar ></FilterBar>
          
        )}
        */}
        </div>
        </Nav>
       
        <Nav className="ml-auto">
        
          <div onclick={()=>handlePageChange('Register')}>
            {currentPage !=='Register' && (
            <Link  to="/signupUser" type="button" className="btn btn-outline-primary"> Sign Up</Link>)}
            </div>
          <div onclick={()=>handlePageChange('Login')} >
            {currentPage !== 'Login' &&( 
            <Link to="/signin" type="button" className="btn btn-outline-primary">Sign In</Link>
            )}
            </div>
          
          <Login />
        </Nav>
      </Container>
    </BootstrapNavbar>
  );
};

export default CustomNavbar;
