namespace Day02PeopleAgain
{
    internal class Teacher : Person
    {
        private const int DATA_LENGTH = 5;
        private string subject;
        private int yearsOfExperience;

        public string Subject
        {
            get => subject;
            set
            {
                if (value.Length < 1 || value.Length > 50 || value.Contains(";"))
                {
                    throw new InvalidParameterException("Subject must be 1-50 characters, no semicolons.");
                }
                subject = value;
            }
        }

        public int YearsOfExperience
        {
            get => yearsOfExperience;
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new InvalidParameterException("Years of experience in data line must be 0-100.");
                }
                yearsOfExperience = value;
            }
        }

        public Teacher() : base()
        {
        }

        public Teacher(string name, int age, string subject, int yoe) : base(name, age)
        {
            Subject = subject;
            YearsOfExperience = yoe;
        }

        public Teacher(string dataLine)
        {
            string[] tempData = dataLine.Split(';');
            if (tempData.Length != DATA_LENGTH)
            {
                throw new InvalidParameterException("Teacher data line must have 5 fields.");
            }
            if (tempData[0] != "Teacher")
            {
                throw new InvalidParameterException("Data line does not describe Theacher.");
            }
            Name = tempData[1];
            int tempAge;
            if (!int.TryParse(tempData[2], out tempAge))
            {
                throw new InvalidParameterException("Age in data line must be an integer.");
            }
            Age = tempAge;
            Subject = tempData[3];
            int tempYoe;
            if (!int.TryParse(tempData[4], out tempYoe))
            {
                throw new InvalidParameterException("Years of experience in data line must be integer.");
            }
            YearsOfExperience = tempYoe;
        }

        public override string ToString()
        {
            return string.Format("Teacher {0} is {1} teaching {2} for {3}", Name, Age, Subject, YearsOfExperience);
        }

        public override string ToDataString()
        {
            return string.Format("Teacher;{0};{1};{2};{3}", Name, Age, Subject, YearsOfExperience);
        }
    }
}