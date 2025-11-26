using System;
using System.Collections.Generic;
using System.Text;

namespace CoursesUMAD.Exceptions
{
    public class ExcCourseFull: Exception
    {
        public int CourseId { get; }
        public ExcCourseFull()
            : base($"The specified course is full.")
        {
        }
        public ExcCourseFull(int courseId)
            : base($"Course with ID {courseId} is full.")
        {
            CourseId = courseId;
        }
        public ExcCourseFull(string message)
            : base(message)
        {
        }
        public ExcCourseFull(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
