# Shortly

Sistema web de acortamiento de enlaces desarrollado con ASP.NET Core Razor Pages, Entity Framework Core y SQLite.

## Descripción

Shortly es una aplicación web que permite a los usuarios registrarse, iniciar sesión y generar enlaces cortos a partir de URLs originales. El sistema almacena los enlaces generados y registra la cantidad de accesos realizados a cada uno.

El proyecto fue desarrollado siguiendo una arquitectura por capas, separando responsabilidades entre dominio, aplicación, infraestructura y presentación.

---

# Tecnologías Utilizadas

- ASP.NET Core Razor Pages
- C#
- Entity Framework Core
- SQLite
- Serilog
- Bootstrap 5
- BCrypt.Net

---

# Características

- Registro de usuarios
- Inicio y cierre de sesión
- Autenticación mediante Cookies
- Encriptación segura de contraseñas
- Creación de enlaces cortos
- Persistencia de datos con SQLite
- Arquitectura por capas
- Logging con Serilog
- Interfaz responsiva con Bootstrap

---

# Arquitectura del Proyecto

El proyecto está organizado en distintas capas para mantener una separación clara de responsabilidades.

```txt
Shortly/
│
├── Application/
│   ├── Interfaces/
│   └── Services/
│
├── Domain/
│   └── Entities/
│
├── Infrastructure/
│   ├── Persistence/
│   └── Seed/
│
├── Pages/
│   ├── Login
│   ├── Register
│   ├── Dashboard
│   └── Shared/
│
├── wwwroot/
│
└── Program.cs
```

---

# Entidades Principales

## User

Representa un usuario registrado en el sistema.

### Propiedades

- Id
- Email
- Password
- Links

---

## Link

Representa un enlace acortado generado por un usuario.

### Propiedades

- Id
- Url
- ShortUrl
- Clicks
- UserId

---

# Configuración del Proyecto

## Requisitos

- .NET SDK 10
- Visual Studio Code o Visual Studio
- SQLite

Verificar instalación:

```bash
dotnet --version
```

---

# Instalación

## 1. Clonar repositorio

```bash
git clone https://github.com/USUARIO/Shortly.git
```

## 2. Entrar al proyecto

```bash
cd Shortly
```

## 3. Restaurar dependencias

```bash
dotnet restore
```

## 4. Ejecutar proyecto

```bash
dotnet run
```

---

# Acceso a la Aplicación

Una vez ejecutado el proyecto:

```txt
http://localhost:5176
```

---

# Configuración de Base de Datos

El proyecto utiliza SQLite.

La conexión se configura en:

```txt
appsettings.json
```

```json
"ConnectionStrings": {
  "AppDbContext": "Data Source=database.db"
}
```

---

# Flujo de Uso

1. Crear una cuenta
2. Iniciar sesión
3. Acceder al Dashboard
4. Crear enlaces cortos
5. Visualizar enlaces generados
6. Cerrar sesión

---

# Seguridad

- Contraseñas almacenadas utilizando BCrypt hashing.
- Autenticación mediante Cookies Authentication.
- Validación de usuarios duplicados.
- Protección de rutas mediante autorización.

---

# Logging

El proyecto utiliza Serilog para registrar:

- Registro de usuarios
- Inicio de sesión
- Creación de enlaces
- Errores y excepciones
- Consultas a base de datos

---

# Dependencias Principales

```xml
<PackageReference Include="BCrypt.Net-Next" Version="4.2.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="10.0.8" />
<PackageReference Include="Serilog.AspNetCore" Version="10.0.0" />
```

---

# Posibles Mejoras Futuras

- Estadísticas avanzadas
- Generación automática de QR
- Panel administrativo
- Recuperación de contraseña
- Diseño UI moderno
- API REST
- Deploy en Azure o Render
- Links personalizados
- Expiración de enlaces
- Dashboard con gráficos

---

# Autor

Desarrollado por Jorge Nuñez.
