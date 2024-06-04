import React, { useContext, useEffect, useState } from 'react'
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faUser } from "@fortawesome/free-solid-svg-icons"
import "./login.css"
import { Link, useLocation } from 'react-router-dom';
import { AuthContext } from '../Context/AuthContext';
const Login = () => {
    const [isExpanded, setIsExpanded] = useState(false);
    const { pathname } = useLocation();
    const [currentPage, SetCurrentPage] = useState("Home");
    const { user, logout } = useContext(AuthContext);


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
            case "/User":
                SetCurrentPage("User");
                break;
            default:
                SetCurrentPage("");
        }
    }, [pathname]);

    const handleLogout = () => {
        logout();
    };
    return (
        <div>
            {currentPage !== "Login" && currentPage !== "Register" && currentPage !== "User" && <>

                <button className='MyCount-Container' onMouseEnter={() => setIsExpanded(true)} >
                    <p style={{ marginBottom: ' 0px' }}> My Account</p>
                    <FontAwesomeIcon icon={faUser} className='iconcount-container' />
                </button>
                {isExpanded && !user && (
                    <div
                        onMouseLeave={() => setIsExpanded(false)}
                        className={`Login-Content ${isExpanded ? 'expanded' : ''}`} >
                        <div className="Login-Links">
                            <Link
                                to="/signupUser"
                                type="button"
                                className="Navbar-SingUp">
                                Sign Up
                            </Link>
                            <Link
                                to="/signin"
                                type="button"
                                className="Navbar-SingIn">
                                Sign In
                            </Link>
                        </div>
                    </div>
                )}


                {isExpanded && user && (<>
                    <div
                        onMouseLeave={() => setIsExpanded(false)}
                        className={`Login-Content ${isExpanded ? 'expanded' : ''}`} >
                        <div className="Login-Links">
                            <Link
                                to="/User"
                                type="button"
                                className="Navbar-SingUp">
                                Account
                            </Link>
                            <button
                                style={{ width: "80px", borderStyle: 'none', color: '#493f3b' }}
                                onClick={handleLogout}
                                className="Navbar-SingIn">
                                Exit
                            </button>
                        </div>
                    </div>
                </>)}
            </>}
            {currentPage == "Login" && (            
                    <Link
                        to="/signupUser"
                        type="button"
                        className="Navbar-SingUp">
                        Sign Up
                    </Link>
            )}
            {currentPage == "Register" && (
                <Link
                    to="/signin"
                    type="button"
                    className="Navbar-SingIn">
                    Sign In
                </Link>
            )}
        </div>
    )
}

export default Login
