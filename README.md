
# PruebaFAC

Esta es una prueba técnica realizada por el desarrollador **David Páramo** de la empresa **Softgic**.  
Correo: **david.paramo@softgic.co**

## Descripción del Proyecto

PruebaFAC es una API REST construida con **ASP.NET Core 8**, que gestiona clientes (Customers), órdenes (Orders), productos (Products) y los ítems asociados a una orden (OrderItems). Se incluye autenticación basada en **JWT (JSON Web Tokens)**.

## Tecnologías Usadas

- ASP.NET Core 8
- Entity Framework Core
- SQL Server
- JWT Bearer Authentication
- Swagger (OpenAPI)

## Requisitos Previos

- [.NET SDK 8.0](https://dotnet.microsoft.com/download)
- SQL Server
- Visual Studio o VS Code
- EF Core Tools (`dotnet tool install --global dotnet-ef`)

## Configuración Inicial

1. Clonar el repositorio:

```bash
git clone https://github.com/dparamo/PruebaFAC.git
cd PruebaFAC
```

2. Configurar la cadena de conexión en `appsettings.json`:

```json
"ConnectionStrings": {
  "ConnectionStringSQLServer": "Server=localhost;Database=PruebaFAC;Trusted_Connection=True;TrustServerCertificate=True;"
},
"JwtSettings": {
  "SecretKey": "TU_CLAVE_SECRETA",
  "Issuer": "PruebaFACAPI",
  "Audience": "PruebaFACUsers",
  "ExpirationMinutes": 60
}
```

3. Crear la base de datos y aplicar migraciones:

```bash
dotnet ef database update
```

## Ejecutar el Proyecto

```bash
dotnet run
```

La API estará disponible en: https://localhost:7216

## Endpoints Disponibles

🔐 Auth
Método	Endpoint	Descripción
POST	/api/v1/auth/login	Autenticación de usuario y generación de token JWT.

👤 Customer
Método	Endpoint	Descripción
GET	/api/v1/Customer	Obtener todos los clientes.
GET	/api/v1/Customer/{id}	Obtener cliente por ID.
POST	/api/v1/Customer	Crear nuevo cliente.
PUT	/api/v1/Customer/{id}	Actualizar cliente.
DELETE	/api/v1/Customer/{id}	Eliminar cliente.

📦 Order
Método	Endpoint	Descripción
GET	/api/v1/orders	Obtener todas las órdenes.
GET	/api/v1/orders/{id}	Obtener orden por ID.
POST	/api/v1/orders	Crear nueva orden.
PUT	/api/v1/orders/{orderId}/items	Actualizar ítems de una orden existente.
DELETE	/api/v1/orders/{id}	Eliminar orden por ID.
GET	/api/v1/orders/customer/{customerId}	Obtener órdenes por cliente.
GET	/api/v1/orders/search	Buscar órdenes filtrando por estado, fecha o cliente.

🛒 Product
Método	Endpoint	Descripción
GET	/api/v1/products	Obtener todos los productos.
GET	/api/v1/products/{id}	Obtener producto por ID.
POST	/api/v1/products	Crear nuevo producto.
PUT	/api/v1/products/{id}	Actualizar producto.
DELETE	/api/v1/products/{id}	Eliminar producto por ID.


## Autenticación JWT

1. Ejecuta el login con un `POST /api/v1/auth/login`.
2. Copia el token generado.
3. Haz clic en "Authorize" en Swagger y pega el token en el formato:

```
Bearer {tu_token}
```
