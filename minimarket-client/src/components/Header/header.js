// Header.js
import React from 'react';
import HeaderCard from './headerCard';

const Header = () => {
  return (
    <header className="header">
      <div className="container">
        <h1>Header</h1>
        <HeaderCard />
      </div>
    </header>
  );
}

export default Header;
