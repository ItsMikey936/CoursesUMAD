using System;
using System.Collections.Generic;
using System.Text;

namespace CoursesUMAD.Exceptions
{
    public class ExcStudentNotFound: Exception
    {
        public int StudentId { get; }
        public ExcStudentNotFound()
            : base($"The specified student was not found.")
        {
        }
        public ExcStudentNotFound(int studentId)
            : base($"Student with ID {studentId} was not found in the system")
        {
            StudentId = studentId;
        }
        public ExcStudentNotFound(string message)
            : base(message)
        {
        }
        public ExcStudentNotFound(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
