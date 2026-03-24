using UniversitySystem.Models;

namespace UniversitySystem.Services;

// Denne klassen inneholder hovedlogikken i systemet
public class UniversityService
{
    // Lister som lagrer data i programmet
    public List<Student> Students { get; set; } = new();
    public List<Employee> Employees { get; set; } = new();
    public List<Course> Courses { get; set; } = new();
    public List<Book> Books { get; set; } = new();
    public List<Loan> Loans { get; set; } = new();

    // Liste med alle brukere samlet
    // Denne brukes til innlogging
    public List<Bruker> Brukere { get; set; } = new();

    // Legger til student i både studentlisten og brukerlisten
    public void AddStudent(Student s)
    {
        Students.Add(s);
        Brukere.Add(s);
    }

    // Legger til ansatt i både ansattlisten og brukerlisten
    public void AddEmployee(Employee e)
    {
        Employees.Add(e);
        Brukere.Add(e);
    }

    // Logger inn en bruker med brukernavn og passord
    public Bruker? Login(string brukernavn, string passord)
    {
        return Brukere.FirstOrDefault(b =>
            b.Brukernavn == brukernavn && b.Passord == passord);
    }

    // Registrerer en ny bruker via konsollen
    public void RegisterUser()
    {
        Console.WriteLine("\nVelg type bruker:");
        Console.WriteLine("[1] Student");
        Console.WriteLine("[2] Ansatt");
        Console.Write("Valg: ");
        string choice = Console.ReadLine()!;

        Console.Write("ID: ");
        string id = Console.ReadLine()!;

        Console.Write("Navn: ");
        string navn = Console.ReadLine()!;

        Console.Write("E-post: ");
        string epost = Console.ReadLine()!;

        Console.Write("Brukernavn: ");
        string brukernavn = Console.ReadLine()!;

        // Sjekker at brukernavn ikke finnes fra før
        if (Brukere.Any(b => b.Brukernavn.Equals(brukernavn, StringComparison.OrdinalIgnoreCase)))
        {
            Console.WriteLine("Brukernavn finnes allerede.");
            return;
        }

        Console.Write("Passord: ");
        string passord = Console.ReadLine()!;

        if (choice == "1")
        {
            Student student = new Student(id, navn, epost, brukernavn, passord);
            AddStudent(student);
            Console.WriteLine("Student registrert.");
        }
        else if (choice == "2")
        {
            Console.Write("Stilling: ");
            string position = Console.ReadLine()!;

            Console.Write("Avdeling: ");
            string department = Console.ReadLine()!;

            Employee employee = new Employee(id, navn, epost, brukernavn, passord, position, department);
            AddEmployee(employee);
            Console.WriteLine("Ansatt registrert.");
        }
        else
        {
            Console.WriteLine("Ugyldig valg.");
        }
    }

