using System;

class Person
{
    private string name;
    private string address;
    private double salary;

    public Person(string name, string address, double salary)
    {
        this.name = name;
        this.address = address;
        this.salary = salary;
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public string Address
    {
        get { return address; }
        set { address = value; }
    }

    public double Salary
    {
        get { return salary; }
        set { salary = value; }
    }

    public static Person InputPersonInfo()
    {
        string name, address;
        double salary;

        Console.WriteLine("Input Information of Person");
        Console.Write("Please input name: ");
        name = Console.ReadLine();

        Console.Write("Please input address: ");
        address = Console.ReadLine();

        while (true)
        {
            Console.Write("Please input salary: ");
            if (double.TryParse(Console.ReadLine(), out salary))
            {
                if (salary > 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Salary must be a positive number.");
                }
            }
            else
            {
                Console.WriteLine("You must input a valid number for salary.");
            }
        }

        return new Person(name, address, salary);
    }

    public static void DisplayPersonInfo(Person person)
    {
        Console.WriteLine("Information of Person you have entered:");
        Console.WriteLine($"Name: {person.Name}");
        Console.WriteLine($"Address: {person.Address}");
        Console.WriteLine($"Salary: {person.Salary}");
    }

    public static Person[] SortBySalary(Person[] people)
    {
        try
        {
            int n = people.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (people[j].Salary > people[j + 1].Salary)
                    {
                        Person temp = people[j];
                        people[j] = people[j + 1];
                        people[j + 1] = temp;
                    }
                }
            }
            return people;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return null;
        }
    }

    static void Main()
    {
        int numberOfPeople = 3;
        Person[] people = new Person[numberOfPeople];

        for (int i = 0; i < numberOfPeople; i++)
        {
            people[i] = InputPersonInfo();
        }

        Console.WriteLine("Displaying Person Information:");
        foreach (Person person in people)
        {
            DisplayPersonInfo(person);
        }

        Console.WriteLine("Sorting by Salary:");
        people = SortBySalary(people);

        foreach (Person person in people)
        {
            DisplayPersonInfo(person);
        }
    }
}
