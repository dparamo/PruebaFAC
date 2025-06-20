# PruebaFAC - API .NET 8 con JWT

Esta es una API desarrollada en ASP.NET Core 8 que permite gestionar clientes (Customers) y órdenes (Orders), utilizando Entity Framework Core con SQL Server y autenticación JWT.

## Tecnologías utilizadas

- .NET 8
- Entity Framework Core
- SQL Server
- Autenticación JWT (Json Web Token)
- Swagger UI para documentación
- C# 11

## Estructura de carpetas

- `Controllers`: Controladores de la API.
- `Services`: Lógica de negocio y servicios de entidades.
- `Entities`: Entidades del dominio (Customer, Order, etc.).
- `Dto`: Objetos de transferencia de datos.
- `Interfaces`: Interfaces de los servicios.
- `Context`: Contexto de la base de datos.
- `Utils`: Clases auxiliares (como generación de JWT).

## Pasos para correr el proyecto

1. **Clona el repositorio:**
   ```bash
   git clone https://github.com/dparamo/PruebaFAC.git
   cd PruebaFAC
   ```

2. **Configura la base de datos:**
   - Crea una base de datos SQL Server vacía.
   - En `appsettings.json`, configura tu cadena de conexión:
     ```json
     "ConnectionStrings": {
         "ConnectionStringSQLServer": "Server=localhost;Database=PruebaFACDb;Trusted_Connection=True;TrustServerCertificate=True;"
     }
     ```

3. **Configura el JWT (opcional):**
   En `appsettings.json`, puedes ajustar los valores JWT:
   ```json
   "JwtSettings": {
     "SecretKey": "TuClaveSuperSecreta123!",
     "Issuer": "PruebaFACApi",
     "Audience": "PruebaFACClient"
   }
   ```

4. **Ejecuta las migraciones (si usas EF Migrations):**
   ```bash
   dotnet ef database update
   ```

5. **Ejecuta el proyecto:**
   ```bash
   dotnet run
   ```

6. **Explora el Swagger:**
   - Abre `https://localhost:7216/swagger/index.html` en tu navegador.
   - Usa el botón "Authorize" para iniciar sesión con un token JWT.

## Pruebas con JWT

1. Ve a `/api/v1/auth/login` con el siguiente payload:
   ```json
   {
     "username": "admin",
     "password": "1234"
   }
   ```

2. Copia el token recibido y haz clic en "Authorize" en Swagger.

---

### Información del desarrollador

> **Esta es una prueba técnica realizada por el desarrollador David Guillenty Páramo Porras, colaborador de la empresa Softgic.**  
> Para cualquier contacto o consulta técnica relacionada con este proyecto, puede comunicarse al correo: **david.paramo@softgic.co**.
