// Definerer namespace (mappe/område) for klassen
namespace UniversitySystem.Models;

// Loan-klassen representerer et boklån i biblioteket
public class Loan
{
    // Hvilken bok som er lånt
    public Book Book { get; set; }

    // Hvem som har lånt boken (kan være Student eller Employee)
    // User brukes fordi både Student og Employee arver fra User
    public User Borrower { get; set; }

    // Dato og tidspunkt når boken ble lånt
    public DateTime LoanDate { get; set; }

    // Dato for når boken ble returnert
    // "?" betyr at denne kan være null (ikke returnert enda)
    public DateTime? ReturnDate { get; set; }

    // Konstruktør - brukes når et nytt lån opprettes
    public Loan(Book book, User borrower)
    {
        // Setter hvilken bok som lånes
        Book = book;

        // Setter hvem som låner boken
        Borrower = borrower;

        // Setter lånedato til nåværende tidspunkt
        LoanDate = DateTime.Now;

        // Siden boken ikke er returnert enda, settes ReturnDate til null
        ReturnDate = null;
    }

    // Metode som sjekker om lånet fortsatt er aktivt
    public bool IsActive()
    {
        // Hvis ReturnDate er null betyr det at boken ikke er levert
        return ReturnDate == null;
    }

    // Metode som returnerer informasjon om lånet som tekst
    public string GetInfo()
    {
        // Hvis lånet er aktivt → skriv "Aktivt lån"
        // Hvis ikke → vis når boken ble returnert
        string status = IsActive() ? "Aktivt lån" : $"Returnert: {ReturnDate}";

        // Returnerer en tekst med info om bok, låner og status
        return $"{Book.Title} lånt av {Borrower.Name} | {status}";
    }
}