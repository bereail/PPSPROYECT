import React from "react";
import Carousel from "react-bootstrap/Carousel";
import coca from "./../../Image/coca.jpg";
import ofertas from "./../../Image/ofertas.jpg";
import ofertas1 from "./../../Image/ofertas2.jpg";

function CarrouselPage() {
  return (
    <div>
      <Carousel>
        <Carousel.Item>
          <img
            style={{ height: "60vh" }}
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
            style={{ height: "60vh" }}
            className="d-block w-100"
            src={ofertas}
            alt="Second slide"
          />
        </Carousel.Item>
        <Carousel.Item>
          <img
            style={{ height: "60vh" }}
            className="d-block w-100"
            src={ofertas1}
            alt="Second slide"
          />
        </Carousel.Item>
      </Carousel>
    </div>
  );
}

export default CarrouselPage;
