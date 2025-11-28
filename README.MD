# Sistema Gestor de Cursos en Línea (CoursesUMAD)

## 📋 Descripción del Proyecto
Sistema de gestión académica que permite administrar cursos, alumnos, instructores y calificaciones dentro de una plataforma educativa en línea. Este sistema simula el backend de un LMS (Learning Management System).

## 👥 Integrantes
- Miguel Angel Arellano Juárez
- 20108247
  
- Edgar Omar Ganem Juarez
- 20111357
  
- [Tu Nombre]
- [Matrícula]

## 🎓 Información Académica
- **Materia:** Programación Orientada a Objetos / Estructuras de Datos
- **Fecha:** Noviembre 2024

## 🚀 Características Principales
- ✅ Registro de cursos y alumnos
- ✅ Inscripción de alumnos en cursos con validación de cupo
- ✅ Sistema de calificaciones con validación de rangos
- ✅ Generación de reportes de rendimiento
- ✅ Manejo robusto de excepciones personalizadas
- ✅ Ensamblado independiente para evaluaciones (Evaluaciones.dll)

## 🛠️ Tecnologías Utilizadas
- **Lenguaje:** C# (.NET 10.0)
- **IDE:** Visual Studio 2026
- **Paradigma:** Programación Orientada a Objetos
- **Colecciones:** List<T>, Dictionary<TKey, TValue>
- **Características:** Genéricos, Excepciones Personalizadas, Ensamblados

## 📂 Estructura del Proyecto
```
src/
├── CoursesUMAD/          # Proyecto principal
└── Evaluaciones/         # Ensamblado de evaluaciones

libs/                     # Bibliotecas compiladas
```

## 🔧 Requisitos del Sistema
- .NET 10.0 SDK o superior
- Windows 10/11
- Visual Studio 2026

## 📥 Instalación y Ejecución

### Opción 1: Con Visual Studio
1. Abrir `CoursesUMAD.sln`
2. Compilar la solución (Ctrl + Shift + B)
3. Ejecutar (F5)

### Opción 2: Con Terminal
```bash
# Clonar/descargar el proyecto
cd src/CoursesUMAD

# Compilar
dotnet build

# Ejecutar
dotnet run
```

## 📖 Manual de Uso

### 1. Registrar un Curso
- Opción: 1
- Ingresar: ID, Nombre, Instructor, Cupo máximo

### 2. Registrar un Alumno
- Opción: 2
- Ingresar: ID, Nombre, Email

### 3. Inscribir Alumno en Curso
- Opción: 3
- Ingresar: ID del alumno, ID del curso
- El sistema valida cupo disponible automáticamente

### 4. Registrar Calificación
- Opción: 4
- Ingresar: ID del alumno, ID del curso, Calificación (0-10), Concepto

### 5. Ver Reportes
- Opción: 5
- Submenú con reportes de cursos, alumnos y estadísticas

## 🧪 Casos de Prueba

### Caso 1: Registro Exitoso
```
1. Registrar curso ID=101, "C# Básico", Prof. García, Cupo=3
2. Registrar alumno ID=1, "Juan Pérez", juan@email.com
3. Inscribir alumno 1 en curso 101
✓ Resultado: Inscripción exitosa
```

### Caso 2: Validación de Cupo
```
1. Curso 101 con cupo=2
2. Inscribir 3 alumnos
✓ Resultado: ExcCourseFull en el tercer intento
```

### Caso 3: Calificación Inválida
```
1. Intentar registrar calificación = 15
✓ Resultado: ExcInvalidGrade (rango 0-10)
```

## 🏗️ Arquitectura del Sistema

### Colecciones Utilizadas
- `List<CCourses>`: Lista de todos los cursos
- `List<CStudents>`: Lista de todos los alumnos
- `Dictionary<int, CCourses>`: Catálogo de cursos por ID (acceso O(1))
- `Dictionary<int, List<Calificacion>>`: Calificaciones por alumno

### Excepciones Personalizadas
- `ExcStudentDuplicate`: Alumno ya registrado
- `ExcStudentNotFound`: Alumno no existe
- `ExcCourseDuplicate`: Curso ya registrado
- `ExcCourseNotFound`: Curso no existe
- `ExcCourseFull`: Curso sin cupo
- `ExcInvalidGrade`: Calificación fuera de rango

### Ensamblado Evaluaciones.dll
Biblioteca independiente que contiene:
- `Calificacion`: Clase para representar calificaciones
- `Evaluador`: Métodos estáticos para cálculos (promedio, mediana, etc.)
- `GeneradorReportes`: Generación de reportes formateados

## 🎯 Genéricos Implementados
```csharp
// Método genérico para mostrar cualquier lista
public void ShowList<T>(List<T> list, string listName)

// Método genérico para contar elementos
public int CountElements<T>(List<T> list)
```

## ⚠️ Manejo de Errores
El sistema implementa validaciones exhaustivas:
- Null checks en todos los métodos públicos
- Validación de rangos para calificaciones (0-10)
- Verificación de existencia antes de operaciones
- Validación de cupo disponible
- Try-catch en el Main para captura global

## 📊 Diagramas
Ver carpeta `docs/` para:
- Diagrama de clases UML
- Diagrama de flujo de inscripción
- Diagrama de arquitectura

## 🐛 Problemas Conocidos
Ninguno reportado hasta la fecha.

## 📄 Licencia
Proyecto académico - Universidad Madero

---
**Última actualización:** Noviembre 2024
```

