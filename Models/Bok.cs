// Definerer namespace (mappe/område) for klassen
namespace UniversitySystem.Models;

// Book-klassen representerer en bok i biblioteket
public class Book
{
    // Unik ID for boken
    public string Id { get; set; }

    // Tittel på boken
    public string Title { get; set; }

    // Forfatter av boken
    public string Author { get; set; }

    // Publiseringsår
    public int Year { get; set; }

    // Totalt antall eksemplarer av boken
    public int TotalCopies { get; set; }

    // Hvor mange eksemplarer som er tilgjengelige akkurat nå
    public int AvailableCopies { get; set; }

    // Konstruktør - brukes når vi oppretter en ny bok
    public Book(string id, string title, string author, int year, int totalCopies)
    {
        // Setter verdier på alle egenskapene
        Id = id;
        Title = title;
        Author = author;
        Year = year;
        TotalCopies = totalCopies;

        // Når boken opprettes, er alle eksemplarer tilgjengelige
        AvailableCopies = totalCopies;
    }

    // Metode som returnerer informasjon om boken som tekst
    public string GetInfo()
    {
        // Returnerer ID, tittel, forfatter, år og hvor mange som er tilgjengelige
        return $"{Id} - {Title} av {Author} ({Year}) | Tilgjengelig: {AvailableCopies}/{TotalCopies}";
    }// Metode som prøver å låne ut en bok
// Returnerer true hvis utlån lykkes
// Returnerer false hvis ingen eksemplarer er tilgjengelige
public bool BorrowCopy()
{
    // Sjekker om det finnes tilgjengelige kopier
    if (AvailableCopies <= 0)
    {
        return false;
    }

    // Trekker fra én kopi
    AvailableCopies--;

    return true;
}


// Metode som leverer tilbake en bok
public void ReturnCopy()
{
    // Øker antall tilgjengelige kopier med 1
    // men ikke mer enn totalt antall
    if (AvailableCopies < TotalCopies)
    {
        AvailableCopies++;
    }
}
}