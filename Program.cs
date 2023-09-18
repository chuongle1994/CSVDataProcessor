using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP
{
    class Program
    {
        static void Main(string[] args)
        {
            // Read data from a CSV file named "value.csv"
            string[] file = Readfile("value.csv");

            // Extract and process the data to create a list of people
            List<Person> people = getPeople(file);

            // Print the details of each person in the list
            PrintPeople(people);

            // Wait for user input before exiting
            Console.ReadKey();
        }

        // Function to read lines from a file
        static string[] Readfile(string filename)
        {
            string[] lines = System.IO.File.ReadAllLines(filename);
            return lines;
        }

        // Function to convert CSV data into a list of Person objects
        static List<Person> getPeople(string[] file)
        {
            Dictionary<int, List<string>> file_items = new Dictionary<int, List<string>>();
            List<Person> people = new List<Person>();

            // Extract CSV headers and data into a dictionary
            for (int i = 0; i < file.Length; i++)
                file_items.Add(i, GetItems(file[i]));

            // Process each data row to create Person objects
            for (int i = 1; i < file.Length; i++)
            {
                string firstname = "", lastname = "", occupation = "";
                int age = 0;

                // Extract data fields based on CSV headers
                for (int j = 0; j < file_items[0].Count(); j++)
                {
                    switch (file_items[0][j])
                    {
                        case "firstname":
                            firstname = file_items[i][j];
                            break;
                        case "lastname":
                            lastname = file_items[i][j];
                            break;
                        case "occupation":
                            occupation = file_items[i][j];
                            break;
                        case "age":
                            age = int.Parse(file_items[i][j]);
                            break;
                        default:
                            Console.WriteLine($"Header '{file_items[0][j]}' is not valid");
                            break;
                    }
                }

                // Create a Person object and add it to the list
                Person e = new Person(firstname, lastname, occupation, age);
                people.Add(e);
            }

            return people;
        }

        // Function to split a CSV line into individual items
        static List<string> GetItems(string line)
        {
            string current_word = "";
            List<string> items = new List<string>();

            // Parse the line character by character
            foreach (char c in line)
            {
                if (c == ',')
                {
                    if (current_word != "")
                    {
                        items.Add(current_word);
                        current_word = "";
                    }
                }
                else
                {
                    current_word += c.ToString();
                }
            }

            // Add the last item if it exists
            if (current_word != "") items.Add(current_word);

            return items;
        }

        // Function to print details of people
        static void PrintPeople(List<Person> people)
        {
            foreach (Person p in people)
            {
                Console.WriteLine($"{p._FirstName} {p._LastName} is {p._Age.ToString()} years old and works as a {p._Occupation}");
            }
        }
    }

    // Define a Person class to represent individual records
    class Person
    {
        private string firstName = "";
        private string lastName = "";
        private int Age = 0;
        private string Occupation = "";

        public string _FirstName
        {
            get
            {
                return firstName;
            }
        }

        public string _LastName
        {
            get
            {
                return lastName;
            }
        }

        public string _Occupation
        {
            get
            {
                return Occupation;
            }
        }

        public int _Age
        {
            get
            {
                return Age;
            }
        }

        // Constructor to initialize Person objects
        public Person(string firstname, string lastname, string occupation, int age)
        {
            firstName = firstname;
            lastName = lastname;
            Occupation = occupation;
            Age = age;
        }
    }
}
