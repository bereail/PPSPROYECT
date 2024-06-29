Este repositorio contiene el codigo realizado para la creacion de una aplicacion web, desarrollada utilizando la libreria ReactJS, y el framework backend .NET

Esta aplicacion web consiste en un E-Commerce planteado para un minimercado ficticio, "Family Market".
Define 3 roles de usuario (Cliente, Vendedor, SuperAdmin) con diferentes permisos para cada uno.

- Cliente puede actualizar su info de perfil y contraseña, generar o eliminar su direccion de entrega, generar, cancelar o pagar ordenes, y desactivar su propia cuenta.

- Vendedor puede hacer todo lo que Cliente, exceptuando la funcionalidad de ordenes. Ademas de ello, puede generar categorias nuevas, y añadir productos nuevos a dichas categorias. Puede tambien actualizar, desactivar y restaurar tanto categorias como productos, añadir o eliminar imagenes de producto.

- SuperAdmin puede hacer todo lo que Vendedor. Ademas de ello, se encarga de gestionar las credenciales (o legajos) utilizados por los vendedores para generar sus cuentas. Tambien tiene acceso a la funcion de eliminar cualquier registro desactivado (exceptuando usuarios), ver, filtar y desactivar usuarios (excluyendo otros SuperAdmins, o el mismo) y dar de alta nuevos SuperAdmins.

El proyecto fue realizado para las materias de Laboratorio de Computacion IV, y Practica Profesional Supervisada, de la Tecnicatura Universitaria en Programacion, UTN FRRO.

Tecnologias utilizadas:

1. FRONTEND:

El Frontend fue desarrollado con la libreria ReactJS, version 18.2.0
El listado de librerias adicionales utilizadas es el siguiente:

- fortawesome/fontawesome-svg-core: 6.5.2
- fortawesome/free-brands-svg-icons: 6.5.2
- fortawesome/free-regular-svg-icons: 6.5.2
- fortawesome/free-solid-svg-icons: 6.5.2
- fortawesome/react-fontawesome: 0.2.2
- mercadopago/sdk-react: 0.0.19
- testing-library/user-event: 13.5.0
- axios: 1.7.2
- bootstrap: 5.3.3
- fontawesome: 5.6.3
- jwt-decode: 4.0.0
- mercadopago: 2.0.9
- query-string: 9.0.0
- react-bootstrap: 2.10.2
- react-dom: 18.2.0
- react-icons: 5.0.1
- react-router-dom: 6.23.1
- react-scripts: 5.0.1
- react-simple-chatbot: 0.6.1
- reactstrap: 9.2.2
- styled-components: 4.4.1
- web-vitals: 2.1.4
- zxcvbn: 4.4.2

2. BACKEND:

El Backend fue desarrollado con el Framework de .NET 8, para la implementacion de una API MVC.
El listado de paquetes NuGet utilizados es el siguiente:

- AutoMapper 13.0.1
- mercadopago-sdk 2.3.8
- Microsoft.AspNetCore.Authentication.JwtBearer 8.0.4
- Microsoft.AspNetCore.Identity.EntityFrameworkCore 8.0.4
- Microsoft.EntityFrameworkCore.SqlServer 8.0.4
- Microsoft.EntityFrameworkCore.Tools 8.0.5
- Serilog 3.1.1
- Serilog.AspNetCore 8.0.1
- Serilog.Sinks.Console 5.0.1
- Serilog.Sinks.File 5.0.0
- Swashbuckle.AspNetCore 6.5.0
- System.IdentityModel.Tokens.Jwt 7.5.1

3. BACKEND TESTING:

El testing unitario del Backend fue desarrollado utilizando la herramienta de testeo unitario xUnit.net

El listado de paquetes NuGet utilizados en el testing es el siguiente:

- coverlet.collector 6.0.0
- Microsoft.NET.Test.Sdk 17.8.0
- Moq 4.20.70
- xunit 2.5.3
- xunit.runner.visualstudio 2.5.3

4. BASE DE DATOS:

La base de datos fue implementada a traves de el Code-First Approach. Se utilizo SQL Server 2022 v.16.0.1000.6, Developer Edition (64-bit).

. Incluido en este repositorio, se encuentra el diagrama de clases del sistema. No contiene todos los metodos, y funciona mas que nada como un repaso general de la aplicacion.
