import React, { useEffect, useState } from "react";
import "./Home.css"; // Importa el archivo CSS para estilos específicos

const TemporaryMessage = ({ message }) => {
  const [showMessage, setShowMessage] = useState(false);

  useEffect(() => {
    if (message) {
      setShowMessage(true);
      setTimeout(() => {
        setShowMessage(false);
      }, 5000); // Tiempo en milisegundos para mostrar el mensaje
    }
  }, [message]);

  return (
    <div className={`TemporaryMessage ${showMessage ? "show" : ""}`} aria-live="polite">
      <p>{message}</p>
    </div>
  );
}

export default TemporaryMessage;
