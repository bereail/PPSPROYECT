import React, { useContext, useEffect, useState } from 'react'
import './Navbar.css'
import Logo from './Logo'
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCartShopping, faSun, faMoon, faHeart } from "@fortawesome/free-solid-svg-icons";

import { ThemeContext } from '../Context/ThemeContext';
import FilterBar from '../FilterBar/FilterBar';
import { Link, useLocation } from 'react-router-dom';
import Login from '../Login/login';
import Search1 from '../SearhBar/Search';
import { AuthContext } from '../Context/AuthContext';
const Navbar = () => {
  const { theme, toggleTheme } = useContext(ThemeContext);
  const { pathname } = useLocation();
  const [currentPage, SetCurrentPage] = useState("Home");
  const { role } = useContext(AuthContext)

  useEffect(() => {
    switch (pathname) {
      case "/":
        SetCurrentPage("Home");
        break;
      case "/signupUser":
        SetCurrentPage("Register");
        break;
      case "/signin":
        SetCurrentPage("Login");
        break;
      case "/cart":
        SetCurrentPage("cart");
        break;
      case "/user":
        SetCurrentPage("User");
        break;
      case "/favorite":
        SetCurrentPage("favorite");
        break;
      default:
        SetCurrentPage("");
    }
  }, [pathname]);
  return (
    <div className='Navbar-Container'>
      <div className="Navbar-Logo" >
        <Link to="/">
          <Logo />
        </Link>
      </div>
      <button
        className="toggle-theme-button"
        onClick={toggleTheme}
        style={{ color: theme === "light" ? "black" : "white", }}
      >
        <FontAwesomeIcon icon={theme === "light" ? faMoon : faSun} />
      </button>
      {currentPage === "Home" &&
        <div className='NavFilter-Contaier'>
          <FilterBar />
          <Search1 />
        </div>
      }
      <div className='NavRightItems'>
      {((currentPage === "Home" || currentPage === "User" || currentPage === 'cart') && (role === 'Customer')) &&

        <div className='NavFavorite-Container'>
          <Link to="/favorite" className='Nav-LinkFavorite'>
            <FontAwesomeIcon
              className='Nav-icon'
              icon={faHeart}
            />
            <p className='Nav-TextFavorite'>Favorites</p>
          </Link>
        </div>
        }
        {((currentPage === "Home" || currentPage === "User" || currentPage === 'favorite') && (role === 'Customer' || !role)) &&
          <div className='NavCart-Container'>
            <Link to="/cart">
              <FontAwesomeIcon
                className='Nav-icon'
                icon={faCartShopping}
              />
              <p>Cart</p>
            </Link>
          </div>
        }
        <div className='NavLogin-Container'>
          <Login />
        </div>
      </div>

    </div>
  )
}

export default Navbar
