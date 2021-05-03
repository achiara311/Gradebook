using System;

namespace GradeBook
{
    public class Statistics
    {
        public double Average
        {
            get
            {
                return Sum / Count;
            }
        }

        public double High;
        public double Low;
        public char Letter
        {
            get
            {
                switch (Average)
                {
                    //var d will contain average grade (result.Average)
                    //and pass it into var d
                    case var d when d >= 90.0:
                        return 'A';

                    case var d when d >= 80.0:
                        return  'B';
                        
                    case var d when d >= 70.0:
                        return  'C';
                        
                    case var d when d >= 60.0:
                        return  'D';
                        
                    default:
                        return  'F';
                    
                }

            }
        }
        public double Sum;
        public int Count;

        public void Add(double number)
        {
            Sum += number;
            Count += 1;
            High = Math.Max(number, High);//finds highest number in the loop
            Low = Math.Min(number, Low);//finds lowest number in the loop
        }

        public Statistics()
        {
            Count = 0;
            Sum = 0.0;
            High = double.MinValue; //should initialize in constructor of Statistics class
            Low = double.MaxValue; //should initialize in constructor of Statistics class
        }
    }
}