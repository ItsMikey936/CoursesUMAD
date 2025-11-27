using System;
using System.Collections.Generic;
using System.Linq;
using Evaluaciones;
using CoursesUMAD.Exceptions;

namespace CoursesUMAD
{
    public class CMainManager
    {
        // ═══════════════════════════════════════════════════════
        //                      COLLECTIONS 
        // ═══════════════════════════════════════════════════════

        private List<CCourses> courses;
        private List<CStudents> students;
        private Dictionary<int, CCourses> course_Catalog;
        private Dictionary<int, List<Calificacion>> student_Grades;

        // ═══════════════════════════════════════════════════════
        //                       CONSTRUCTOR 
        // ═══════════════════════════════════════════════════════
        public CMainManager()
        {
            courses = new List<CCourses>();
            students = new List<CStudents>();
            course_Catalog = new Dictionary<int, CCourses>();
            student_Grades = new Dictionary<int, List<Calificacion>>(); 
        }

        // ═══════════════════════════════════════════════════════
        //                   REGISTER METHODS
        // ═══════════════════════════════════════════════════════

        /// <summary>
        /// Registers a new course in the system.
        /// </summary>
        public void RegisterCourse(CCourses course)
        {
            //Null check
            if (course == null) throw new ArgumentNullException(nameof(course), "The course cannot be null");

            //Duplicate check
            if (course_Catalog.ContainsKey(course.course_ID))
            {
                throw new ExcCourseDuplicate($"A course with ID {course.course_ID} already exists.");
            }

            //Add course to collections
            courses.Add(course);
            course_Catalog.Add(course.course_ID, course);

            //Confirmation message
            Console.WriteLine($"Course '{course.course_Name}' with ID {course.course_ID} registered successfully.");
        }

        /// <summary>
        /// Registra un alumno en el sistema
        /// </summary>
        public void RegisterStudent(CStudents student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student), "The student cannot be null");

            if (students.Any(s => s.student_ID == student.student_ID))
            {
                throw new ExcStudentDuplicate($"A student with ID {student.student_ID} already exists.");
            }

            students.Add(student);

            // Inicializar con lista de objetos Calificacion
            student_Grades[student.student_ID] = new List<Calificacion>();  // ← Cambio aquí

