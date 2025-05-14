using System;
using System.Collections;
using System.IO;

class Employee : IComparable<Employee>, ICloneable
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

    public int CompareTo(Employee other)
    {
        return Salary.CompareTo(other.Salary);
    }

    public object Clone()
    {
        return new Employee($"{LastName} {FirstName} {MiddleName} {Gender} {Age} {Salary}");
    }

    public override string ToString()
    {
        return $"{LastName} {FirstName} {MiddleName} {Gender} {Age} {Salary}";
    }
}

class SalaryComparer : IComparer
{
    public int Compare(object x, object y)
    {
        return ((Employee)x).Salary.CompareTo(((Employee)y).Salary);
    }
}

class EmployeeList : IEnumerable
{
    private ArrayList list = new ArrayList();

    public void Add(Employee emp)
    {
        list.Add(emp);
    }

    public void Sort()
    {
        list.Sort(new SalaryComparer());
    }

    public IEnumerator GetEnumerator()
    {
        return list.GetEnumerator();
    }

    public EmployeeList CloneYoungerThan30()
    {
        var copy = new EmployeeList();
        foreach (Employee e in list)
        {
            if (e.Age < 30)
                copy.Add((Employee)e.Clone());
        }
        return copy;
    }

    public EmployeeList CloneOthers()
    {
        var copy = new EmployeeList();
        foreach (Employee e in list)
        {
            if (e.Age >= 30)
                copy.Add((Employee)e.Clone());
        }
        return copy;
    }
}

class Program
{
    static void Main()
    {
        string address = "D:\\Універ\\2 курс\\2 семестр\\Крос-платформне програмуваня\\Лаби\\Lab_9\\Program_2\\input.txt";
        EmployeeList all = new EmployeeList();

        foreach (var line in File.ReadLines(address))
            all.Add(new Employee(line));

        EmployeeList younger = all.CloneYoungerThan30();
        EmployeeList others = all.CloneOthers();

        Console.WriteLine("Молодшi 30:");
        foreach (Employee e in younger)
            Console.WriteLine(e);

        Console.WriteLine("\nIншi:");
        foreach (Employee e in others)
            Console.WriteLine(e);
    }
}
