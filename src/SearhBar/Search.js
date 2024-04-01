import React from "react";

const Search = () => {
  return (
    <nav style={{ width: "100%", padding: "10px", backgroundColor: "#f8f9fa", borderRadius: "10px", display: "flex", alignItems: "center", justifyContent: "space-between" }}>
      <div className="container-fluid">
        <form className="d-flex" role="search" style={{ display: "flex", alignItems: "center" }}>
          <input className="form-control me-2" style={{ width: "300px", marginRight: "10px" }} type="search" placeholder="Consigue lo que buscas" aria-label="Search" />
          <button className="btn btn-outline-primary" style={{ width: "100px" }} type="submit">Buscar</button>
        </form>
      </div>
    </nav>
  );
}

export default Search;
