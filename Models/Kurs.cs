// Definerer namespace (mappe/område)
namespace UniversitySystem.Models;

// Course-klassen representerer et kurs ved universitetet
public class Course
{
    // Kurskode (f.eks. IS-105)
    public string Code { get; set; }

    // Navn på kurset
    public string Name { get; set; }

    // Antall studiepoeng kurset gir
    public int Credits { get; set; }

    // Maks antall studenter som kan melde seg på kurset
    public int MaxStudents { get; set; }

    // Pensum for kurset
    // Dette brukes når faglærer registrerer pensum
    public string Curriculum { get; set; }

    // Ansatt som underviser kurset
    // Denne brukes for å knytte et kurs til en faglærer
    public Employee? Teacher { get; set; }

    // Liste over studenter som er meldt på kurset
    public List<Student> Participants { get; set; }

    // Konstruktør - brukes når vi oppretter et nytt kurs
    public Course(string code, string name, int credits, int maxStudents)
    {
        // Setter verdier for kurset
        Code = code;
        Name = name;
        Credits = credits;
        MaxStudents = maxStudents;

        // Setter tom tekst som standard pensum
        Curriculum = "";

        // Lager en tom liste som skal inneholde studentene
        Participants = new List<Student>();
    }

    // Metode som sjekker om det er ledige plasser på kurset
    public bool HasAvailableSeat()
    {
        // Hvis antall studenter er mindre enn maks → ledig plass
        return Participants.Count < MaxStudents;
    }

    // Metode som returnerer informasjon om kurset som tekst
    public string GetInfo()
    {
        // Lager tekst for lærer hvis lærer finnes
        string teacherInfo = Teacher != null ? Teacher.Navn : "Ingen faglærer registrert";

        // Viser kurskode, navn, studiepoeng, lærer og antall studenter
        return $"{Code} - {Name}, {Credits} studiepoeng, " +
               $"Faglærer: {teacherInfo}, " +
               $"{Participants.Count}/{MaxStudents} studenter";
    }
}