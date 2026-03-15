namespace UniversitySystem.Models;

public class Employee : User
{
    public string EmployeeID { get; set; }
    public string Position { get; set; }
    public string Department { get; set; }

    public Employee(string employeeID, string name, string email, string position, string department)
        : base(name, email)
    {
        EmployeeID = employeeID;
        Position = position;
        Department = department;
    }

    public override string GetInfo()
    {
        return $"Ansatt {Name} ({EmployeeID}) - {Email}, Stilling: {Position}, Avdeling: {Department}";
    }
}