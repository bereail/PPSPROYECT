import React, { useState } from 'react'
import './FilterBar.css'
export default function FilterBar() {

    const [activeButton, setActiveButton] = useState(null);
    const handleFilter = (filter) => {
        setActiveButton(filter);

        //DAR FUNCION AL FILTRADO
      };
    
      return (
        <div className="filter-bar">
          <button onClick={() => handleFilter("Bebidas")} className={activeButton === "Bebidas" ? "filter-button active" : "filter-button"}>Bebidas</button>
          <button onClick={() => handleFilter("Limpieza")} className={activeButton === "Limpieza" ? "filter-button active" : "filter-button"}>Limpieza</button>
          <button onClick={() => handleFilter("Higiene")} className={activeButton === "Higiene" ? "filter-button active" : "filter-button"}>Higiene</button>
          <button onClick={() => handleFilter("Tecnología")} className={activeButton === "Tecnología" ? "filter-button active" : "filter-button"}>Tecnología</button>
        </div>
      );
    }