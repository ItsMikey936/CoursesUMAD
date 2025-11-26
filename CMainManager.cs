using System;
using System.Collections.Generic;
using System.Linq;
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
        private Dictionary<int, List<double>> student_Grades;

        // ═══════════════════════════════════════════════════════
        //                       CONSTRUCTOR 
        // ═══════════════════════════════════════════════════════
        public CMainManager()
        {
            courses = new List<CCourses>();
            students = new List<CStudents>();
            course_Catalog = new Dictionary<int, CCourses>();
            student_Grades = new Dictionary<int, List<double>>();
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
            //Null check
            if (student == null) throw new ArgumentNullException(nameof(student), "The student cannot be null");

            //Student duplicate check
            if (students.Any(s => s.student_ID == student.student_ID))
            {
                throw new ExcStudentDuplicate($"A student with ID {student.student_ID} already exists.");
            }

            //Add student to collection
            students.Add(student);

            //Initializes the student's grades list
            student_Grades[student.student_ID] = new List<double>();

            //Confirmation message
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

        public void RegisterGrade(int studentId, double grade)
        {
            //Check grade range
            if(grade < 0 || grade > 10)
            {
                throw new ExcInvalidGrade($"The grade {grade} is invalid. It must be between 0 and 10.");
            }

            //Check if student exists
            if (students.All(s => s.student_ID != studentId))
            {
                throw new ExcStudentNotFound(studentId);
            }

            //Register grade
            student_Grades[studentId].Add(grade);

            //Confirmation message
            Console.WriteLine($"Grade {grade} registered for student ID {studentId}.");
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
        public List<double> GetStudentGrades(int studentId)
        {
            //Check if student exists in Dictionary
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
            // TODO: Obtener el alumno
            var student = GetStudentById(studentId);

            // TODO: Obtener sus calificaciones
            var grades = GetStudentGrades(studentId);

            // TODO: Calcular promedio
            double average = grades.Count > 0 ? grades.Average() : 0;

            // TODO: Mostrar reporte
            Console.WriteLine($"\n=== STUDENT REPORT ===");
            Console.WriteLine($"ID: {student.student_ID}");
            Console.WriteLine($"Name: {student.student_Name}");
            Console.WriteLine($"Email: {student.student_Email}");
            Console.WriteLine($"Enrolled Courses: {student.GetEnrolledCourseCount()}");
            Console.WriteLine($"Total Grades: {grades.Count}");
            Console.WriteLine($"Average: {average:F2}");
            Console.WriteLine($"Status: {(average >= 6.0 ? "APPROVED ✓" : "FAILED ✗")}");
        }

        /// <summary>
        /// Muestra estadísticas generales
        /// </summary>
        public void ShowStatistics()
        {
            Console.WriteLine("\n=== SYSTEM STATISTICS ===");
            Console.WriteLine($"Total Courses: {CountElements(courses)}");
            Console.WriteLine($"Total Students: {CountElements(students)}");
            Console.WriteLine($"Total Grades Registered: {student_Grades.Values.Sum(g => g.Count)}");
        }
    }
}