            Console.WriteLine($"Student '{student.student_Name}' with ID {student.student_ID} registered successfully.");
        }

        // ═══════════════════════════════════════════════════════
        //                  INSCRIPTION METHODS
        // ═══════════════════════════════════════════════════════

        /// <summary>
        /// Inscribes a student in a course.
        /// </summary>
        public void EnrollStudentInCourse(int studentId, int courseId)
        {
            //Check if student exists
            if (students.All(s => s.student_ID != studentId))
            {
                throw new ExcStudentNotFound(studentId);
            }

            //Check if course exists
            if (!course_Catalog.ContainsKey(courseId))
            {
                throw new ExcCourseNotFound(courseId);
            }

            //Get course
            CCourses course = course_Catalog[courseId];
            CStudents student = students.First(s => s.student_ID == studentId);

            //Check quota
            if (!course.VeryfyQuota())
            {
                throw new ExcCourseFull($"The course '{course.course_Name}' (ID: {course.course_ID}) is full.");
            }

            //Register student in course
            course.RegisterStudent(studentId);

            //Enroll Student in a course
            student.EnrollInCourse(courseId);

            //Confirmation message
            Console.WriteLine($"Student ID {studentId} enrolled in course '{course.course_Name}' (ID: {course.course_ID}) successfully.");
        }

        /// ═══════════════════════════════════════════════════════
        ///                GRADE MANAGEMENT METHODS
        /// =══════════════════════════════════════════════════════

        /// <summary>
        /// Register a grade for a student.
        /// </summary>

        /// <summary>
        /// Registra una calificación para un alumno en un curso específico
        /// </summary>
        public void RegisterGrade(int studentId, int courseId, double grade, string concept = "General")
        {
            // Validar rango
            if (!Evaluador.IsValidGrade(grade))
            {
                throw new ExcInvalidGrade(grade, Evaluador.MinimumGrade, Evaluador.MaximumGrade);
            }

            // Verificar que el alumno existe
            if (!student_Grades.ContainsKey(studentId))
            {
                throw new ExcStudentNotFound(studentId);
            }

            // Verificar que el curso existe
            if (!course_Catalog.ContainsKey(courseId))
            {
                throw new ExcCourseNotFound(courseId);
            }

            // Crear objeto Calificacion completo usando el ensamblado
            Calificacion nuevaCalificacion = new Calificacion(studentId, courseId, grade, concept);

            // Agregar a la colección
            student_Grades[studentId].Add(nuevaCalificacion);

            Console.WriteLine($"Grade {grade} ({concept}) registered for student {studentId} in course {courseId}.");
        }

        /// <summary>
        /// Sobrecarga simplificada (sin especificar curso ni concepto)
        /// Para compatibilidad con código anterior
        /// </summary>
        public void RegisterGrade(int studentId, double grade)
        {
            RegisterGrade(studentId, 0, grade, "General");  // courseId = 0 indica "sin curso específico"
        }


        /// ═══════════════════════════════════════════════════════
        ///                    SEARCH METHODS
        /// =══════════════════════════════════════════════════════

        /// <summary>
        /// Search for a course by its ID.
        /// </summary>
        public CCourses GetCourseById(int courseId)
        {
            if (!course_Catalog.ContainsKey(courseId))
            {
                throw new ExcCourseNotFound(courseId);
            }
            return course_Catalog[courseId];
        }

        /// <summary>
        /// Search for a student by their ID.
        /// </summary>
        public CStudents GetStudentById(int studentId)
        {
            var student = students.FirstOrDefault(s => s.student_ID == studentId);

            if (student == null)
            {
                throw new ExcStudentNotFound(studentId);
            }

            return student;
        }

        ///<summary>
        ///Get student grades
        /// </summary>
        /// <summary>
        /// Obtiene solo los valores numéricos de las calificaciones de un alumno
        /// </summary>
        public List<double> GetStudentGrades(int studentId)
        {
            if (!student_Grades.ContainsKey(studentId))
            {
                throw new ExcStudentNotFound(studentId);
            }

            // Extraer solo los valores numéricos de los objetos Calificacion
            return student_Grades[studentId].Select(c => c.Grade).ToList();
        }

        /// <summary>
        /// Obtiene los objetos Calificacion completos de un alumno
        /// </summary>
        public List<Calificacion> GetStudentGradeObjects(int studentId)
        {
            if (!student_Grades.ContainsKey(studentId))
            {
                throw new ExcStudentNotFound(studentId);
            }

            return student_Grades[studentId];
        }

        /// ══════════════════════════════════════════════════════
        ///                    GENERIC METHODS
        /// ══════════════════════════════════════════════════════

        /// <summary>
        /// Display any list of generic way.
        /// </summary>
        public void ShowList<T>(List<T> list, string listName)
        {
            Console.WriteLine($"\n=== {listName} ===");

            if (list.Count == 0)
            {
                Console.WriteLine("No items found.");
                return;
            }

            foreach (T item in list)
            {
                Console.WriteLine(item);
            }
        }

        ///<sumary>
        /// Display items in any listy
        /// </sumary>
        public int CountElements<T>(List<T> list)
        {
            return list.Count;
        }


        // ═══════════════════════════════════════════════════════
        // MÉTODOS DE REPORTE
        // ═══════════════════════════════════════════════════════

        /// <summary>
        /// Muestra todos los cursos
        /// </summary>
        public void ShowAllCourses()
        {
            ShowList(courses, "REGISTERED COURSES");
        }

        /// <summary>
        /// Muestra todos los alumnos
        /// </summary>
        public void ShowAllStudents()
        {
            ShowList(students, "REGISTERED STUDENTS");
        }

        /// <summary>
        /// Muestra el reporte de un alumno
        /// </summary>
        public void ShowStudentReport(int studentId)
        {
            var student = GetStudentById(studentId);
            var grades = GetStudentGrades(studentId);

            // Usar el GeneradorReportes del ensamblado externo
            string report = GeneradorReportes.GenerateStudentReport(
                student.student_Name,
                student.student_ID,
                grades
            );

            Console.WriteLine(report);
        }

        /// <summary>
        /// Muestra estadísticas generales
        /// </summary>
        public void ShowStatistics()
        {
            Console.WriteLine("\n╔════════════════════════════════════════════════╗");
            Console.WriteLine("║            SYSTEM STATISTICS                   ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝");
            Console.WriteLine($"Total Courses:         {CountElements(courses)}");
            Console.WriteLine($"Total Students:        {CountElements(students)}");

            // Contar todas las calificaciones registradas
            int totalGrades = student_Grades.Values.Sum(list => list.Count);
            Console.WriteLine($"Total Grades:          {totalGrades}");

            // Estadísticas adicionales usando el ensamblado
            if (totalGrades > 0)
            {
                var allGrades = student_Grades.Values
                    .SelectMany(list => list)
                    .Select(c => c.Grade)
                    .ToList();

                double systemAverage = Evaluador.CalculateAverage(allGrades);
                double highest = Evaluador.GetHighestGrade(allGrades);
                double lowest = Evaluador.GetLowestGrade(allGrades);
                int approved = Evaluador.CountApprovedGrades(allGrades);

                Console.WriteLine($"System Average:        {systemAverage:F2}");
                Console.WriteLine($"Highest Grade:         {highest:F2}");
                Console.WriteLine($"Lowest Grade:          {lowest:F2}");
                Console.WriteLine($"Approved Grades:       {approved}");
                Console.WriteLine($"Failed Grades:         {totalGrades - approved}");
            }
        }

        /// <summary>
        /// Muestra todas las calificaciones de un alumno con detalles completos
        /// </summary>
        public void ShowDetailedGrades(int studentId)
        {
            var student = GetStudentById(studentId);
            var gradeObjects = GetStudentGradeObjects(studentId);

            Console.WriteLine($"\n╔════════════════════════════════════════════════╗");
            Console.WriteLine($"║     DETAILED GRADES FOR {student.student_Name,-22} ║");
            Console.WriteLine($"╚════════════════════════════════════════════════╝");

            if (gradeObjects.Count == 0)
            {
                Console.WriteLine("No grades registered.");
                return;
            }

            Console.WriteLine();
            foreach (var calificacion in gradeObjects)
            {
                Console.WriteLine(calificacion);  // Usa el ToString() de Calificacion
            }
        }

        /// <summary>
        /// Obtiene las calificaciones de un alumno en un curso específico
        /// </summary>
        public List<Calificacion> GetGradesByCourse(int studentId, int courseId)
        {
            var allGrades = GetStudentGradeObjects(studentId);
            return allGrades.Where(c => c.CourseId == courseId).ToList();
        }

        /// <summary>
        /// Muestra el promedio de un alumno en un curso específico
        /// </summary>
        public void ShowCourseAverage(int studentId, int courseId)
        {
            var student = GetStudentById(studentId);
            var course = GetCourseById(courseId);
            var courseGrades = GetGradesByCourse(studentId, courseId);

            if (courseGrades.Count == 0)
            {
                Console.WriteLine($"\n{student.student_Name} has no grades in {course.course_Name}");
                return;
            }

            double average = courseGrades.Select(c => c.Grade).Average();
            string status = Evaluador.IsApproved(average) ? "APPROVED ✓" : "FAILED ✗";

            Console.WriteLine($"\n╔════════════════════════════════════════════════╗");
            Console.WriteLine($"║          COURSE PERFORMANCE REPORT             ║");
            Console.WriteLine($"╚════════════════════════════════════════════════╝");
            Console.WriteLine($"Student:  {student.student_Name}");
            Console.WriteLine($"Course:   {course.course_Name}");
            Console.WriteLine($"Grades:   {courseGrades.Count}");
            Console.WriteLine($"Average:  {average:F2}");
            Console.WriteLine($"Status:   {status}");
        }
    }
}