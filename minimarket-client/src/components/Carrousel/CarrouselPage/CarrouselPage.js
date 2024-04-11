import React from "react";
import Carousel from "react-bootstrap/Carousel";
import coca from "./../../Image/coca.jpg";
import ofertas from "./../../Image/ofertas.jpg";
import ofertas1 from "./../../Image/ofertas2.jpg";
import mp from "./../../Image/mp.jpg";
import bancario from "./../../Image/bancario.jpg";
import ofertas4 from "./../../Image/ofertas4.jpg";

function CarrouselPage() {
  return (
    <div>
      <Carousel>
        <Carousel.Item>
          <img
            
            className="d-block w-100"
            src={coca}
            alt="First slide"
          />
          <Carousel.Caption>
            <h3>First slide label</h3>
            <p>Texto</p>
          </Carousel.Caption>
        </Carousel.Item>
        <Carousel.Item>
          <img
            
            className="d-block w-100"
            src={ofertas}
            alt="Second slide"
          />
        </Carousel.Item>
        <Carousel.Item>
          <img
            
            className="d-block w-100"
            src={ofertas1}
            alt="Second slide"
          />
        </Carousel.Item>
        <Carousel.Item>
          <img
            
            className="d-block w-100"
            src={mp}
            alt="Second slide"
          />
        </Carousel.Item>
        <Carousel.Item>
          <img
            
            className="d-block w-100"
            src={bancario}
            alt="Second slide"
          />
        </Carousel.Item>
        <Carousel.Item>
          <img
            
            className="d-block w-100"
            src={ofertas4}
            alt="Second slide"
          />
        </Carousel.Item>
      </Carousel>
    </div>
  );
}

export default CarrouselPage;
