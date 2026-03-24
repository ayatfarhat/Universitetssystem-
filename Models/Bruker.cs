// Namespace organiserer klassen i prosjektstrukturen
// Andre klasser i Models-mappen kan bruke denne klassen
namespace UniversitySystem.Models;

// Bruker er en baseklasse (forelderklasse)
// Student og Ansatt arver fra denne klassen
public class Bruker
{
    // Unik ID for brukeren (studentnummer eller ansattnummer)
    public string Id { get; set; }

    // Navnet til brukeren
    public string Navn { get; set; }

    // E-postadressen til brukeren
    public string Epost { get; set; }

    // Brukernavn brukes til innlogging
    public string Brukernavn { get; set; }

    // Passord brukes sammen med brukernavn ved innlogging
    public string Passord { get; set; }

    // Konstruktør (designer)
    // Denne kjøres når vi oppretter et nytt Bruker-objekt
    public Bruker(string id, string navn, string epost, string brukernavn, string passord)
    {
        Id = id;
        Navn = navn;
        Epost = epost;
        Brukernavn = brukernavn;
        Passord = passord;
    }

    // Virtual betyr at metoden kan overskrives i klasser som arver fra Bruker
    public virtual string GetInfo()
    {
        return $"Bruker: {Navn} - {Epost}";
    }
}