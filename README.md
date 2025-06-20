
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

## Endpoints Principales

### Autenticación
- `POST /api/v1/auth/login` → Generar token JWT

### Customers
- `GET /api/v1/customers`
- `POST /api/v1/customers`
- `PUT /api/v1/customers/{id}`

### Orders
- `GET /api/v1/orders`
- `POST /api/v1/orders`
- `PUT /api/v1/orders/items/{orderId}`
- `GET /api/v1/orders/search?status=&date=&customerId=`

### Products
- `GET /api/v1/products`
- `POST /api/v1/products`

## Autenticación JWT

1. Ejecuta el login con un `POST /api/v1/auth/login`.
2. Copia el token generado.
3. Haz clic en "Authorize" en Swagger y pega el token en el formato:

```
Bearer {tu_token}
```

## Notas Finales

- El modelo de datos ahora incluye una entidad `Product`, y `OrderItem` se relaciona con `Product`.
- La búsqueda de órdenes permite filtrar por estado, cliente y fecha de creación.
