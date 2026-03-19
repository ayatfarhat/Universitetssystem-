// Definerer namespace (mappe/område) for klassen
namespace UniversitySystem.Models;

// User er en baseklasse (forelderklasse)
// Andre klasser som Student og Employee kan arve fra denne
public class User
{
    // Navn på brukeren
    public string Name { get; set; }

    // E-post til brukeren
    public string Email { get; set; }

    // Konstruktør - brukes når vi oppretter en ny bruker
    public User(string name, string email)
    {
        // Setter verdiene for navn og e-post
        Name = name;
        Email = email;
    }

    // Virtual betyr at metoden kan overskrives (override) i klasser som arver fra User
    public virtual string GetInfo()
    {
        // Returnerer en enkel tekst med informasjon om brukeren
        return $"Bruker {Name} - {Email}";
    }
}