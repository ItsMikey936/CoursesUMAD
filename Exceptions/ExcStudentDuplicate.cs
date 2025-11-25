using System;
using System.Collections.Generic;
using System.Text;

namespace CoursesUMAD.Exceptions
{
    public class ExcStudentDuplicate: Exception
    {
        public int StudentId { get; }

        public ExcStudentDuplicate()
            : base($"A student with this ID already exists.")
        {
        }

        public ExcStudentDuplicate(int studentId)
            : base($"Student with ID {studentId} is already registered in the system")
        {
            StudentId = studentId;
        }

        public ExcStudentDuplicate(string message)
            : base(message)
        {
        }

        public ExcStudentDuplicate(string message, Exception innerException)
            : base(message, innerException)
        {
        }   
    }
}
