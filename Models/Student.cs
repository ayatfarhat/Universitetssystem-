namespace UniversitySystem.Models;

public class Student : User
{
    public string StudentID { get; set; }

    public Student(string studentID, string name, string email) : base(name, email)
    {
        StudentID = studentID;
    }

    public override string GetInfo()
    {
        return $"Student {Name} ({StudentID}) - {Email}";
    }
}