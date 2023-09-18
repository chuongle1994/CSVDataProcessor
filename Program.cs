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

            // Implement additional features
            while (true)
            {
                Console.WriteLine("\nAdditional Features:");
                Console.WriteLine("1. Sort by Age");
                Console.WriteLine("2. Sort by First Name");
                Console.WriteLine("3. Sort by Last Name");
                Console.WriteLine("4. Search by First Name");
                Console.WriteLine("5. Search by Last Name");
                Console.WriteLine("6. Search by Occupation");
                Console.WriteLine("7. Export to CSV");
                Console.WriteLine("8. Exit");

                int choice = GetChoice(1, 8);

                switch (choice)
                {
                    case 1:
                        people = SortByAge(people);
                        PrintPeople(people);
                        break;
                    case 2:
                        people = SortByFirstName(people);
                        PrintPeople(people);
                        break;
                    case 3:
                        people = SortByLastName(people);
                        PrintPeople(people);
                        break;
                    case 4:
                        Console.Write("Enter First Name to search: ");
                        string firstNameToSearch = Console.ReadLine();
                        SearchByFirstName(people, firstNameToSearch);
                        break;
                    case 5:
                        Console.Write("Enter Last Name to search: ");
                        string lastNameToSearch = Console.ReadLine();
                        SearchByLastName(people, lastNameToSearch);
                        break;
                    case 6:
                        Console.Write("Enter Occupation to search: ");
                        string occupationToSearch = Console.ReadLine();
                        SearchByOccupation(people, occupationToSearch);
                        break;
                    case 7:
                        ExportToCSV(people);
                        break;
                    case 8:
                        Console.WriteLine("Exiting the program.");
                        return;
                }
            }
        }

        static string[] Readfile(string filename)
        {
            string[] lines = System.IO.File.ReadAllLines(filename);
            return lines;
        }

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

        static void PrintPeople(List<Person> people)
        {
            Console.WriteLine("\nList of People:");
            foreach (Person p in people)
            {
                Console.WriteLine($"{p._FirstName} {p._LastName} is {p._Age.ToString()} years old and works as a {p._Occupation}");
            }
        }

        static List<Person> SortByAge(List<Person> people)
        {
            return people.OrderBy(p => p._Age).ToList();
        }

        static List<Person> SortByFirstName(List<Person> people)
        {
            return people.OrderBy(p => p._FirstName).ToList();
        }

        static List<Person> SortByLastName(List<Person> people)
        {
            return people.OrderBy(p => p._LastName).ToList();
        }

        static void SearchByFirstName(List<Person> people, string firstName)
        {
            var results = people.Where(p => p._FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase)).ToList();
            if (results.Count == 0)
            {
                Console.WriteLine($"No matching records found for first name: {firstName}");
            }
            else
            {
                Console.WriteLine($"\nSearch Results for First Name: {firstName}");
                PrintPeople(results);
            }
        }

        static void SearchByLastName(List<Person> people, string lastName)
        {
            var results = people.Where(p => p._LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase)).ToList();
            if (results.Count == 0)
            {
                Console.WriteLine($"No matching records found for last name: {lastName}");
            }
            else
            {
                Console.WriteLine($"\nSearch Results for Last Name: {lastName}");
                PrintPeople(results);
            }
        }

        static void SearchByOccupation(List<Person> people, string occupation)
        {
            var results = people.Where(p => p._Occupation.Contains(occupation, StringComparison.OrdinalIgnoreCase)).ToList();
            if (results.Count == 0)
            {
                Console.WriteLine($"No matching records found for occupation: {occupation}");
            }
            else
            {
                Console.WriteLine($"\nSearch Results for Occupation: {occupation}");
                PrintPeople(results);
            }
        }

        static void ExportToCSV(List<Person> people)
        {
            Console.Write("Enter the CSV file name to export: ");
            string fileName = Console.ReadLine();
            fileName += ".csv";

            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(fileName))
            {
                // Write CSV headers
                file.WriteLine("firstname,lastname,occupation,age");

                // Write data for each person
                foreach (Person p in people)
                {
                    file.WriteLine($"{p._FirstName},{p._LastName},{p._Occupation},{p._Age}");
                }
            }

            Console.WriteLine($"Data exported to {fileName}");
        }

        static int GetChoice(int min, int max)
        {
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < min || choice > max)
            {
                Console.WriteLine("Invalid choice. Please enter a valid option.");
                                Console.Write("Enter your choice: ");
            }
            return choice;
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

