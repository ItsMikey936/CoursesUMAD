using System;
using System.Collections.Generic;
using System.Text;

namespace CoursesUMAD.Exceptions
{
    public class ExcInvalidGrade: Exception
    {
        public double Grade { get; }
        public double MinGrade { get; }
        public double MaxGrade { get; }

        public ExcInvalidGrade()
            : base($"The specified grade is invalid.")
        {
        }

        public ExcInvalidGrade(double grade, double minGrade, double maxGrade)
            : base($"The grade {grade} is invalid. It must be between {minGrade} and {maxGrade}.")
        {
            Grade = grade;
            MinGrade = minGrade;
            MaxGrade = maxGrade;
        }

        public ExcInvalidGrade(string message)
            : base(message)
        {
        }

        public ExcInvalidGrade(string message, Exception innerException)
            : base(message, innerException)
        {
        }   
    }
}
