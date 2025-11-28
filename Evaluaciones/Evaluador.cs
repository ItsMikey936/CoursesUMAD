using System;
using System.Collections.Generic;
using System.Linq;

namespace Evaluaciones
{
    public static class Evaluador
    {
        public const double MinimumApprovedGrade = 6.0;
        public const double MinimumGrade = 0.0;
        public const double MaximumGrade = 10.0;

        public static double CalculateAverage(List<double> grades)
        {
            if (grades == null || grades.Count == 0)
                return 0;

            return grades.Average();
        }

        public static bool IsApproved(double average, double minimumGrade = MinimumApprovedGrade)
        {
            return average >= minimumGrade;
        }

        public static bool IsValidGrade(double grade)
        {
            return grade >= MinimumGrade && grade <= MaximumGrade;
        }

        public static double GetHighestGrade(List<double> grades)
        {
            if (grades == null || grades.Count == 0)
                return 0;

            return grades.Max();
        }

        public static double GetLowestGrade(List<double> grades)
        {
            if (grades == null || grades.Count == 0)
                return 0;

            return grades.Min();
        }

        public static int CountApprovedGrades(List<double> grades, double minimumGrade = MinimumApprovedGrade)
        {
            if (grades == null || grades.Count == 0)
                return 0;

            return grades.Count(g => g >= minimumGrade);
        }

        public static int CountFailedGrades(List<double> grades, double minimumGrade = MinimumApprovedGrade)
        {
            if (grades == null || grades.Count == 0)
                return 0;

            return grades.Count(g => g < minimumGrade);
        }

        public static double CalculateMedian(List<double> grades)
        {
            if (grades == null || grades.Count == 0)
                return 0;

            var sortedGrades = grades.OrderBy(g => g).ToList();
            int count = sortedGrades.Count;

            if (count % 2 == 0)
            {
                return (sortedGrades[count / 2 - 1] + sortedGrades[count / 2]) / 2.0;
            }
            else
            {
                return sortedGrades[count / 2];
            }
        }

        public static string GetLetterGrade(double grade)
        {
            if (grade >= 9.0) return "A";
            if (grade >= 8.0) return "B";
            if (grade >= 7.0) return "C";
            if (grade >= 6.0) return "D";
            return "F";
        }
    }
}