// Definerer namespace (mappe/område) for klassen
namespace UniversitySystem.Models;

// Loan-klassen representerer et boklån i biblioteket
public class Loan
{
    // Hvilken bok som er lånt
    public Book Book { get; set; }

    // Hvem som har lånt boken
    // Bruker brukes fordi både Student og Employee arver fra Bruker
    public Bruker Borrower { get; set; }

    // Dato og tidspunkt når boken ble lånt
    public DateTime LoanDate { get; set; }

    // Dato for når boken ble returnert
    // "?" betyr at denne kan være null hvis boken ikke er levert enda
    public DateTime? ReturnDate { get; set; }

    // Konstruktør - brukes når et nytt lån opprettes
    public Loan(Book book, Bruker borrower)
    {
        // Setter hvilken bok som lånes
        Book = book;

        // Setter hvem som låner boken
        Borrower = borrower;

        // Setter lånedato til nåværende tidspunkt
        LoanDate = DateTime.Now;

        // Boken er ikke returnert når lånet opprettes
        ReturnDate = null;
    }

    // Metode som sjekker om lånet fortsatt er aktivt
    public bool IsActive()
    {
        // Hvis ReturnDate er null betyr det at boken ikke er levert tilbake
        return ReturnDate == null;
    }

    // Metode som returnerer informasjon om lånet som tekst
    public string GetInfo()
    {
        // Hvis lånet fortsatt er aktivt, skriv "Aktivt lån"
        // Hvis det er avsluttet, vis returdat o
        string status = IsActive() ? "Aktivt lån" : $"Returnert: {ReturnDate}";

        // Returnerer informasjon om boken, låneren og status
        return $"{Book.Title} lånt av {Borrower.Navn} | {status}";
    }
}