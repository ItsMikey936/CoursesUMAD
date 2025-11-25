using System;
using System.Collections.Generic;
using System.Text;

namespace CoursesUMAD
{
    public class CStudents
    {
        //Properties
        public int student_ID { get; private set; }
        public string student_Name { get; set; }
        public string student_Email { get; set; }
        public List<int> enrolled_Courses { get; private set; }


        //Constructor
        public CStudents(int id, string name, string email)
        {
            student_ID = id;
            student_Name = name;
            student_Email = email;
            enrolled_Courses = new List<int>();
        }

        //Methods

        // Method to verify if the student can enroll in more courses
        public bool verifyEnrollmentLimit(int maxCourses)
        {
            return enrolled_Courses.Count < maxCourses;
        }

        // Method to enroll the student in a course
        public void EnrollInCourse(int courseID)
        {
            //Assuming a maximum of 10 courses per student
            if (verifyEnrollmentLimit(10)) 
            {
                // Extra check to avoid duplicate enrollments
                if (enrolled_Courses.Contains(courseID))
                {
                    throw new InvalidOperationException("Student is already enrolled in the course.");
                }
                enrolled_Courses.Add(courseID);
            }
            else
            {
                throw new InvalidOperationException("Student has reached the maximum number of enrolled courses.");
            }
        }

        // Method to get the current number of enrolled courses
        public int GetEnrolledCourseCount()
        {
            return enrolled_Courses.Count;
        }

        // Method to check if the student is enrolled in a specific course
        public bool IsEnrolledInCourse(int courseID)
        {
            return enrolled_Courses.Contains(courseID);
        }

        // Override ToString method to display student information
        public override string ToString()
        {
            return $"Student ID: {student_ID}, Name: {student_Name}, Email: {student_Email}, Enrolled Courses: {string.Join(", ", enrolled_Courses)}";
        }
    }
}
