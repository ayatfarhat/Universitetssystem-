// Namespace organiserer klassen i Models-mappen
namespace UniversitySystem.Models;

// ExchangeStudent arver fra Student
// Det betyr at den automatisk får:
// Id, Navn, Epost, Brukernavn, Passord
// PåmeldteKurs og Karakterer
public class ExchangeStudent : Student
{
    // Hjemuniversitetet til studenten
    public string HomeUniversity { get; set; }

    // Hvilket land studenten kommer fra
    public string Country { get; set; }

    // Startdato for utvekslingsperioden
    public string PeriodFrom { get; set; }

    // Sluttdato for utvekslingsperioden
    public string PeriodTo { get; set; }

    // Konstruktør
    // Denne kjøres når vi oppretter en ny utvekslingsstudent
    public ExchangeStudent(
        string id,
        string navn,
        string epost,
        string brukernavn,
        string passord,
        string homeUniversity,
        string country,
        string periodFrom,
        string periodTo
    )
        : base(id, navn, epost, brukernavn, passord)
    {
        HomeUniversity = homeUniversity;
        Country = country;
        PeriodFrom = periodFrom;
        PeriodTo = periodTo;
    }

    // Override betyr at vi overskriver metoden fra Student
    // Gir mer spesifikk informasjon om utvekslingsstudenten
    public override string GetInfo()
    {
        return $"Utvekslingsstudent: {Navn} ({Id}) - {Epost}, " +
               $"Hjemuniversitet: {HomeUniversity}, Land: {Country}, " +
               $"Periode: {PeriodFrom} til {PeriodTo}";
    }
}