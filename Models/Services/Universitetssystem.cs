using UniversitySystem.Models;

namespace UniversitySystem.Services;

// Denne klassen inneholder all logikken i systemet
public class UniversityService
{
    // Lister som lagrer data
    public List<Student> Students { get; set; } = new();
    public List<Employee> Employees { get; set; } = new();
    public List<Course> Courses { get; set; } = new();
    public List<Book> Books { get; set; } = new();
    public List<Loan> Loans { get; set; } = new();

    // Legger til student
    public void AddStudent(Student s)
    {
        Students.Add(s);
    }

    // Legger til ansatt
    public void AddEmployee(Employee e)
    {
        Employees.Add(e);
    }

    // Oppretter nytt kurs via input
    public void CreateCourse()
    {
        Console.Write("Kode: ");
        string code = Console.ReadLine()!;

        Console.Write("Navn: ");
        string name = Console.ReadLine()!;

        Console.Write("Studiepoeng: ");
        int credits = int.Parse(Console.ReadLine()!);

        Console.Write("Max plasser: ");
        int max = int.Parse(Console.ReadLine()!);

        Courses.Add(new Course(code, name, credits, max));

        Console.WriteLine("Kurs opprettet.");
    }

    // Melder student på kurs
    public void EnrollStudentInCourse(string studentId, string courseCode)
    {
        // Finn student og kurs i listene
        var student = Students.FirstOrDefault(s => s.StudentID == studentId);
        var course = Courses.FirstOrDefault(c => c.Code == courseCode);

        if (student == null || course == null)
        {
            Console.WriteLine("Feil student/kurs.");
            return;
        }

        // Sjekk om det er plass
        if (!course.HasAvailableSeat())
        {
            Console.WriteLine("Kurset er fullt.");
            return;
        }

        // Legg student til i kurset
        course.Participants.Add(student);

        Console.WriteLine("Student meldt på.");
    }

    // Melder student av kurs
    public void UnenrollStudentFromCourse(string studentId, string courseCode)
    {
        var student = Students.FirstOrDefault(s => s.StudentID == studentId);
        var course = Courses.FirstOrDefault(c => c.Code == courseCode);

        if (student == null || course == null) return;

        course.Participants.Remove(student);

        Console.WriteLine("Student meldt av.");
    }

    // Skriver ut kurs og studenter
    public void PrintCoursesAndParticipants()
    {
        foreach (var c in Courses)
        {
            Console.WriteLine("\n" + c.GetInfo());

            foreach (var s in c.Participants)
            {
                Console.WriteLine("- " + s.GetInfo());
            }
        }
    }

    // Søker etter kurs
    public void SearchCourse(string text)
    {
        var results = Courses.Where(c =>
            c.Code.Contains(text, StringComparison.OrdinalIgnoreCase) ||
            c.Name.Contains(text, StringComparison.OrdinalIgnoreCase));

        foreach (var c in results)
        {
            Console.WriteLine(c.GetInfo());
        }
    }

    // Registrerer bok
    public void RegisterBook()
    {
        Console.Write("ID: ");
        string id = Console.ReadLine()!;

        Console.Write("Tittel: ");
        string title = Console.ReadLine()!;

        Console.Write("Forfatter: ");
        string author = Console.ReadLine()!;

        Console.Write("År: ");
        int year = int.Parse(Console.ReadLine()!);

        Console.Write("Antall: ");
        int copies = int.Parse(Console.ReadLine()!);

        Books.Add(new Book(id, title, author, year, copies));

        Console.WriteLine("Bok lagt til.");
    }

    // Søker etter bok
    public void SearchBook(string text)
    {
        var results = Books.Where(b =>
            b.Title.Contains(text, StringComparison.OrdinalIgnoreCase) ||
            b.Author.Contains(text, StringComparison.OrdinalIgnoreCase));

        foreach (var b in results)
        {
            Console.WriteLine(b.GetInfo());
        }
    }

    // Låner bok
    public void BorrowBook(string bookId, string type, string id)
    {
        var book = Books.FirstOrDefault(b => b.Id == bookId);

        if (book == null || book.AvailableCopies == 0)
        {
            Console.WriteLine("Ikke tilgjengelig.");
            return;
        }

        // Finn riktig bruker (student eller ansatt)
        User? borrower = type == "student"
            ? Students.FirstOrDefault(s => s.StudentID == id)
            : Employees.FirstOrDefault(e => e.EmployeeID == id);

        if (borrower == null) return;

        // Opprett lån
        Loans.Add(new Loan(book, borrower));

        // Reduser antall tilgjengelige bøker
        book.AvailableCopies--;

        Console.WriteLine("Bok lånt.");
    }

    // Returnerer bok
    public void ReturnBook(string bookId, string name)
    {
        var loan = Loans.FirstOrDefault(l =>
            l.Book.Id == bookId &&
            l.Borrower.Name.Equals(name, StringComparison.OrdinalIgnoreCase) &&
            l.IsActive());

        if (loan == null) return;

        loan.ReturnDate = DateTime.Now;

        // Øker tilgjengelige bøker igjen
        loan.Book.AvailableCopies++;

        Console.WriteLine("Bok levert.");
    }

    // Viser aktive lån
    public void PrintActiveLoans()
    {
        foreach (var l in Loans.Where(l => l.IsActive()))
        {
            Console.WriteLine(l.GetInfo());
        }
    }

    // Viser historikk
    public void PrintLoanHistory()
    {
        foreach (var l in Loans)
        {
            Console.WriteLine(l.GetInfo());
        }
    }
}