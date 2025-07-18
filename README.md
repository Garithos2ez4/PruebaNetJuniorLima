Caso Práctico: Gestión de Matrículas 

Requisitos Funcionales
Gestión de Matrículas:
o	Crear una nueva matrícula (asociada a un estudiante y un curso).
o	Modificar el estado de una matrícula (ej: "Activa", "Cancelada", "Finalizada").
o	Eliminar una matrícula (solo si está en estado "Cancelada").
o	Consultar matrículas por:
•	ID de matrícula.
•	ID de estudiante.
•	ID de curso.
•	Estado.
o	Validar que un estudiante no se matricule dos veces en el mismo curso.
 

Desarrollo Técnico
1.	Configuración del Proyecto
Tecnologías:
o	ASP.NET Core Web API (.NET 6.0 o posterior).
o	Entity Framework Core / Store Procedure (utilizar ambos).
o	SQL Server 2012 posterior.
o	Swagger/OpenAPI para documentación.

2.	Validaciones Específicas
Creación de Matrícula:
o	El estudiante y el curso deben existir.
o	No permitir matrícula duplicada (mismo StudentId y CourseId).
o	EnrollmentDate no puede ser futura.

Actualización:
o	Solo se permite cambiar el Status.
o	No permitir cambiar a "Cancelada" si ya está "Finalizada".

Eliminación:
o	Solo si el estado es "Cancelada".




Evaluación Esperada
Criterios de Éxito:
o	Proponer estructura de proyecto y aplicar buenas prácticas.
o	Correcta implementación de los endpoints CRUD.
o	Validaciones sólidas (ej: duplicados, estados).
o	Uso de Entity Framework Core (relaciones, migraciones).
o	Manejo de errores y respuestas HTTP claras.
o	Documentación en Swagger.

Bonus:
o	Implementar DTOs y AutoMapper.
o	Incluir logging (ej: Serilog).
o	Usar patrones como Repository o Unit of Work.
o	Utilizar procedimientos almacenados

Manera de Entrega de Proyecto:
•	En Repositorio GIT 
•	Enlace Drive Comprimido (zip, rar) 

# 📘 Prueba Técnica - Gestión de Matrículas (.NET 6 API)

Este proyecto consiste en una **API RESTful** construida con **ASP.NET Core Web API (.NET 6)** que permite gestionar matrículas de estudiantes en cursos.

---

## 🚀 Características

- Crear nuevas matrículas asociadas a estudiantes y cursos.
- Cambiar el estado de una matrícula ("Activa", "Cancelada", "Finalizada").
- Eliminar matrículas (solo si están "Canceladas").
- Consultar matrículas por:
  - ID de matrícula
  - ID de estudiante
  - ID de curso
  - Estado
- Validaciones de negocio:
  - No duplicar matrícula de un estudiante en el mismo curso.
  - Validar fechas y reglas de estado.

---

## ⚙️ Tecnologías Usadas

- ASP.NET Core Web API (.NET 6)
- Entity Framework Core
- SQL Server
- Swagger / OpenAPI
- DTOs y AutoMapper
- Inyección de dependencias
- Patrón Repository (opcional)
- Serilog para logging (opcional)

---

## 🛠️ Endpoints Principales

| Método | Ruta                             | Descripción                           |
|--------|----------------------------------|---------------------------------------|
| POST   | `/api/Matriculas`               | Crear matrícula                       |
| PUT    | `/api/Matriculas/{id}/estado`   | Actualizar estado de matrícula        |
| DELETE | `/api/Matriculas/{id}`          | Eliminar matrícula (si está cancelada)|

Prueba los endpoints fácilmente en Swagger:  
📎 `https://localhost:7054/swagger/index.html`

---

## 💾 Base de Datos

```sql
CREATE DATABASE MatriculasDB;
GO
USE MatriculasDB;
GO

-- Tabla: Estudiantes
CREATE TABLE Estudiante (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL
);

-- Tabla: Cursos
CREATE TABLE Curso (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL
);

-- Tabla: Matrículas
CREATE TABLE Matricula (
    Id INT PRIMARY KEY IDENTITY(1,1),
    EstudianteId INT NOT NULL,
    CursoId INT NOT NULL,
    FechaMatricula DATE NOT NULL,
    Estado NVARCHAR(20) NOT NULL CHECK (Estado IN ('Activa', 'Cancelada', 'Finalizada')),
    FOREIGN KEY (EstudianteId) REFERENCES Estudiante(Id),
    FOREIGN KEY (CursoId) REFERENCES Curso(Id),
    CONSTRAINT UQ_Student_Course UNIQUE (EstudianteId, CursoId) -- Evita duplicados
);
-- Insertar Estudiantes
INSERT INTO Estudiante (Nombre) VALUES ('Juan Pérez'), ('Ana Torres'), ('Luis García');

-- Insertar Cursos
INSERT INTO Curso (Nombre) VALUES ('Matemáticas'), ('Historia'), ('Programación');
