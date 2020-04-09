using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day02PeopleAgain
{
    internal class Student : Person
    {
        private const int DATA_LENGTH = 5;
        private string program;
        private double gpa;

        public string Program
        {
            get => program;
            set
            {
                if (value.Length < 1 || value.Length > 50 || value.Contains(";"))
                {
                    throw new InvalidParameterException("Program must be 1-50, no semicolons.");
                }
                program = value;
            }
        }

        public double GPA
        {
            get => gpa;
            set
            {
                if (value < 0 || value > 4.3)
                {
                    throw new InvalidParameterException("GPA must be 0-4.3.");
                }
                gpa = value;
            }
        }

        public Student() : base()
        {
        }

        public Student(string name, int age, string program, double gpa) : base(name, age)
        {
            Program = program;
            GPA = gpa;
        }

        public Student(string dataLine)
        {
            string[] tempData = dataLine.Split(';');
            if (tempData.Length != DATA_LENGTH)
            {
                throw new InvalidParameterException("Student data line must have 5 fields.");
            }
            if (tempData[0] != "Student")
            {
                throw new InvalidParameterException("Data line does not describe Student.");
            }
            Name = tempData[1];
            int tempAge;
            if (!int.TryParse(tempData[2], out tempAge))
            {
                throw new InvalidParameterException("Age in data line must be an integer.");
            }
            Age = tempAge;
            Program = tempData[3];
            double tempGpa;
            if (!double.TryParse(tempData[4], out tempGpa))
            {
                throw new InvalidParameterException("GPA in data line must be numerical.");
            }
            GPA = tempGpa;
        }

        public override string ToString()
        {
            return string.Format("Student {0} is {1} studying {2} with GPA {3}", Name, Age, Program, GPA);
        }

        public override string ToDataString()
        {
            return string.Format("Student;{0};{1};{2};{3}", Name, Age, Program, GPA);
        }
    }
}