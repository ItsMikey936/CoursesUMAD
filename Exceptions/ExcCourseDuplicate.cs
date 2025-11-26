using System;
using System.Collections.Generic;
using System.Text;

namespace CoursesUMAD.Exceptions
{
    public class ExcCourseDuplicate: Exception
    {
        public int CourseId { get; }
        public ExcCourseDuplicate()
            : base($"A duplicate course was found.")
        {
        }
        public ExcCourseDuplicate(int courseId)
            : base($"A course with ID {courseId} already exists in the system")
        {
            CourseId = courseId;
        }
        public ExcCourseDuplicate(string message)
            : base(message)
        {
        }
        public ExcCourseDuplicate(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
