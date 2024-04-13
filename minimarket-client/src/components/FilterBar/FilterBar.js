import React, { useState } from "react";
import "./FilterBar.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faBars } from "@fortawesome/free-solid-svg-icons";
import { faSortDown } from "@fortawesome/free-solid-svg-icons";
export default function FilterBar() {
  const [activeButton, setActiveButton] = useState(null);
  const [isExpanded, setIsExpanded] = useState(false);

  const handleFilter = (filter) => {
    setActiveButton(filter);
    setIsExpanded(false);
  };

  const handleMouseEnter = () => {
    setIsExpanded(true);
  };

  const handleMouseLeave = () => {
    setIsExpanded(false);
  };

  return (
    <div className="filter-bar-container">
      <button
        className="icon-category"
        onMouseEnter={handleMouseEnter}
        onMouseLeave={handleMouseLeave}
      >
        <FontAwesomeIcon icon={faBars} /> All<FontAwesomeIcon icon={faSortDown} />
      </button>
      {isExpanded && (
        <div
          className="filter-bar"
          onMouseEnter={handleMouseEnter}
          onMouseLeave={handleMouseLeave}
        >
          <button
            onClick={() => handleFilter("Bebidas")}
            className={
              activeButton === "Bebidas"
                ? "filter-button active"
                : "filter-button"
            }
          >
            Drinks
          </button>
          <button
            onClick={() => handleFilter("Limpieza")}
            className={
              activeButton === "Limpieza"
                ? "filter-button active"
                : "filter-button"
            }
          >
            Cleaning
          </button>
          <button
            onClick={() => handleFilter("Higiene")}
            className={
              activeButton === "Higiene"
                ? "filter-button active"
                : "filter-button"
            }
          >
            hygiene
          </button>
          <button
            onClick={() => handleFilter("Tecnología")}
            className={
              activeButton === "Tecnología"
                ? "filter-button active"
                : "filter-button"
            }
          >
            Technology
          </button>
        </div>
      )}
    </div>
  );
}
