import React, { useContext, useEffect, useState } from "react";
import "./FilterBar.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faBars } from "@fortawesome/free-solid-svg-icons";
import { faSortDown } from "@fortawesome/free-solid-svg-icons";
import Api from "../../Api";
import { CategoryContext } from "../Context/CategoryContext";



export default function FilterBar() {
  const {CategoryId, SetCategoryId} = useContext(CategoryContext);
  const [isExpanded, setIsExpanded] = useState(false);
  const [Category, SetCategory] = useState([]);

  useEffect(() => {
    const fetchCategories = async () => {
      try {
        const api = Api();
        const response = await api.get("/api/categories");
        SetCategory(response.data);
      } catch (error) {
        console.error('Error fetching categories:', error);
      }
    };
    fetchCategories();
  }, []);

  const handleFilter = (filter) => {
    SetCategoryId(filter);
  };

  const handleMouseEnter = () => {
    setIsExpanded(true);
  };

  const handleMouseLeave = () => {
    setIsExpanded(false);
  };
  return (
    <div className="filter-bar-container">
      <div         
        onMouseEnter={handleMouseEnter}
        onMouseLeave={handleMouseLeave}> 
      <button
        className="icon-category"
        onClick={() => handleFilter("all")}
      >
        <FontAwesomeIcon icon={faBars} /> All
        <FontAwesomeIcon icon={faSortDown} />
      </button>
      {isExpanded && (
        <div className="filter-bar">
          {Category.map((category, index) => (
            <div
              key={category.id}
              className={CategoryId === category.id ? "filter-button active" : "filter-button"}
              onClick={() => handleFilter(category.id)}
            >
              {category.categoryName}
            </div>
          ))}
        </div>
      )}
      </div>
    </div>
  );
}
