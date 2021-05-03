using System;
using System.Collections.Generic;
using System.IO;

namespace GradeBook
{
    public delegate void GradeAddedDelegate(object sender, EventArgs args);

    public class NamedObject
    {
        public NamedObject(string name)
        {
            this.Name = name;
        }

        public string Name {get;set;}
    }

    public interface IBook 
    {
        void AddGrade(double grade);
        Statistics GetStatistics();
        string Name{get;}
        event GradeAddedDelegate GradeAdded;
    }

    public abstract class Book : NamedObject , IBook
    {
        public Book(string name) : base(name)
        {
        }

        public abstract event GradeAddedDelegate GradeAdded;
        public abstract void AddGrade(double grade);
        public abstract Statistics GetStatistics();
       
    }

    public class DiskBook : Book
    {
        //when in doubt, add constructor first
        //need to add abstract member
        public DiskBook(string name) : base(name)
        {
        }

        public override event GradeAddedDelegate GradeAdded;

        public override void AddGrade(double grade)
        {
            //will open a file with the same name as book 
            //and write a new line into file that contains
            //the grade value
           using (var writer = File.AppendText($"{Name}.txt"))//allows to add as
           //many grades as you want
            {
                //using statement-- most common pattern with files or sockets
                //using statement --telling C# compiler to clean things up when I'm finished 
                //with this object and finished with object when reaching this curly brace.
                writer.WriteLine(grade);
                if(GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }
        }

        public override Statistics GetStatistics()
        {
            var newResult = new Statistics();

            using(var reader = File.OpenText($"{Name}.txt"))
            {
                string line = reader.ReadLine();
                //reads single line from the file and then moves forward
                //the next time ReadLine is called
                while(line != null)
                {
                    var number = double.Parse(line);
                    newResult.Add(number);
                    line = reader.ReadLine();
                }
            }

            return newResult;
        }
      
    }

    public class InMemoryBook : Book
    {
       
        public InMemoryBook(string name) : base(name)
        {
            //explicitly initializes grades list
            //CANNOT have a return type
            grades = new List<double>();//always initialize list in a constructor
            Name = name;
            //on this object, I want to set the private field called 
            //NAME (below) to this incoming parameter called name
        }
        public void AddGrade(char letter)
        {
            switch(letter)
            {
                case 'A':
                AddGrade(90);
                break;

                case 'B':
                AddGrade(80);
                break;

                case 'C':
                AddGrade(70);
                break;

                default:
                AddGrade(0);
                break;
            }
        }
        public override event GradeAddedDelegate GradeAdded;
        public override void AddGrade(double grade)
        {
            if(grade <=100 && grade >= 0)
            {
                this.grades.Add(grade);
                if(GradeAdded != null)
                {
                    GradeAdded(this,new EventArgs());
                }
            }
            else
            {
                  throw new ArgumentException($"Invalid {nameof(grade)}");
            }
          
        } 

          public override Statistics GetStatistics()
        {
            var result = new Statistics();
           
           
            // var highGrade = double.MinValue;//starting at lowest possible value
            // var lowestGrade = double.MaxValue; //starting at highest possible value

           // var index = 0; //for tracking which index we are at, while looping through
            //grades list
            for(var i=0; i<grades.Count;i+=1)
           {
               result.Add(grades[i]);
                //very similar to the "snowball" you know in JS
                // index+=1; //for tracking how many times we've looped through
           };
            
            return result;
        }

     private List<double> grades; 
     //public string Name {get; set;}
     readonly string category;

    }

  
}