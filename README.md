# 📦 PruebaFAC - Sistema de Pedidos con Autenticación JWT

Este proyecto fue desarrollado como parte de una prueba técnica por el desarrollador **David Paramo** de la empresa **Softgic**.
Contacto: david.paramo@softgic.co

---

## 🚀 Tecnologías Usadas

- ASP.NET Core 8
- Entity Framework Core
- JWT (JSON Web Tokens)
- Swagger (documentación)
- SQL Server

---

## ⚙️ Configuración inicial

1. Clonar el repositorio:
```
git clone https://github.com/dparamo/PruebaFAC.git
```

2. Abrir la solución en Visual Studio 2022 o superior.

3. Crear la base de datos con las migraciones:
```
dotnet ef database update
```

4. Revisar el archivo `appsettings.json` y configurar tu cadena de conexión a SQL Server:
```json
"ConnectionStrings": {
  "ConnectionStringSQLServer": "Server=.;Database=PruebaFAC;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

5. Correr la aplicación (`Ctrl + F5` o `dotnet run`).

6. Accede a Swagger UI en:
```
https://localhost:{puerto}/swagger
```

---

## 🔐 Autenticación JWT

- En el login `/api/v1/auth/login`, puedes autenticarte con un usuario registrado y obtener un token JWT.
- Usa el botón **Authorize 🔒** en Swagger para ingresar el token y acceder a los endpoints protegidos.

---

## 📌 Endpoints disponibles

### 🔐 Auth
| Método | Endpoint | Descripción |
|--------|----------|-------------|
| POST   | `/api/v1/auth/login` | Autenticación de usuario y generación de token JWT. |

### 👤 Customer
| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET    | `/api/v1/Customer`        | Obtener todos los clientes. |
| GET    | `/api/v1/Customer/{id}`   | Obtener cliente por ID. |
| POST   | `/api/v1/Customer`        | Crear nuevo cliente. |
| PUT    | `/api/v1/Customer/{id}`   | Actualizar cliente. |
| DELETE | `/api/v1/Customer/{id}`   | Eliminar cliente. |

### 📦 Order
| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET    | `/api/v1/orders`                            | Obtener todas las órdenes. |
| GET    | `/api/v1/orders/{id}`                       | Obtener orden por ID. |
| POST   | `/api/v1/orders`                            | Crear nueva orden. |
| PUT    | `/api/v1/orders/{orderId}/items`            | Actualizar ítems de una orden existente. |
| DELETE | `/api/v1/orders/{id}`                       | Eliminar orden por ID. |
| GET    | `/api/v1/orders/customer/{customerId}`      | Obtener órdenes por cliente. |
| GET    | `/api/v1/orders/search`                     | Buscar órdenes filtrando por estado, fecha o cliente. |

### 🛒 Product
| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET    | `/api/v1/products`            | Obtener todos los productos. |
| GET    | `/api/v1/products/{id}`       | Obtener producto por ID. |
| POST   | `/api/v1/products`            | Crear nuevo producto. |
| PUT    | `/api/v1/products/{id}`       | Actualizar producto. |
| DELETE | `/api/v1/products/{id}`       | Eliminar producto por ID. |

## 📝 Notas

- Los productos se gestionan por separado y pueden ser vinculados a órdenes como ítems.
- El sistema maneja relaciones entre entidades como Customer → Order → OrderItems → Product.
- Se valida formato de email, presencia de campos obligatorios, y manejo de errores.

