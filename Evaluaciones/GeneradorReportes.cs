using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Evaluaciones
{
    public static class GeneradorReportes
    {
        public static string GenerateStudentReport(
            string studentName,
            int studentId,
            List<double> grades)
        {
            StringBuilder report = new StringBuilder();

            report.AppendLine("╔════════════════════════════════════════════════╗");
            report.AppendLine("║           STUDENT PERFORMANCE REPORT           ║");
            report.AppendLine("╚════════════════════════════════════════════════╝");
            report.AppendLine();

            report.AppendLine($"Student Name: {studentName}");
            report.AppendLine($"Student ID:   {studentId}");
            report.AppendLine($"Report Date:  {DateTime.Now:yyyy-MM-dd HH:mm}");
            report.AppendLine();

            if (grades == null || grades.Count == 0)
            {
                report.AppendLine("No grades registered for this student.");
                return report.ToString();
            }

            double average = Evaluador.CalculateAverage(grades);
            double highest = Evaluador.GetHighestGrade(grades);
            double lowest = Evaluador.GetLowestGrade(grades);
            double median = Evaluador.CalculateMedian(grades);
            int approved = Evaluador.CountApprovedGrades(grades);
            int failed = Evaluador.CountFailedGrades(grades);
            string letterGrade = Evaluador.GetLetterGrade(average);
            bool isApproved = Evaluador.IsApproved(average);

            report.AppendLine("─────────────── GRADE STATISTICS ───────────────");
            report.AppendLine($"Total Grades:      {grades.Count}");
            report.AppendLine($"Average:           {average:F2}");
            report.AppendLine($"Letter Grade:      {letterGrade}");
            report.AppendLine($"Median:            {median:F2}");
            report.AppendLine($"Highest Grade:     {highest:F2}");
            report.AppendLine($"Lowest Grade:      {lowest:F2}");
            report.AppendLine($"Approved Grades:   {approved}");
            report.AppendLine($"Failed Grades:     {failed}");
            report.AppendLine();

            report.AppendLine("─────────────── FINAL STATUS ───────────────────");
            if (isApproved)
            {
                report.AppendLine($"Status: ✓ APPROVED (Average: {average:F2})");
            }
            else
            {
                report.AppendLine($"Status: ✗ FAILED (Average: {average:F2})");
            }

            report.AppendLine("════════════════════════════════════════════════");

            return report.ToString();
        }

        public static string GenerateGradeTable(List<double> grades)
        {
            if (grades == null || grades.Count == 0)
            {
                return "No grades to display.";
            }

            StringBuilder table = new StringBuilder();

            table.AppendLine("┌─────┬─────────┬────────┬────────┐");
            table.AppendLine("│  #  │  Grade  │ Letter │ Status │");
            table.AppendLine("├─────┼─────────┼────────┼────────┤");

            for (int i = 0; i < grades.Count; i++)
            {
                double grade = grades[i];
                string letter = Evaluador.GetLetterGrade(grade);
                string status = grade >= Evaluador.MinimumApprovedGrade ? "✓" : "✗";

                table.AppendLine($"│ {i + 1,3} │  {grade,5:F2}  │   {letter,-4} │   {status,-4} │");
            }

            table.AppendLine("└─────┴─────────┴────────┴────────┘");

            return table.ToString();
        }
    }
}