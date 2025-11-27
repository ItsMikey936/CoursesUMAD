using System;
using CoursesUMAD.Exceptions;

namespace CoursesUMAD
{
    class Program
    {
        static CMainManager manager = new CMainManager();

        static void Main(string[] args)
        {
            bool continuar = true;

            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║   ONLINE COURSE MANAGEMENT SYSTEM      ║");
            Console.WriteLine("╚════════════════════════════════════════╝\n");

            while (continuar)
            {
                MostrarMenu();
                string opcion = Console.ReadLine();

                try
                {
                    switch (opcion)
                    {
                        case "1":
                            RegistrarCurso();
                            break;
                        case "2":
                            RegistrarAlumno();
                            break;
                        case "3":
                            InscribirAlumno();
                            break;
                        case "4":
                            RegistrarCalificacion();
                            break;
                        case "5":
                            MostrarReportes();
                            break;
                        case "6":
                            MostrarEstadisticas();
                            break;
                        case "0":
                            continuar = false;
                            Console.WriteLine("\n✓ Thank you for using the system. Goodbye!");
                            break;
                        default:
                            Console.WriteLine("\n❌ Invalid option. Please try again.");
                            break;
                    }
                }
                catch (ExcStudentDuplicate ex)
                {
                    Console.WriteLine($"\n❌ ERROR: {ex.Message}");
                }
                catch (ExcStudentNotFound ex)
                {
                    Console.WriteLine($"\n❌ ERROR: {ex.Message}");
                }
                catch (ExcCourseDuplicate ex)
                {
                    Console.WriteLine($"\n❌ ERROR: {ex.Message}");
                }
                catch (ExcCourseNotFound ex)
                {
                    Console.WriteLine($"\n❌ ERROR: {ex.Message}");
                }
                catch (ExcCourseFull ex)
                {
                    Console.WriteLine($"\n❌ ERROR: {ex.Message}");
                }
                catch (ExcInvalidGrade ex)
                {
                    Console.WriteLine($"\n❌ ERROR: {ex.Message}");
                }
                catch (FormatException)
                {
                    Console.WriteLine("\n❌ ERROR: Invalid input format. Please enter valid data.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n❌ UNEXPECTED ERROR: {ex.Message}");
                }

                if (continuar)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        static void MostrarMenu()
        {
            Console.WriteLine("\n╔════════════════════════════════════════╗");
            Console.WriteLine("║              MAIN MENU                 ║");
            Console.WriteLine("╠════════════════════════════════════════╣");
            Console.WriteLine("║  1. Register Course                    ║");
            Console.WriteLine("║  2. Register Student                   ║");
            Console.WriteLine("║  3. Enroll Student in Course           ║");
            Console.WriteLine("║  4. Register Grade                     ║");
            Console.WriteLine("║  5. Show Reports                       ║");
            Console.WriteLine("║  6. Show Statistics                    ║");
            Console.WriteLine("║  0. Exit                               ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.Write("\nSelect an option: ");
        }

        static void RegistrarCurso()
        {
            Console.WriteLine("\n╔════════════════════════════════════════╗");
            Console.WriteLine("║         REGISTER NEW COURSE            ║");
            Console.WriteLine("╚════════════════════════════════════════╝\n");

            // TODO: Captura de datos del curso
            Console.Write("Course ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Course Name: ");
            string nombre = Console.ReadLine();

            Console.Write("Instructor Name: ");
            string instructor = Console.ReadLine();

            Console.Write("Maximum Quota: ");
            int cupo = int.Parse(Console.ReadLine());

            // Crear y registrar el curso
            CCourses curso = new CCourses(id, nombre, instructor, cupo);
            manager.RegisterCourse(curso);
        }

        static void RegistrarAlumno()
        {
            Console.WriteLine("\n╔════════════════════════════════════════╗");
            Console.WriteLine("║         REGISTER NEW STUDENT           ║");
            Console.WriteLine("╚════════════════════════════════════════╝\n");

            // TODO: Captura de datos del alumno
            Console.Write("Student ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Student Name: ");
            string nombre = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            // Crear y registrar el alumno
            CStudents alumno = new CStudents(id, nombre, email);
            manager.RegisterStudent(alumno);
        }

        static void InscribirAlumno()
        {
            Console.WriteLine("\n╔════════════════════════════════════════╗");
            Console.WriteLine("║      ENROLL STUDENT IN COURSE          ║");
            Console.WriteLine("╚════════════════════════════════════════╝\n");

            // TODO: Captura de IDs
            Console.Write("Student ID: ");
            int studentId = int.Parse(Console.ReadLine());

            Console.Write("Course ID: ");
            int courseId = int.Parse(Console.ReadLine());

            // Inscribir
            manager.EnrollStudentInCourse(studentId, courseId);
        }

        static void RegistrarCalificacion()
        {
            Console.WriteLine("\n╔════════════════════════════════════════════════╗");
            Console.WriteLine("║              REGISTER GRADE                    ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝\n");

            Console.Write("Student ID: ");
            int studentId = int.Parse(Console.ReadLine());

            Console.Write("Course ID: ");
            int courseId = int.Parse(Console.ReadLine());

            Console.Write("Grade (0-10): ");
            double calificacion = double.Parse(Console.ReadLine());

            Console.Write("Concept (Parcial 1, Final, etc.) [Enter for 'General']: ");
            string concepto = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(concepto))
                concepto = "General";

            // Registrar con curso y concepto
            manager.RegisterGrade(studentId, courseId, calificacion, concepto);
        }

        static void MostrarReportes()
        {
            Console.WriteLine("\n╔════════════════════════════════════════════════╗");
            Console.WriteLine("║                  REPORTS                       ║");
            Console.WriteLine("╠════════════════════════════════════════════════╣");
            Console.WriteLine("║  1. Show All Courses                           ║");
            Console.WriteLine("║  2. Show All Students                          ║");
            Console.WriteLine("║  3. Show Student Report                        ║");
            Console.WriteLine("║  4. Show Detailed Grades                       ║");  // ← NUEVO
            Console.WriteLine("║  5. Show Course Average for Student            ║");  // ← NUEVO
            Console.WriteLine("║  0. Back to Main Menu                          ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝");
            Console.Write("\nSelect an option: ");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    manager.ShowAllCourses();
                    break;
                case "2":
                    manager.ShowAllStudents();
                    break;
                case "3":
                    Console.Write("\nEnter Student ID: ");
                    int studentId = int.Parse(Console.ReadLine());
                    manager.ShowStudentReport(studentId);
                    break;
                case "4":  // NUEVO
                    Console.Write("\nEnter Student ID: ");
                    int sid = int.Parse(Console.ReadLine());
                    manager.ShowDetailedGrades(sid);
                    break;
                case "5":  // NUEVO
                    Console.Write("\nEnter Student ID: ");
                    int studentId2 = int.Parse(Console.ReadLine());
                    Console.Write("Enter Course ID: ");
                    int courseId = int.Parse(Console.ReadLine());
                    manager.ShowCourseAverage(studentId2, courseId);
                    break;
                case "0":
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }

        static void MostrarEstadisticas()
        {
            manager.ShowStatistics();
        }
    }
}