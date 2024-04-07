import React from "react";
import { useState, useEffect } from "react";
import { Navbar as BootstrapNavbar, Container, Nav } from "react-bootstrap";
import Search from "../../SearhBar/Search";
import Logo from "./Logo";
import Login from "../Login/login";
import { Link, useLocation } from 'react-router-dom';

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faCartShopping } from '@fortawesome/free-solid-svg-icons'
import FilterBar from "../FilterBar/FilterBar";

const CustomNavbar = () => {
  const [isExpanded, setIsExpanded] = useState(false);
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
      case '/cart':
        SetCurrentPage('cart');
        break;
      default:
        SetCurrentPage('');
    }
  }, [pathname]);
  const handlePageChange = (pageName) => {
    SetCurrentPage(pageName)
  }

  return (
    <BootstrapNavbar className="navbar" style={{ backgroundImage: "linear-gradient(to right,#AFDFF3, #8AB5E8)", borderBottomLeftRadius: "15%", borderBottomRightRadius: "15%" }}>

      <Container fluid style={{ maxHeight: '50px', padding: '0px' }}>


        <Link to='/' onclick={() => handlePageChange('Home')}>  <Logo /> </Link>
        <Link to='/' onclick={() => handlePageChange('Home')} type="button" className="btn btn-outline-primary">  Home </Link>
        <Nav className="flex-grow-1 d-flex justify-content-center"> {/* Utiliza flex-grow-1 para que el Search ocupe todo el espacio disponible */}

          <div style={{ display: 'inline-flex' }}>
            <div style={{ display: 'flex', alignItems: 'center' }}>
              {currentPage === 'Home' && (
                <FilterBar ></FilterBar>

              )}
            </div>
            <div style={{ display: 'inline-flex', alignItems: 'center' }}>{currentPage === 'Home' && <Search />}</div>
          </div>
        </Nav>
        {currentPage !== 'Login' && currentPage !== 'Register' && (
          <div style={{ marginRight: currentPage === 'cart' && "100px"}} className="filter-bar-container">
            <button className='icon-category' onMouseEnter={() => setIsExpanded(true)} onMouseLeave={() => setIsExpanded(false)}>  My Count</button>
            {isExpanded && (
              <div className="filter-bar expanded" onMouseEnter={() => setIsExpanded(true)} onMouseLeave={() => setIsExpanded(false)} style={{ backgroundColor: 'white', border: '1px solid #ccc', boxShadow: '0 2px 4px rgba(0,0,0,0.1)', padding: '10px', }}>
                <Nav className="ml-auto">
                  <div style={{ marginRight: '5px', width: '100px' }} onClick={() => handlePageChange('Register')}>
                    {currentPage !== 'Register' && (
                      <Link to="/signupUser" type="button" className="btn btn-outline-primary"> Sing Up</Link>
                    )}
                  </div>
                  <div style={{ width: '80px' }} onClick={() => handlePageChange('Login')} >
                    {currentPage !== 'Login' && (
                      <Link to="/signin" type="button" className="btn btn-outline-primary" >Sing In</Link>
                    )}
                  </div>
                </Nav>
              </div>
            )}
          </div>
        )}

        {currentPage == 'Login' && (
          <Link to="/signupUser" type="button" className="btn btn-outline-primary"> Sing Up</Link>
        )}
          {currentPage == 'Register' && (
                      <Link to="/signin" type="button" className="btn btn-outline-primary" >Sing In</Link>
        )}

        {currentPage === 'Home' && (

          <Link to="/cart" style={{ color: 'black', fontSize: '25px', textDecoration: 'none', backgroundColor: '#60a2e7', padding: '14px', borderBottomRightRadius: '15%', marginLeft: "30px" }}>
            My cart<FontAwesomeIcon icon={faCartShopping} style={{ fontSize: '30px', marginLeft: '5px' }} />
          </Link>

        )}
      </Container>
    </BootstrapNavbar>
  );
};

export default CustomNavbar;
