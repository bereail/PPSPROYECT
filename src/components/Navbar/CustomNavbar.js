import React from "react";
import { useState, useEffect } from "react";
import { Navbar as BootstrapNavbar, Container, Nav } from "react-bootstrap";
import Search from "../../SearhBar/Search";
import Logo from "./Logo";
import Login from "../Login/login";
import { Link, useLocation } from 'react-router-dom';

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
    <BootstrapNavbar className="navbar" style={{ backgroundImage: "linear-gradient(to right,#AFDFF3, #8AB5E8)", borderBottomLeftRadius: "15%", borderBottomRightRadius: "15%"}}>
      <Container fluid>

    
            
            <Link to='/' onclick={()=>handlePageChange('Home')}>  <Logo /> </Link> 
   
            <Link to='/' onclick={()=>handlePageChange('Home')} type="button" className="btn btn-outline-primary">  Home </Link> 
        <Nav className="flex-grow-1 d-flex justify-content-center"> {/* Utiliza flex-grow-1 para que el Search ocupe todo el espacio disponible */}
        <div>{currentPage === 'Home' && <Search />}</div>
        </Nav>
        <Nav className="ml-auto">
        
          <div onclick={()=>handlePageChange('Register')}>
            {currentPage !=='Register' && (
            <Link  to="/signupUser" type="button" className="btn btn-outline-primary"> Sing Up</Link>)}
            </div>
          <div onclick={()=>handlePageChange('Login')} >
            {currentPage !== 'Login' &&( 
            <Link to="/signin" type="button" className="btn btn-outline-primary">Sing In</Link>
            )}
            </div>
          
          <Login />
        </Nav>
      </Container>
    </BootstrapNavbar>
  );
};

export default CustomNavbar;
