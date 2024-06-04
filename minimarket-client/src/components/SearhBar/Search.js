import React from 'react'
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import{faMagnifyingGlass} from "@fortawesome/free-solid-svg-icons"
import './Search.css'
const Search1 = () => {
  return (
    <div>
          <div className="Search-Container">
            <input className="Search-Input-Container" placeholder="Search"></input>
            <button className="Search-icon-Container" type="submit">
                <FontAwesomeIcon icon={faMagnifyingGlass} />
            </button>
          </div>

    </div>
  )
}

export default Search1
