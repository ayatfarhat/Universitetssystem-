// Definerer hvilket namespace (mappe/område) klassen tilhører
namespace UniversitySystem.Models;

// Employee-klassen arver fra User-klassen
// Det betyr at den får med seg egenskaper som Name og Email fra User
public class Employee : User
{
    // Egenskap for ansattens ID
    public string EmployeeID { get; set; }

    // Egenskap for stilling (f.eks. foreleser, bibliotekar)
    public string Position { get; set; }

    // Egenskap for hvilken avdeling personen jobber i
    public string Department { get; set; }

    // Konstruktør - brukes når vi oppretter et nytt Employee-objekt
    // Tar inn verdier og sender name og email til baseklassen (User)
    public Employee(string employeeID, string name, string email, string position, string department)
        : base(name, email) // kaller konstruktøren i User
    {
        // Setter verdier på egenskapene
        EmployeeID = employeeID;
        Position = position;
        Department = department;
    }

    // Override betyr at vi overskriver en metode fra baseklassen (User)
    // Denne metoden gir en tekstbeskrivelse av objektet
    public override string GetInfo()
    {
        // Returnerer en streng med info om den ansatte
        return $"Ansatt {Name} ({EmployeeID}) - {Email}, Stilling: {Position}, Avdeling: {Department}";
    }
}