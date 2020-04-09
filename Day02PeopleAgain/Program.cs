using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day02PeopleAgain
{
    internal class Program
    {
        private const string FROM_PATH = @"..\..\people.txt";
        private const string TO_PATH = @"..\..\byname.txt";
        private const string ERROR_PATH = @"..\..\errors.txt";
        private static List<Person> peopleList = new List<Person>();

        public delegate void LogFailedSetterDelegate(string reason);

        public static LogFailedSetterDelegate LogFailSet;

        private static void ReadDataFromFile()
        {
            try
            {
                string[] lines = File.ReadAllLines(FROM_PATH);
                foreach (string line in lines)
                {
                    try
                    {
                        string[] tempData = line.Split(';');
                        switch (tempData[0])
                        {
                            case "Person":
                                peopleList.Add(new Person(line));
                                break;

                            case "Teacher":
                                peopleList.Add(new Teacher(line));
                                break;

                            case "Student":
                                peopleList.Add(new Student(line));
                                break;

                            default:
                                LogFailSet(string.Format("Error parsing line '{0}': Data line must be Person, Teacher or Student. [skipped]\n", line));
                                break;
                        }
                    }
                    catch (InvalidParameterException ex)
                    {
                        LogFailSet(string.Format("Error parsing line '{0}': {1} [skipped]\n", line, ex.Message));
                    }
                }
            }
            catch (IOException ex)
            {
                LogFailSet(string.Format("Error reading file: {0}\n", ex.Message));
            }
            catch (SystemException ex)
            {
                LogFailSet(string.Format("{0}\n", ex.Message));
            }
        }

        private static void WriteDataToFile()
        {
            try
            {
                var orderedByName = from p in peopleList orderby p.ToDataString() select p;
                File.WriteAllText(TO_PATH, string.Empty);
                foreach (Person person in orderedByName)
                {
                    File.AppendAllText(TO_PATH, person.ToDataString() + "\n");
                }
            }
            catch (IOException ex)
            {
                LogFailSet(string.Format("{0}\n", ex.Message));
            }
        }

        private static void DisplayAllData()
        {
            Console.WriteLine("All Data:");
            foreach (Person p in peopleList)
            {
                Console.WriteLine(p.ToString());
            }
            Console.WriteLine();
        }

        private static void DispalyAllStudents()
        {
            Console.WriteLine("All Students:");
            //foreach (Person p in peopleList)
            //{
            //    if (p is Student)
            //    {
            //        Console.WriteLine(p.ToString());
            //    }
            //}

            var onlyStudents = from p in peopleList where p is Student select p;
            foreach (Person p in onlyStudents)
            {
                Console.WriteLine(p.ToString());
            }

            Console.WriteLine();
        }

        private static void DisplayAllTeachers()
        {
            Console.WriteLine("All Teachers:");
            //foreach (Person p in peopleList)
            //{
            //    if (p is Teacher)
            //    {
            //        Console.WriteLine(p.ToString());
            //    }
            //}
            var onlyTeachers = from p in peopleList where p is Teacher select p;
            foreach (Person p in onlyTeachers)
            {
                Console.WriteLine(p.ToString());
            }
            Console.WriteLine();
        }

        private static void DisplayAllPerson()
        {
            Console.WriteLine("All Person");
            foreach (Person p in peopleList)
            {
                if (p.GetType() == typeof(Person))
                {
                    Console.WriteLine(p.ToString());
                }
            }
            Console.WriteLine();
        }

        private static void DispalyGpaAverage()
        {
            Console.WriteLine("Average GPA");
            double averageOfGpa = (from p in peopleList where p is Student select ((Student)p).GPA).Average();
            Console.WriteLine(averageOfGpa);
        }

        private static void LogToScreen(string reason)
        {
            Console.WriteLine(reason);
        }

        private static void logIntoFile(string reason)
        {
            try
            {
                File.AppendAllText(ERROR_PATH, reason);
            }
            catch (IOException ex)
            {
                LogFailSet(ex.Message);
            }
        }

        private static void DoNotLog(string reason)
        {
            //do nothing
        }

        private static int ShowMenu()
        {
            int choice;
            bool isContinue = default;
            do
            {
                Console.WriteLine("Where would you like to log setters errors?\n1 - screen only\n2 - screen and file\n3 - do not log");
                Console.Write("Your choice: ");
                if (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 3)
                {
                    LogFailSet("Invalid choice try again.\n");
                    //isContinue = true;
                }
            } while (isContinue);
            Console.WriteLine();
            return choice;
        }

        private static void ChoiceMenu()
        {
            int choice = ShowMenu();
            switch (choice)
            {
                case 1:
                    LogFailSet = LogToScreen;
                    break;

                case 2:
                    LogFailSet = LogToScreen;
                    LogFailSet += logIntoFile;
                    break;

                case 3:
                    LogFailSet = DoNotLog;
                    break;

                default:
                    LogFailSet = LogToScreen;
                    break;
            }
        }

        private static void Main(string[] args)
        {
            LogFailSet = LogToScreen;
            ChoiceMenu();

            try
            {
                ReadDataFromFile();
                DisplayAllData();
                DisplayAllTeachers();
                DispalyAllStudents();
                DisplayAllPerson();
                DispalyGpaAverage();
                WriteDataToFile();
            }
            catch (InvalidParameterException ex)
            {
                LogFailSet(string.Format("{0}\n", ex.Message));
            }

            Console.ReadKey();
        }
    }
}