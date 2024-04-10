/*import React, { useState } from "react";
import './acordion.css';


const Accordion = () => {
  const [isHovered1, setIsHovered1] = useState(false);

  return (
    <div className="accordion" id="accordionExample">
      <div className="accordion-item">
        <h2 className="accordion-header">
          <button
            className={`accordion-button ${isHovered1 ? "collapsed" : ""}`}
            type="button"
            onMouseEnter={() => setIsHovered1(true)}
            onMouseLeave={() => setIsHovered1(false)}
            data-bs-toggle="collapse"
            data-bs-target="#collapseOne"
            aria-expanded="false"
            aria-controls="collapseOne"
          >
           Product
          </button>
        </h2>
        <div
          id="collapseOne"
          className={`accordion-collapse collapse ${isHovered1 ? "show" : ""}`}
          aria-labelledby="headingOne"
          data-bs-parent="#accordionExample"
        >
          <div className="accordion-body">
            Detalle del Producto
          </div>
        </div>
      </div>
    </div>
  );
};

export default Accordion;*/
