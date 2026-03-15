namespace UniversitySystem.Models;

public class ExchangeStudent : Student
{
    public string HomeUniversity { get; set; }
    public string Country { get; set; }
    public string PeriodFrom { get; set; }
    public string PeriodTo { get; set; }

    public ExchangeStudent(
        string studentID,
        string name,
        string email,
        string homeUniversity,
        string country,
        string periodFrom,
        string periodTo
    ) : base(studentID, name, email)
    {
        HomeUniversity = homeUniversity;
        Country = country;
        PeriodFrom = periodFrom;
        PeriodTo = periodTo;
    }

    public override string GetInfo()
    {
        return $"Utvekslingsstudent {Name} ({StudentID}) - {Email}, Hjemuniversitet: {HomeUniversity}, Land: {Country}, Periode: {PeriodFrom} til {PeriodTo}";
    }
}