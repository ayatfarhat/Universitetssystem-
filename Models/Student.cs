// Namespace organiserer klassen i Models-mappen
namespace UniversitySystem.Models;

// Student-klassen arver fra Bruker
// Det betyr at den automatisk får:
// Id, Navn, Epost, Brukernavn, Passord
public class Student : Bruker
{
    // Liste over kurs studenten er meldt på
    // Brukes når studenten melder seg på eller av kurs
    public List<Course> PåmeldteKurs { get; set; } = new();

    // Dictionary brukes til å lagre karakterer
    // Key = kurskode
    // Value = karakter
    public Dictionary<string, string> Karakterer { get; set; } = new();

    // Konstruktør
    // Denne kjøres når vi oppretter en ny student
    public Student(string id, string navn, string epost, string brukernavn, string passord)
        : base(id, navn, epost, brukernavn, passord)
    {
    }

    // Override betyr at vi overskriver metoden fra Bruker-klassen
    // Vi lager en mer spesifikk versjon for Student
    public override string GetInfo()
    {
        return $"Student: {Navn} ({Id}) - {Epost}";
    }
}