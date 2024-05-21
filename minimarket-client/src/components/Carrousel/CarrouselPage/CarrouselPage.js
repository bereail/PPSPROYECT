import React from "react";
import Carousel from "react-bootstrap/Carousel";
import Pepsi from "./../../Image/Pepsi.jpg";
import ofertas from "./../../Image/ofertas.jpg";
import ofertas1 from "./../../Image/oferta1.jpg";
import limpieza from "./../../Image/limpieza.jpg";
import mp from "./../../Image/mp.jpg";
import milka from "./../../Image/milka.jpg";
import './CarrouselPage.css'
function CarrouselPage() {
  return (
    <div>
 <Carousel>
        <Carousel.Item >
          <img 
            className="Image-Carrousel"
            src={Pepsi}
            alt="First slide"
          />
          <Carousel.Caption>
          </Carousel.Caption>
        </Carousel.Item>
        <Carousel.Item>
          <img
            className="Image-Carrousel"
            src={limpieza}
            alt="Second slide"
          />
        </Carousel.Item>
        <Carousel.Item>
          <img
            className="Image-Carrousel"
            src={milka}
            alt="Second slide"
          />
        </Carousel.Item>
        <Carousel.Item>
          <img
            className="Image-Carrousel"
            src={ofertas1}
            alt="third slide"
          />
        </Carousel.Item>
        <Carousel.Item>
          <img
            className="Image-Carrousel"
            src={ofertas}
            alt="Second slide"
          />
        </Carousel.Item>
        <Carousel.Item>
          <img
            className="Image-Carrousel"
            src={mp}
            alt="quarter slide"
          />
        </Carousel.Item>
      </Carousel>
    </div>
  );
}

export default CarrouselPage;
