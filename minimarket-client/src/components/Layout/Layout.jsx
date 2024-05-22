import React from "react";
import CustomNavbar from "../Navbar/CustomNavbar";
import Footer from "../Footer/footer";


const Layout = ({ children }) => {
  return (
    <div className="Layout">
      <CustomNavbar />
      <main>{children}</main>
      <Footer />
    </div>
  );
};

export default Layout;