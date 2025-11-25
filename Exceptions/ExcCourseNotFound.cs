using System;
using System.Collections.Generic;
using System.Text;

namespace CoursesUMAD.Exceptions
{
    public class ExcCourseNotFound: Exception
    {
        public int CourseId { get; }
        public ExcCourseNotFound()
            : base($"The specified course was not found.")
        {
        }
        public ExcCourseNotFound(int courseId)
            : base($"Course with ID {courseId} was not found in the system")
        {
            CourseId = courseId;
        }
        public ExcCourseNotFound(string message)
            : base(message)
        {
        }
        public ExcCourseNotFound(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
