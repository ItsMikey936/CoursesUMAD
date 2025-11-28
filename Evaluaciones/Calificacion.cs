using System;

namespace Evaluaciones
{
    public class Calificacion
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public double Grade { get; set; }
        public DateTime Date { get; set; }
        public string Concept { get; set; }

        public Calificacion(int studentId, int courseId, double grade, string concept)
        {
            StudentId = studentId;
            CourseId = courseId;
            Grade = grade;
            Concept = concept;
            Date = DateTime.Now;
        }

        public Calificacion(int studentId, int courseId, double grade)
            : this(studentId, courseId, grade, "General")
        {
        }

        public bool IsApproved(double minimumGrade = 6.0)
        {
            return Grade >= minimumGrade;
        }

        public override string ToString()
        {
            return $"[{Date:yyyy-MM-dd}] Student: {StudentId} | Course: {CourseId} | " +
                   $"Grade: {Grade:F2} | Concept: {Concept} | " +
                   $"Status: {(IsApproved() ? "APPROVED ✓" : "FAILED ✗")}";
        }
    }
}