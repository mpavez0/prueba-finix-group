# API Gestor de Facturas

## Descripci�n
API para gesti�n de facturas utilizando una base de datos local. Las principales funcionalidades son:

- B�squeda de facturas por n�mero, estado de la factura y estado de pago de la factura.
- Gesti�n de notas de cr�dito, pudiendo a�adir notas de cr�dito a facturas existentes.

## Instalaci�n y ejecuci�n del proyecto

- **Clonar el repositorio:**
  ```bash
  git clone https://github.com/mpavez0/prueba-finix-group

  ````bash 
  docker build -t gestor-facturas-api .
  docker run --rm -p 5084:5084 gestor-facturas-api

As�, para acceder al Swagger se debe consultar el enlace

http://localhost:5084/swagger/index.html

## Seguridad

La API cuenta con un middleware de autenticaci�n. Para ello, se deber� enviar un header tal que:

- **Llave**: Authorization

- **Valor**: Basic YWRtaW46cGFzc3dvcmQ=

Desde Swagger, la soluci�n se puede probar haciendo click en el bot�n Authorize (arriba a la derecha), y luego a�adiendo los siguientes valores:

- **username**: admin
- **password**: password

## Caracter�sticas del proyecto

- El proyecto se estructura bajo el enfoque de arquitectura de Domain-Driven Design (DDD).
- En la biblioteca de clases Infrastructure se aplica el patr�n de dise�o Repository, lo que a�ade una capa de abstracci�n sobre el acceso a la base de datos.

## Pendientes

- Implementar un CRUD completo que permita gestionar de mejor manera las facturas
- Implementar paginaci�n en todos los endpoints que retornen listas de facturas
- Mejorar la estructura de Program.cs, haciendo uso del ServiceCollectionExtension.cs
- Mejorar la estructura de DataSeeder.cs, desacoplando las validaciones/c�lculos que se hacen sobre las facturas (estado y estado de pago)