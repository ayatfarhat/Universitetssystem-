// Definerer namespace (mappe/område)
namespace UniversitySystem.Models;

// ExchangeStudent arver fra Student
// Det betyr at den får med seg alt fra Student (og indirekte fra User)
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

    // Konstruktør - brukes når vi oppretter en ny utvekslingsstudent
    public ExchangeStudent(
        string studentID,
        string name,
        string email,
        string homeUniversity,
        string country,
        string periodFrom,
        string periodTo
    ) : base(studentID, name, email) // kaller konstruktøren til Student
    {
        // Setter verdier for egne egenskaper
        HomeUniversity = homeUniversity;
        Country = country;
        PeriodFrom = periodFrom;
        PeriodTo = periodTo;
    }

    // Override betyr at vi overskriver GetInfo-metoden fra Student/User
    public override string GetInfo()
    {
        // Returnerer detaljert informasjon om utvekslingsstudenten
        return $"Utvekslingsstudent {Name} ({StudentID}) - {Email}, Hjemuniversitet: {HomeUniversity}, Land: {Country}, Periode: {PeriodFrom} til {PeriodTo}";
    }
}