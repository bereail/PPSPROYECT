import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import{faMagnifyingGlass} from "@fortawesome/free-solid-svg-icons"
const Search = () => {


  return (
<div className="container-fluid">
  <form className="d-flex" role="search" style={{ display: "flex", alignItems: "center", margin: "-5px" }}>
    <div className="input-group" style={{ position: "relative", width: "390px" }}>
      <input className="form-control me-2" style={{ border: "none", padding: "12px 30px", borderRadius: "25px" }} type="search" placeholder="Search" aria-label="Search"></input>
      <button  style={{ position: "absolute", top: "50%", transform: "translateY(-50%)", right: "5px", border:"none", backgroundColor: 'transparent', padding: "15px" }} type="submit">
        <FontAwesomeIcon icon={faMagnifyingGlass} />
      </button>
    </div>
  </form>
</div>
  );
}

export default Search;
