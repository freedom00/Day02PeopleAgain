namespace Day02PeopleAgain
{
    internal class Person
    {
        private const int DATA_LENGTH = 3;
        private string name;
        private int age;

        public Person()
        {
        }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public Person(string dataLine)
        {
            string[] tempData = dataLine.Split(';');
            if (tempData.Length != DATA_LENGTH)
            {
                throw new InvalidParameterException("Person data line must have 3 fields.");
            }
            if (tempData[0] != "Person")
            {
                throw new InvalidParameterException("Data line does not describe person.");
            }
            Name = tempData[1];
            int tempAge;
            if (!int.TryParse(tempData[2], out tempAge))
            {
                throw new InvalidParameterException("Age in data line must be an integer.");
            }
            Age = tempAge;
        }

        public string Name
        {
            get => name;
            set
            {
                if (value.Length < 1 || value.Length > 50 || value.Contains(";"))
                {
                    throw new InvalidParameterException("Name must be 1-50 characters, no semicolons.");
                }
                name = value;
            }
        }

        public int Age
        {
            get => age;
            set
            {
                if (value < 0 || value > 150)
                {
                    throw new InvalidParameterException("Age must be 1-150.");
                }
                age = value;
            }
        }

        public override string ToString()
        {
            return string.Format("Person {0} is {1}", Name, Age);
        }

        public virtual string ToDataString()
        {
            return string.Format("Person;{0};{1}", Name, Age);
        }
    }
}