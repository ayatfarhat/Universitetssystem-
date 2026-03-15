namespace UniversitySystem.Models;

public class Loan
{
    public Book Book { get; set; }
    public User Borrower { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }

    public Loan(Book book, User borrower)
    {
        Book = book;
        Borrower = borrower;
        LoanDate = DateTime.Now;
        ReturnDate = null;
    }

    public bool IsActive()
    {
        return ReturnDate == null;
    }

    public string GetInfo()
    {
        string status = IsActive() ? "Aktivt lån" : $"Returnert: {ReturnDate}";
        return $"{Book.Title} lånt av {Borrower.Name} | {status}";
    }
}