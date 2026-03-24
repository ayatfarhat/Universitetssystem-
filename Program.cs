using UniversitySystem.Models;
using UniversitySystem.Services;

// Lager service-objektet som styrer hele systemet
UniversityService service = new UniversityService();

// Legger inn noen startbrukere så du kan teste programmet
service.AddStudent(new Student("S001", "Ayat", "ayat@uia.no", "ayat", "1234"));
service.AddStudent(new Student("S002", "Ali", "ali@uia.no", "ali", "1234"));

service.AddEmployee(new Employee("A001", "Per Hansen", "per@uia.no", "per", "1234", "Faglærer", "Informatikk"));
service.AddEmployee(new Employee("A002", "Lise Berg", "lise@uia.no", "lise", "1234", "Bibliotekar", "Bibliotek"));

// Startløkke for innlogging eller registrering
Bruker? loggedInUser = null;

while (loggedInUser == null)
{
    Console.WriteLine("\n===== Velkommen til universitetssystemet =====");
    Console.WriteLine("[1] Logg inn");
    Console.WriteLine("[2] Registrer ny bruker");
    Console.WriteLine("[0] Avslutt");
    Console.Write("Velg: ");
    string valg = Console.ReadLine()!;

    switch (valg)
    {
        case "1":
            Console.Write("Brukernavn: ");
            string brukernavn = Console.ReadLine()!;

            Console.Write("Passord: ");
            string passord = Console.ReadLine()!;

            loggedInUser = service.Login(brukernavn, passord);

            if (loggedInUser == null)
            {
                Console.WriteLine("Feil brukernavn eller passord.");
            }
            else
            {
                Console.WriteLine($"Velkommen {loggedInUser.Navn}!");
            }
            break;

        case "2":
            service.RegisterUser();
            break;

        case "0":
            return;

        default:
            Console.WriteLine("Ugyldig valg.");
            break;
    }
}

// Hvis innlogget bruker er student, vis studentmeny
if (loggedInUser is Student student)
{
    ShowStudentMenu(service, student);
}
// Hvis innlogget bruker er ansatt, sjekk rolle
else if (loggedInUser is Employee employee)
{
    if (employee.Position.Equals("Faglærer", StringComparison.OrdinalIgnoreCase))
    {
        ShowTeacherMenu(service, employee);
    }
    else if (employee.Position.Equals("Bibliotekar", StringComparison.OrdinalIgnoreCase))
    {
        ShowLibraryMenu(service, employee);
    }
    else
    {
        Console.WriteLine("Ingen meny tilgjengelig for denne rollen.");
    }
}


// Studentmeny
static void ShowStudentMenu(UniversityService service, Student student)
{
    bool running = true;

    while (running)
    {
        Console.WriteLine("\n===== Studentmeny =====");
        Console.WriteLine("[1] Meld på kurs");
        Console.WriteLine("[2] Meld av kurs");
        Console.WriteLine("[3] Se mine kurs");
        Console.WriteLine("[4] Søk på kurs");
        Console.WriteLine("[5] Søk på bok");
        Console.WriteLine("[6] Lån bok");
        Console.WriteLine("[7] Returner bok");
        Console.WriteLine("[8] Se karakterer");
        Console.WriteLine("[0] Logg ut");
        Console.Write("Velg: ");

        string choice = Console.ReadLine()!;

        switch (choice)
        {
            case "1":
                Console.Write("Kurskode: ");
                service.EnrollStudentInCourse(student, Console.ReadLine()!);
                break;

            case "2":
                Console.Write("Kurskode: ");
                service.UnenrollStudentFromCourse(student, Console.ReadLine()!);
                break;

            case "3":
                service.ShowStudentCourses(student);
                break;

            case "4":
                Console.Write("Søk etter kurs: ");
                service.SearchCourse(Console.ReadLine()!);
                break;

            case "5":
                Console.Write("Søk etter bok: ");
                service.SearchBook(Console.ReadLine()!);
                break;

            case "6":
                Console.Write("Bok-ID: ");
                service.BorrowBook(Console.ReadLine()!, student);
                break;

            case "7":
                Console.Write("Bok-ID: ");
                service.ReturnBook(Console.ReadLine()!, student);
                break;

            case "8":
                service.ShowStudentGrades(student);
                break;

            case "0":
                running = false;
                break;

            default:
                Console.WriteLine("Ugyldig valg.");
                break;
        }
    }
}


// Faglærermeny
static void ShowTeacherMenu(UniversityService service, Employee teacher)
{
    bool running = true;

    while (running)
    {
        Console.WriteLine("\n===== Faglærermeny =====");
        Console.WriteLine("[1] Opprett kurs");
        Console.WriteLine("[2] Søk på kurs");
        Console.WriteLine("[3] Søk på bok");
        Console.WriteLine("[4] Lån bok");
        Console.WriteLine("[5] Returner bok");
        Console.WriteLine("[6] Sett karakter");
        Console.WriteLine("[7] Registrer pensum");
        Console.WriteLine("[8] Skriv ut kurs og deltakere");
        Console.WriteLine("[0] Logg ut");
        Console.Write("Velg: ");

        string choice = Console.ReadLine()!;

        switch (choice)
        {
            case "1":
                service.CreateCourse(teacher);
                break;

            case "2":
                Console.Write("Søk etter kurs: ");
                service.SearchCourse(Console.ReadLine()!);
                break;

            case "3":
                Console.Write("Søk etter bok: ");
                service.SearchBook(Console.ReadLine()!);
                break;

            case "4":
                Console.Write("Bok-ID: ");
                service.BorrowBook(Console.ReadLine()!, teacher);
                break;

            case "5":
                Console.Write("Bok-ID: ");
                service.ReturnBook(Console.ReadLine()!, teacher);
                break;

            case "6":
                Console.Write("Kurskode: ");
                string courseCode = Console.ReadLine()!;

                Console.Write("Student-ID: ");
                string studentId = Console.ReadLine()!;

                Console.Write("Karakter: ");
                string grade = Console.ReadLine()!;

                service.SetGrade(teacher, courseCode, studentId, grade);
                break;

            case "7":
                Console.Write("Kurskode: ");
                string code = Console.ReadLine()!;

                Console.Write("Pensum: ");
                string curriculum = Console.ReadLine()!;

                service.AddCurriculum(teacher, code, curriculum);
                break;

            case "8":
                service.PrintCoursesAndParticipants();
                break;

            case "0":
                running = false;
                break;

            default:
                Console.WriteLine("Ugyldig valg.");
                break;
        }
    }
}


// Bibliotekmeny
static void ShowLibraryMenu(UniversityService service, Employee employee)
{
    bool running = true;

    while (running)
    {
        Console.WriteLine("\n===== Bibliotekmeny =====");
        Console.WriteLine("[1] Registrer bok");
        Console.WriteLine("[2] Søk på bok");
        Console.WriteLine("[3] Se aktive lån");
        Console.WriteLine("[4] Se lånehistorikk");
        Console.WriteLine("[0] Logg ut");
        Console.Write("Velg: ");

        string choice = Console.ReadLine()!;

        switch (choice)
        {
            case "1":
                service.RegisterBook();
                break;

            case "2":
                Console.Write("Søk etter bok: ");
                service.SearchBook(Console.ReadLine()!);
                break;

            case "3":
                service.PrintActiveLoans();
                break;

            case "4":
                service.PrintLoanHistory();
                break;

            case "0":
                running = false;
                break;

            default:
                Console.WriteLine("Ugyldig valg.");
                break;
        }
    }
}