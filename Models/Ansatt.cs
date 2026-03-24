// Namespace organiserer klassen i Models-mappen
namespace UniversitySystem.Models;

// Employee-klassen arver fra Bruker
// Det betyr at den automatisk får:
// Id, Navn, Epost, Brukernavn, Passord
public class Employee : Bruker
{
    // Stilling beskriver rollen til den ansatte
    // Eksempel: Faglærer eller Bibliotekar
    public string Position { get; set; }

    // Avdeling den ansatte jobber i
    public string Department { get; set; }

    // Liste over kurs den ansatte underviser i
    // Brukes hvis Position = "Faglærer"
    public List<Course> UnderviserIKurs { get; set; } = new();

    // Konstruktør
    // Denne kjøres når vi oppretter en ny ansatt
    public Employee(string id, string navn, string epost,
                    string brukernavn, string passord,
                    string position, string department)
        : base(id, navn, epost, brukernavn, passord)
    {
        Position = position;
        Department = department;
    }

    // Override betyr at vi overskriver metoden fra Bruker-klassen
    // Gir mer spesifikk informasjon om ansatte
    public override string GetInfo()
    {
        return $"Ansatt: {Navn} ({Id}) - {Epost}, Stilling: {Position}, Avdeling: {Department}";
    }
}