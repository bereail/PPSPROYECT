import React, { useContext, useEffect, useState } from "react";
import "./FilterBar.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faBars } from "@fortawesome/free-solid-svg-icons";
import { faTrashCan, faTrashCanArrowUp, faSortDown } from "@fortawesome/free-solid-svg-icons";


import { CategoryContext } from "../Context/CategoryContext";
import api from "../../api";
import { AuthContext } from "../Context/AuthContext";
import { ThemeContext } from "../Context/ThemeContext";



export default function FilterBar() {
  const { CategoryId, SetCategoryId } = useContext(CategoryContext);
  const [isExpanded, setIsExpanded] = useState(false);
  const { theme } = useContext(ThemeContext);
  const [Category, SetCategory] = useState([]);
  const [isActive, SetisActive] = useState(false)
  const { role } = useContext(AuthContext)
  const fetchCategories = async (isactive) => {
    try {
      if (isActive !== '') {
        SetisActive(!isActive)
      }

      const response = await api.get("/api/categories", {
        params: { isActive: isactive }
      });
      SetCategory(response.data);
    } catch (error) {
    }
  };
  useEffect(() => {
    fetchCategories();
  }, []);
  const handleDisabelCategory = async () => {
    try {
      await api.delete(`/api/categories/${CategoryId}`)
      fetchCategories()
    } catch (error) {
    }
  }
  const handleActiveCategory = async () => {
    try {
      await api.patch(`/api/categories/${CategoryId}`);
      fetchCategories()
    } catch (error) {
    }
  };

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
        <div className={`icon-category ${theme === 'dark' ? 'dark-theme' : ''}`}>
          <FontAwesomeIcon icon={faBars} /> Categories
        </div>
        {isExpanded && (
          <div className='filter-bar'>
            {Category.map((category) => (<>

              <div
                key={category.id}
                className={`${CategoryId === category.id ? "filter-button active" : "filter-button"} ${!category.isActive ? "Category-disabled" : ""} ${theme === 'dark' ? 'dark-theme-category' : ''}`}
                onClick={() => handleFilter(category.id)}
              >
                {category.categoryName}
                {CategoryId == category.id && role === 'Seller' && category.isActive &&
                  <FontAwesomeIcon icon={faTrashCan} style={{ marginLeft: '10px' }} onClick={() => { handleDisabelCategory() }} />
                }
                {CategoryId == category.id && role === 'Seller' && !category.isActive &&
                  <FontAwesomeIcon icon={faTrashCanArrowUp} style={{ marginLeft: '10px' }} onClick={() => { handleActiveCategory() }} />
                }
              </div>
            </>
            ))}
            {role === 'Seller' && <button className="Buton-Disabel-Category" onClick={() => { fetchCategories(!isActive) }}>GetCategoryDisable</button>}
          </div>
        )}
      </div>
    </div>
  );
}
