import React from "react";
import Accordion from "./acordion";

const AccordionCard = () => {
  return (
    <div style={{ display: "flex", gap: "10px" }}>
      <Accordion />
      <Accordion />
      <Accordion />
      <Accordion />
      <Accordion />
      <Accordion />
    </div>
  );
}

export default AccordionCard;
