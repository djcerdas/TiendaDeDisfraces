# Universidad Latina de Costa Rica  
## Facultad de Ingeniería de Software  
### Proyecto: TiendaDeDisfraces  

**Curso:** Proyecto de Ingeniería del Software I  

**Estudiantes:**  
- David Jesús Cerdas Pérez  
- Jeremy Granados Araya  
- Michael Aramis Cabrera Hodgson  

---

# Descripción
Aplicación web en ASP.NET Core MVC para la gestión de:

- Productos  
- Usuarios  

Incluye operaciones CRUD, validación de datos, manejo de roles y almacenamiento seguro de contraseñas.

---

# Modelo conceptual

## Diagrama de clases

Usuario  
- Id  
- Cedula  
- Nombre  
- Correo  
- Telefono  
- Username  
- Password  
- Rol  
- FechaNacimiento  
- Preferencial  
- Edad (calculado)  
- TipoPublico (calculado)  

Cliente (hereda de Usuario)  

Producto  
- Id  
- Nombre  
- Precio  
- Descripcion  
- Tipo  
- Publico  
- Talla  

---

# Componentes y Tecnologías

- IDE: Visual Studio 2026 (Community Edition)  
- Terminal: WSL  
- Lenguaje: C#  
- Framework: ASP.NET Core MVC  
- Base de datos: Entity Framework Core  

---

# Patrón de programación MVC

- Model → `Producto.cs`, `Usuario.cs`  
- View → `Views/Producto`, `Views/Usuario`  
- Controller → `ProductoController`, `UsuarioController`  

Flujo:
1. Usuario interactúa con la vista  
2. El controlador procesa la solicitud  
3. El modelo gestiona los datos  
4. Se retorna la vista actualizada  

---

# Lógica del sistema

## Usuario
- Edad calculada a partir de `FechaNacimiento`  
- Clasificación automática:
  - Adulto
  - Niño  
- Rol define comportamiento del sistema  
- Campo `Preferencial` aplica únicamente a clientes  

## Producto
- Tipos:
  - Disfraz
  - Accesorio
  - Maquillaje  
- Público:
  - Niño
  - Adulto
  - Todo público  
- Talla aplicable a disfraces  

---
# CRUD

## Producto
- Create → registro de productos  
- Read → listado de productos  
- Update → edición de productos  
- Delete → eliminación de productos  

## Usuario
- Create → registro de usuarios  
- Read → listado de usuarios  
- Update → edición de usuarios  
- Delete → eliminación de usuarios  

---

# Instalación

1. Abrir el proyecto en Visual Studio  

2. Ejecutar en WSL:dotnet restore

3. Verificar configuración en `appsettings.json`  

4. Actualizar la base de datos:
- Tools → NuGet Package Manager → Package Manager Console  
Update-Database

5. Ejecutar el proyecto:
- Presionar F5  
