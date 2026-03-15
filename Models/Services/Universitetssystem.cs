using UniversitySystem.Models;

namespace UniversitySystem.Services;
// Service class responsible for handling the logic of the university system
// Includes course management and library management
public class UniversityService
{
    public List<Student> Students { get; set; }
    public List<Employee> Employees { get; set; }
    public List<Course> Courses { get; set; }
    public List<Book> Books { get; set; }
    public List<Loan> Loans { get; set; }

    public UniversityService()
    {
        Students = new List<Student>();
        Employees = new List<Employee>();
        Courses = new List<Course>();
        Books = new List<Book>();
        Loans = new List<Loan>();
    }

    public void AddStudent(Student student)
    {
        Students.Add(student);
    }

    public void AddEmployee(Employee employee)
    {
        Employees.Add(employee);
    }

    public void AddCourse(Course course)
    {
        Courses.Add(course);
    }

    public void AddBook(Book book)
    {
        Books.Add(book);
    }

    public void EnrollStudentInCourse(string studentId, string courseCode)
    {
        Student? student = Students.FirstOrDefault(s => s.StudentID == studentId);
        Course? course = Courses.FirstOrDefault(c => c.Code == courseCode);

        if (student == null)
        {
            Console.WriteLine("Fant ikke student.");
            return;
        }

        if (course == null)
        {
            Console.WriteLine("Fant ikke kurs.");
            return;
        }

        if (!course.HasAvailableSeat())
        {
            Console.WriteLine("Kurset er fullt.");
            return;
        }

        course.Participants.Add(student);
        Console.WriteLine("Student meldt på kurs.");
    }

    public void PrintCoursesAndParticipants()
    {
        foreach (Course course in Courses)
        {
            Console.WriteLine("\n" + course.GetInfo());

            if (course.Participants.Count == 0)
            {
                Console.WriteLine("Ingen deltakere.");
            }
            else
            {
                foreach (Student student in course.Participants)
                {
                    Console.WriteLine("- " + student.GetInfo());
                }
            }
        }
    }

    public void SearchCourse(string searchText)
    {
        var results = Courses.Where(c =>
            c.Code.ToLower().Contains(searchText.ToLower()) ||
            c.Name.ToLower().Contains(searchText.ToLower()));

        foreach (Course course in results)
        {
            Console.WriteLine(course.GetInfo());
        }
    }

    public void SearchBook(string searchText)
    {
        var results = Books.Where(b =>
            b.Title.ToLower().Contains(searchText.ToLower()) ||
            b.Author.ToLower().Contains(searchText.ToLower()));

        foreach (Book book in results)
        {
            Console.WriteLine(book.GetInfo());
        }
    }

    public void BorrowBook(string bookId, string borrowerType, string borrowerId)
    {
        Book? book = Books.FirstOrDefault(b => b.Id == bookId);

        if (book == null)
        {
            Console.WriteLine("Fant ikke bok.");
            return;
        }

        if (book.AvailableCopies <= 0)
        {
            Console.WriteLine("Ingen eksemplarer tilgjengelig.");
            return;
        }

        User? borrower = null;

        if (borrowerType.ToLower() == "student")
        {
            borrower = Students.FirstOrDefault(s => s.StudentID == borrowerId);
        }
        else if (borrowerType.ToLower() == "employee")
        {
            borrower = Employees.FirstOrDefault(e => e.EmployeeID == borrowerId);
        }

        if (borrower == null)
        {
            Console.WriteLine("Fant ikke låner.");
            return;
        }

        Loan loan = new Loan(book, borrower);
        Loans.Add(loan);
        book.AvailableCopies--;

        Console.WriteLine("Bok lånt ut.");
    }

    public void ReturnBook(string bookId, string borrowerName)
    {
        Loan? loan = Loans.FirstOrDefault(l =>
            l.Book.Id == bookId &&
            l.Borrower.Name.ToLower() == borrowerName.ToLower() &&
            l.IsActive());

        if (loan == null)
        {
            Console.WriteLine("Fant ikke aktivt lån.");
            return;
        }

        loan.ReturnDate = DateTime.Now;
        loan.Book.AvailableCopies++;

        Console.WriteLine("Bok returnert.");
    }

    public void CreateCourse()
    {
        Console.Write("Kurskode: ");
        string code = Console.ReadLine()!;

        Console.Write("Kursnavn: ");
        string name = Console.ReadLine()!;

        Console.Write("Studiepoeng: ");
       if (!int.TryParse(Console.ReadLine(), out int credits))
{
    Console.WriteLine("Ugyldig input. Studiepoeng må være et tall.");
    return;
}

        Console.Write("Maks antall plasser: ");
       if (!int.TryParse(Console.ReadLine(), out int maxStudents))
{
    Console.WriteLine("Ugyldig input. Maks antall plasser må være et tall.");
    return;
}

        Course course = new Course(code, name, credits, maxStudents);
        Courses.Add(course);

        Console.WriteLine("Kurs opprettet.");
    }

    public void RegisterBook()
    {
        Console.Write("Bok-ID: ");
        string id = Console.ReadLine()!;

        Console.Write("Tittel: ");
        string title = Console.ReadLine()!;

        Console.Write("Forfatter: ");
        string author = Console.ReadLine()!;

        Console.Write("År: ");
       if (!int.TryParse(Console.ReadLine(), out int year))
{
    Console.WriteLine("Ugyldig input. År må være et tall.");
    return;
}

        Console.Write("Antall eksemplarer: ");
       if (!int.TryParse(Console.ReadLine(), out int copies))
{
    Console.WriteLine("Ugyldig input. Antall eksemplarer må være et tall.");
    return;
}

        Book book = new Book(id, title, author, year, copies);
        Books.Add(book);

        Console.WriteLine("Bok registrert.");
    }public void UnenrollStudentFromCourse(string studentId, string courseCode)
{
    Student? student = Students.FirstOrDefault(s => s.StudentID == studentId);
    Course? course = Courses.FirstOrDefault(c => c.Code == courseCode);

    if (student == null)
    {
        Console.WriteLine("Fant ikke student.");
        return;
    }

    if (course == null)
    {
        Console.WriteLine("Fant ikke kurs.");
        return;
    }

    if (!course.Participants.Contains(student))
    {
        Console.WriteLine("Studenten er ikke meldt på dette kurset.");
        return;
    }

    course.Participants.Remove(student);
    Console.WriteLine("Student meldt av kurs.");
}public void PrintActiveLoans()
{
    Console.WriteLine("\nAktive lån:");

    var activeLoans = Loans.Where(l => l.IsActive()).ToList();

    if (activeLoans.Count == 0)
    {
        Console.WriteLine("Ingen aktive lån.");
        return;
    }

    foreach (Loan loan in activeLoans)
    {
        Console.WriteLine(loan.GetInfo());
    }
}

public void PrintLoanHistory()
{
    Console.WriteLine("\nLånehistorikk:");

    if (Loans.Count == 0)
    {
        Console.WriteLine("Ingen historikk.");
        return;
    }

    foreach (Loan loan in Loans)
    {
        Console.WriteLine(loan.GetInfo());
    }
}
}