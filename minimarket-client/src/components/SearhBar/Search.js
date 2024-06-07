import React, { useContext, useState } from 'react'
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import{faMagnifyingGlass} from "@fortawesome/free-solid-svg-icons"
import './Search.css'
import { SearchContext } from '../Context/SearchContext';
const Search1 = () => {
  const [Search, SetSearch] = useState()
  const {SetSearchValue} = useContext(SearchContext)
  return (
    <div>
          <form className="Search-Container" onSubmit={(event)=>{event.preventDefault(); SetSearchValue(Search)}}>
            <input onChange={(e)=>{SetSearch(e.target.value)}} className="Search-Input-Container" placeholder="Search"></input>
            <button className="Search-icon-Container" type="submit">
                <FontAwesomeIcon icon={faMagnifyingGlass} />
            </button>
          </form>
    </div>
  )
}

export default Search1
