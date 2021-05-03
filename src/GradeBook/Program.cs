using System;
using System.Collections.Generic;

namespace GradeBook
{

    class Program
    {
        static void Main(string[] args)
        {
            IBook book = new DiskBook("Chi's GradeBook");
            book.GradeAdded += OnGradeAdded;

            EnterGrades(book);

            var stats = book.GetStatistics();

            Console.WriteLine($"Hey, the lowest grade is {stats.Low}");
            Console.WriteLine($"The highest grade is {stats.High}");
            Console.WriteLine($"Average grade is {stats.Average:N1}");
            Console.WriteLine($"The average grade is  {stats.Letter}");

        }

        private static void EnterGrades(IBook book)
        {
            while (true)
            {
                Console.WriteLine("Enter a grade or 'q' to quit");
                var input = Console.ReadLine();

                if (input == "q")
                {
                    break;
                }

                try
                {
                    var grade = double.Parse(input);
                    book.AddGrade(grade);
                }
                catch (ArgumentException ex) //catches any type of exception
                {
                    Console.WriteLine(ex.Message);

                }
                catch (FormatException ex) //catches any type of exception
                {
                    Console.WriteLine(ex.Message);

                }
                finally
                {
                    Console.WriteLine("**");
                }

            }
        }

        static void OnGradeAdded(object sender, EventArgs e)
        {
            Console.WriteLine("A grade was added.");
        }
    }

  
}
