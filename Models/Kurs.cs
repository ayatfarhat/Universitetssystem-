namespace UniversitySystem.Models;

public class Course
{
    public string Code { get; set; }
    public string Name { get; set; }
    public int Credits { get; set; }
    public int MaxStudents { get; set; }
    public List<Student> Participants { get; set; }

    public Course(string code, string name, int credits, int maxStudents)
    {
        Code = code;
        Name = name;
        Credits = credits;
        MaxStudents = maxStudents;
        Participants = new List<Student>();
    }

    public bool HasAvailableSeat()
    {
        return Participants.Count < MaxStudents;
    }

    public string GetInfo()
    {
        return $"{Code} - {Name}, {Credits} studiepoeng, {Participants.Count}/{MaxStudents} studenter";
    }
}