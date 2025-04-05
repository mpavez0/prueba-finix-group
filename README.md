# API Gestor de Facturas

## Descripción
API para gestión de facturas utilizando una base de datos local. Las principales funcionalidades son:

- Búsqueda de facturas por número, estado de la factura y estado de pago de la factura.
- Gestión de notas de crédito, pudiendo añadir notas de crédito a facturas existentes.

## Instalación y ejecución del proyecto

- **Clonar el repositorio:**
  ```bash
  git clone https://github.com/mpavez0/prueba-finix-group

- **Ejecutar solución**
  ````bash 
  docker build -t gestor-facturas-api .
  docker run --rm -p 5084:5084 gestor-facturas-api

Así, para acceder al Swagger se debe consultar el enlace

http://localhost:5084/swagger/index.html

## Seguridad

La API cuenta con un middleware de autenticación. Para ello, se deberá enviar un header tal que:

- **Llave**: Authorization

- **Valor**: Basic YWRtaW46cGFzc3dvcmQ=

Desde Swagger, la solución se puede probar haciendo click en el botón Authorize (arriba a la derecha), y luego añadiendo los siguientes valores:

- **username**: admin
- **password**: password

## Características del proyecto

- El proyecto se estructura bajo el enfoque de arquitectura de Domain-Driven Design (DDD).
- En la biblioteca de clases Infrastructure se aplica el patrón de diseño Repository, lo que añade una capa de abstracción sobre el acceso a la base de datos.

## Pendientes

- Implementar un CRUD completo que permita gestionar de mejor manera las facturas
- Implementar paginación en todos los endpoints que retornen listas de facturas
- Mejorar la estructura de Program.cs, haciendo uso del ServiceCollectionExtension.cs
- Mejorar la estructura de DataSeeder.cs, desacoplando las validaciones/cálculos que se hacen sobre las facturas (estado y estado de pago)
