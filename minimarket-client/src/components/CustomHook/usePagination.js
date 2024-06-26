import { useState } from 'react';

const usePagination = (initialPage = 1) => {
  const [pageNumber, setPageNumber] = useState(initialPage);

  const nextPage = () => {
    setPageNumber(prevPageNumber => prevPageNumber + 1);
  };

  const prevPage = () => {
    if (pageNumber > 1) {
      setPageNumber(prevPageNumber => prevPageNumber - 1);
    }
  };

  const PaginationButtons = () => (
    <div>
      {pageNumber !== 1 && <button className='Page-button' onClick={prevPage}>Previous page</button>}
      <button className='Page-button' onClick={nextPage}>Next page</button>
    </div>
  );

  return { pageNumber, PaginationButtons };
};

export default usePagination;
