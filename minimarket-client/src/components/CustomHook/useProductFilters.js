import { useState } from 'react';

const useProductFilters = () => {
  const [sortByOption, setSortByOption] = useState('');
  const [isAscending, setIsAscending] = useState();

  const handleSortByChange = (value) => {
    setSortByOption(value);
  };

  const handleOrderChange = (value) => {
    setIsAscending(value);
  };

  return {
    sortByOption,
    isAscending,
    handleSortByChange,
    handleOrderChange,
    SortOptions: () => {
      // Renderizar los selects de ordenamiento
      return (
        <div className="sort-options">
          <select id="sortOptions" name="sortOptions" value={sortByOption} onChange={(e) => handleSortByChange(e.target.value)}>
            <option value='' disabled>Sort by:</option>
            <option value="Name">Name</option>
            <option value="Price">Price</option>
            <option value="Discount">Discount</option>
          </select>
          {sortByOption &&
            <select id="sortOrder" name="sortOrder" value={isAscending} onChange={(e) => handleOrderChange(e.target.value)}>
              <option value='' disabled>Sort order</option>
              <option value={true}>Ascending</option>
              <option value={false}>Descending</option>
            </select>
          }
        </div>
      );
    }
  };
};

export default useProductFilters;
