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
        <Carousel.Item >
          <img 
            style={{maxWidth: '100%', maxHeight:'450px'}}
            className="d-block w-100 img-fluid"
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
            style={{maxWidth: '100%', maxHeight:'450px'}}
            className="d-block w-100 img-fluid"
            src={ofertas}
            alt="Second slide"
          />
        </Carousel.Item>
        <Carousel.Item>
          <img
            style={{maxWidth: '100%', maxHeight:'450px'}}
            className="d-block w-100 img-fluid"
            src={ofertas1}
            alt="Second slide"
          />
        </Carousel.Item>
        <Carousel.Item>
          <img
            style={{maxWidth: '100%', maxHeight:'450px'}}
            className="d-block w-100 img-fluid"
            src={mp}
            alt="third slide"
          />
        </Carousel.Item>
        <Carousel.Item>
          <img
            style={{maxWidth: '100%', maxHeight:'450px'}} 
            className="d-block w-100 img-fluid"
            src={bancario}
            alt="Second slide"
          />
        </Carousel.Item>
        <Carousel.Item>
          <img
            style={{maxWidth: '100%', maxHeight:'450px'}}
            className="d-block w-100 img-fluid"
            src={ofertas4}
            alt="quarter slide"
          />
        </Carousel.Item>
      </Carousel>
    </div>
  );
}

export default CarrouselPage;
