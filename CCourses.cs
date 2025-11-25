using System;
using System.Collections.Generic;
using System.Text;

namespace CoursesUMAD
{
    public class CCourses
    {
        //PROPERTIES
        public int course_ID { get; private set; } // Unique identifier for the course
        public string course_Name { get; set; } // Name of the course
        public string course_Intructor { get; set; } // Name of the course instructor
        public int course_Quota { get; set; } // Maximum number of students allowed in the course
        public List<int> registered_Students { get; private set; } // List of student IDs registered in the course


        //CONSTRUCTOR
        public CCourses(int id, string name, string instructor, int quota)
        {
            course_ID = id;
            course_Name = name;
            course_Intructor = instructor;
            course_Quota = quota;
            registered_Students = new List<int>();
        }

        //METHODS

        // Method to verify if there is available quota in the course
        public bool VeryfyQuota()
        {
            return registered_Students.Count < course_Quota;
        }

        // Method to register a student in the course
        public void RegisterStudent(int studentID)
        {
            if (VeryfyQuota())
            {
                //Extra check to avoid duplicate registrations
                if (registered_Students.Contains(studentID))
                {
                    throw new InvalidOperationException("Student is already registered in the course.");
                }

                registered_Students.Add(studentID);
            }
            else
            {
                throw new InvalidOperationException("Course quota exceeded.");
            }
        }

        // Method to get the current number of registered students
        public int GetRegisteredStudentCount()
        {
            return registered_Students.Count;
        }

        // Override ToString method to display course information
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Course ID: {course_ID}");
            sb.AppendLine($"Course Name: {course_Name}");
            sb.AppendLine($"Instructor: {course_Intructor}");
            sb.AppendLine($"Quota: {course_Quota}");
            sb.AppendLine($"Registered Students: {GetRegisteredStudentCount()}");
            return sb.ToString();
        }
    }
}
