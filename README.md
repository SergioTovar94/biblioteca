# Biblioteca API

## Descripción

API para gestión de biblioteca con autores, libros y préstamos.
Incluye arquitectura en capas, EF Core y frontend en React.

---

## Requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/) (v18 o superior)
- SQL Server (cualquier edición: LocalDB, Express, Developer, o Docker)

---

## Configuración y ejecución del backend

### 1. Clonar el repositorio y posicionarse en la raíz.

### 2. Restaurar paquetes:

```bash
dotnet restore
```

### 3. Configurar la cadena de conexión

Edita el archivo Api/appsettings.json (o appsettings.Development.json) y ajusta el valor de ConnectionStrings:Default según tu entorno.

- Para LocalDB (recomendado, sin instalación adicional):

```
"Server=(localdb)\\mssqllocaldb;Database=BibliotecaDB;Trusted_Connection=True;MultipleActiveResultSets=true"
```

- Para SQL Server en Docker (como usaste durante el desarrollo):

```
"Server=localhost,1433;Database=BibliotecaDB;User Id=dba;Password=Controldoc2024;TrustServerCertificate=True"
```

- Para SQL Express:

```
"Server=.\\SQLEXPRESS;Database=BibliotecaDB;Trusted_Connection=True;"
```

### 4. Aplicar migraciones

```bash
dotnet ef database update --startup-project ../Api
```

**Nota:** Si el comando anterior falla, ejecuta `dotnet tool install --global dotnet-ef` primero.

Estos datos pueden ser recuperados mediante un respaldo de base de datos o, en su defecto, utilizando los valores de inserción incluidos en el proyecto.

### 5. Ejecutar API

```bash
cd Api
dotnet run
```

La API estará disponible en http://localhost:5088 (o el puerto que indique la consola).
Swagger:
http://localhost:5088/swagger/index.html

---

## Frontend

Desde la raiz del proyecto

```bash
cd front
npm install
npm run dev
```

Abrir http://localhost:5173 en el navegador.

Estado del frontend: Implementa listado de autores y listado de libros (con visualización de portadas). No han sido implementadas las funciones de Create, Update y Delete en front.
Las funcionalidades completas CRUD para autores, libros y préstamos están implementadas en backend y se pueden consumir directamente desde Swagger.

## Endpoints principales

- Autores – CRUD completo con paginación y ordenamiento.
- Libros – CRUD con carga de imagen (portada).
- Préstamos – Crear préstamo y listar.

# Bugs resueltos

| Bug                                                              | Síntoma                                                               | Causa raíz                                                                                                                         | Solución                                                                                                       |
| ---------------------------------------------------------------- | --------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------------- |
| BUG 1: Listado de autores no funciona                            | Error indicando que la tabla `authorses` no existe                    | Mapeo incorrecto de entidad a base de datos (nombre singular), uso de método incorrecto para listar y consulta a tabla inexistente | Corrección del mapeo entidad-tabla, ajuste del método para listar todos los autores y generación de migración  |
| BUG 2: No se puede registrar el autor                            | No permite crear autores desde el endpoint POST                       | Problemas en el flujo de persistencia y validaciones en la capa de aplicación                                                      | Corrección del flujo de creación, validación de DTOs, ajuste de mapeo y persistencia correcta en base de datos |
| BUG 3: Swagger UI no aparece                                     | Error al acceder a `/swagger/index.html`                              | Falta configuración de Swagger en `Program.cs`                                                                                     | Se agregaron `AddSwaggerGen`, `UseSwagger` y `UseSwaggerUI`                                                    |
| BUG 4 (Adicional): Estructura inválida / arquitectura incorrecta | Lógica mezclada entre capas y uso directo de entidades en controllers | Entidades en capa incorrecta, ausencia de DTOs y violación de arquitectura limpia                                                  | Reorganización de capas, implementación de DTOs, creación de Use Cases y separación de responsabilidades       |

---

# Decisiones técnicas clave

- DTOs estrictos: Ninguna entidad de dominio se expone directamente en los controladores. Se usan CreateAuthorDto, UpdateAuthorDto, AuthorResponseDto, etc.

- Eliminación lógica: El campo IsDeleted en AuthorEntity permite marcar autores como eliminados sin perder la relación con libros existentes. El endpoint DELETE solo marca IsDeleted = true (soft delete).

- Imágenes de portada: Se guardan en wwwroot/uploads/books y se sirven con app.UseStaticFiles(). La ruta relativa se almacena en CoverImagePath y se devuelve en los DTOs.

- Arquitectura por capas:
  - Api: controladores, configuración de inicio.

  - Application: DTOs, interfaces de repositorios y queries.

  - Domain: entidades de negocio.

  - Infrastructure: implementación de repositorios, DbContext, migraciones.

# Estructura del proyecto

```
.
├── Api/                # Controladores, Program.cs, appsettings
├── Application/        # DTOs, interfaces (repositorios, unit of work)
├── Core/               # Entidades de dominio (Author, Book, Loan)
├── Infrastructure/     # DbContext, migraciones, repositorios concretos
├── frontend/           # Aplicación React (Vite)
└── README.md
```
