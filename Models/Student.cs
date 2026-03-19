// Definerer namespace (mappe/område)
namespace UniversitySystem.Models;

// Student-klassen arver fra User
// Det betyr at den får med seg Name og Email automatisk
public class Student : User
{
    // Unik ID for studenten
    public string StudentID { get; set; }

    // Konstruktør - brukes når vi oppretter en ny student
    public Student(string studentID, string name, string email) : base(name, email)
    {
        // Setter studentens ID
        StudentID = studentID;
    }

    // Override betyr at vi overskriver metoden GetInfo fra User
    public override string GetInfo()
    {
        // Returnerer en tekst med informasjon om studenten
        return $"Student {Name} ({StudentID}) - {Email}";
    }
}