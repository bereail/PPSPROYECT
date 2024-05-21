import React, { useState } from "react";
import "./FilterBar.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faBars } from "@fortawesome/free-solid-svg-icons";
import { faSortDown } from "@fortawesome/free-solid-svg-icons";
import { useNavigate } from "react-router-dom";

export default function FilterBar() {
  const [activeButton, setActiveButton] = useState(null);
  const [isExpanded, setIsExpanded] = useState(false);
  const navigate = useNavigate();

  const handleFilter = (filter) => {
    setActiveButton(filter);
    setIsExpanded(false);
    navigate(`/products/${filter}`);
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
        onClick={() => handleFilter("all")}
      >
        <FontAwesomeIcon icon={faBars} /> All
        <FontAwesomeIcon icon={faSortDown} />
      </button>
      {isExpanded && (
        <div
          className="filter-bar"
          onMouseEnter={handleMouseEnter}
          onMouseLeave={handleMouseLeave}
        >
          <button
            onClick={() => handleFilter("bebidas")}
            className={
              activeButton === "bebidas"
                ? "filter-button active"
                : "filter-button"
            }
          >
            Drinks
          </button>
          <button
            onClick={() => handleFilter("limpieza")}
            className={
              activeButton === "Limpieza"
                ? "filter-button active"
                : "filter-button"
            }
          >
            Cleaning
          </button>
          <button
            onClick={() => handleFilter("higiene")}
            className={
              activeButton === "Higiene"
                ? "filter-button active"
                : "filter-button"
            }
          >
            hygiene
          </button>
          <button
            onClick={() => handleFilter("tecnologia")}
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