    // Oppretter nytt kurs via input
    // Bare faglærer skal bruke denne metoden
    public void CreateCourse(Employee teacher)
    {
        Console.Write("Kode: ");
        string code = Console.ReadLine()!;

        Console.Write("Navn: ");
        string name = Console.ReadLine()!;

        Console.Write("Studiepoeng: ");
        if (!int.TryParse(Console.ReadLine(), out int credits))
        {
            Console.WriteLine("Ugyldig studiepoeng.");
            return;
        }

        Console.Write("Max plasser: ");
        if (!int.TryParse(Console.ReadLine(), out int max))
        {
            Console.WriteLine("Ugyldig antall plasser.");
            return;
        }

        // Hindrer samme kurskode eller kursnavn flere ganger
        if (Courses.Any(c => c.Code.Equals(code, StringComparison.OrdinalIgnoreCase) ||
                             c.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
        {
            Console.WriteLine("Kurs med samme kode eller navn finnes allerede.");
            return;
        }

        Course course = new Course(code, name, credits, max);
        course.Teacher = teacher;

        Courses.Add(course);
        teacher.UnderviserIKurs.Add(course);

        Console.WriteLine("Kurs opprettet.");
    }

    // Testvennlig metode for å opprette kurs uten Console.ReadLine
    public void CreateCourseDirect(string code, string name, int credits, int max, Employee teacher)
    {
        if (Courses.Any(c => c.Code.Equals(code, StringComparison.OrdinalIgnoreCase) ||
                             c.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
        {
            return;
        }

        Course course = new Course(code, name, credits, max);
        course.Teacher = teacher;

        Courses.Add(course);
        teacher.UnderviserIKurs.Add(course);
    }

    // Melder student på kurs ved å sende inn studentobjekt og kurskode
    public void EnrollStudentInCourse(Student student, string courseCode)
    {
        var course = Courses.FirstOrDefault(c => c.Code.Equals(courseCode, StringComparison.OrdinalIgnoreCase));

        if (course == null)
        {
            Console.WriteLine("Kurs ble ikke funnet.");
            return;
        }

        // Hindrer dobbel påmelding
        if (student.PåmeldteKurs.Any(c => c.Code.Equals(courseCode, StringComparison.OrdinalIgnoreCase)))
        {
            Console.WriteLine("Studenten er allerede meldt på dette kurset.");
            return;
        }

        // Sjekker om kurset er fullt
        if (!course.HasAvailableSeat())
        {
            Console.WriteLine("Kurset er fullt.");
            return;
        }

        student.PåmeldteKurs.Add(course);
        course.Participants.Add(student);

        Console.WriteLine("Student meldt på.");
    }

    // Melder student av kurs
    public void UnenrollStudentFromCourse(Student student, string courseCode)
    {
        var course = student.PåmeldteKurs.FirstOrDefault(c => c.Code.Equals(courseCode, StringComparison.OrdinalIgnoreCase));

        if (course == null)
        {
            Console.WriteLine("Studenten er ikke meldt på dette kurset.");
            return;
        }

        student.PåmeldteKurs.Remove(course);
        course.Participants.Remove(student);

        Console.WriteLine("Student meldt av.");
    }

    // Viser alle kurs og alle deltakere
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

    // Søker etter kurs basert på kode eller navn
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

    // Viser kursene en student er meldt på
    public void ShowStudentCourses(Student student)
    {
        if (!student.PåmeldteKurs.Any())
        {
            Console.WriteLine("Studenten er ikke meldt på noen kurs.");
            return;
        }

        foreach (var c in student.PåmeldteKurs)
        {
            Console.WriteLine(c.GetInfo());
        }
    }

    // Viser karakterene til en student
    public void ShowStudentGrades(Student student)
    {
        if (!student.Karakterer.Any())
        {
            Console.WriteLine("Ingen karakterer registrert.");
            return;
        }

        foreach (var grade in student.Karakterer)
        {
            Console.WriteLine($"{grade.Key}: {grade.Value}");
        }
    }

    // Faglærer setter karakter til student i eget kurs
    public void SetGrade(Employee teacher, string courseCode, string studentId, string grade)
    {
        var course = Courses.FirstOrDefault(c =>
            c.Code.Equals(courseCode, StringComparison.OrdinalIgnoreCase) &&
            c.Teacher == teacher);

        if (course == null)
        {
            Console.WriteLine("Kurs ikke funnet eller du underviser ikke dette kurset.");
            return;
        }

        var student = course.Participants.FirstOrDefault(s => s.Id == studentId);

        if (student == null)
        {
            Console.WriteLine("Studenten er ikke meldt på kurset.");
            return;
        }

        student.Karakterer[courseCode] = grade;
        Console.WriteLine("Karakter satt.");
    }

    // Faglærer registrerer pensum på eget kurs
    public void AddCurriculum(Employee teacher, string courseCode, string curriculum)
    {
        var course = Courses.FirstOrDefault(c =>
            c.Code.Equals(courseCode, StringComparison.OrdinalIgnoreCase) &&
            c.Teacher == teacher);

        if (course == null)
        {
            Console.WriteLine("Kurs ikke funnet eller du underviser ikke dette kurset.");
            return;
        }

        course.Curriculum = curriculum;
        Console.WriteLine("Pensum registrert.");
    }

    // Registrerer bok via input
    public void RegisterBook()
    {
        Console.Write("ID: ");
        string id = Console.ReadLine()!;

        Console.Write("Tittel: ");
        string title = Console.ReadLine()!;

        Console.Write("Forfatter: ");
        string author = Console.ReadLine()!;

        Console.Write("År: ");
        if (!int.TryParse(Console.ReadLine(), out int year))
        {
            Console.WriteLine("Ugyldig årstall.");
            return;
        }

        Console.Write("Antall: ");
        if (!int.TryParse(Console.ReadLine(), out int copies))
        {
            Console.WriteLine("Ugyldig antall.");
            return;
        }

        Books.Add(new Book(id, title, author, year, copies));
        Console.WriteLine("Bok lagt til.");
    }

    // Søker etter bok basert på tittel eller forfatter
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

    // Låner bok til en bruker (student eller ansatt)
    public void BorrowBook(string bookId, Bruker borrower)
    {
        var book = Books.FirstOrDefault(b => b.Id == bookId);

        if (book == null)
        {
            Console.WriteLine("Boken finnes ikke.");
            return;
        }

        // Bruker metoden i Book-klassen
        if (!book.BorrowCopy())
        {
            Console.WriteLine("Ingen eksemplarer tilgjengelig.");
            return;
        }

        Loans.Add(new Loan(book, borrower));
        Console.WriteLine("Bok lånt.");
    }

    // Leverer bok tilbake
    public void ReturnBook(string bookId, Bruker borrower)
    {
        var loan = Loans.FirstOrDefault(l =>
            l.Book.Id == bookId &&
            l.Borrower.Id == borrower.Id &&
            l.IsActive());

        if (loan == null)
        {
            Console.WriteLine("Fant ikke aktivt lån.");
            return;
        }

        loan.ReturnDate = DateTime.Now;
        loan.Book.ReturnCopy();

        Console.WriteLine("Bok levert.");
    }

    // Viser alle aktive lån
    public void PrintActiveLoans()
    {
        foreach (var l in Loans.Where(l => l.IsActive()))
        {
            Console.WriteLine(l.GetInfo());
        }
    }

    // Viser hele lånehistorikken
    public void PrintLoanHistory()
    {
        foreach (var l in Loans)
        {
            Console.WriteLine(l.GetInfo());
        }
    }
}