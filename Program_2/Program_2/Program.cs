using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    class Employee
    {
        public string LastName;
        public string FirstName;
        public string MiddleName;
        public string Gender;
        public int Age;
        public decimal Salary;

        public Employee(string line)
        {
            var parts = line.Split(' ');
            LastName = parts[0];
            FirstName = parts[1];
            MiddleName = parts[2];
            Gender = parts[3];
            Age = int.Parse(parts[4]);
            Salary = decimal.Parse(parts[5]);
        }

        public override string ToString()
        {
            return $"{LastName} {FirstName} {MiddleName} {Gender} {Age} {Salary}";
        }
    }

    static void Main()
    {
        Queue<Employee> youngerThan30 = new Queue<Employee>();
        Queue<Employee> others = new Queue<Employee>();

        string address = "D:\\Універ\\2 курс\\2 семестр\\Крос-платформне програмуваня\\Лаби\\Lab_9\\Program_2\\input.txt";

        using (StreamReader reader = new StreamReader(address))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                Employee emp = new Employee(line);
                if (emp.Age < 30)
                    youngerThan30.Enqueue(emp);
                else
                    others.Enqueue(emp);
            }
        }

        Console.WriteLine("Молодшi 30:");
        foreach (var emp in youngerThan30)
            Console.WriteLine(emp);

        Console.WriteLine("\nIншi:");
        foreach (var emp in others)
            Console.WriteLine(emp);
    }
}
