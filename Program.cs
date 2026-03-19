using UniversitySystem.Models;
using UniversitySystem.Services;

// Hovedprogrammet starter her
class Program
{
    static void Main()
    {
        // Lager et nytt system (her ligger all logikken)
        UniversityService service = new UniversityService();

        // Legger inn testdata slik at vi slipper å opprette alt manuelt
        service.AddStudent(new Student("S1", "Ola", "ola@mail.com"));
        service.AddStudent(new Student("S2", "Kari", "kari@mail.com"));
        service.AddEmployee(new Employee("E1", "Per", "per@mail.com", "Foreleser", "IT"));

        bool running = true;

        // Løkke som kjører menyen helt til brukeren avslutter
        while (running)
        {
            Console.WriteLine("\n--- MENY ---");
            Console.WriteLine("[1] Opprett kurs");
            Console.WriteLine("[2] Meld student til kurs");
            Console.WriteLine("[3] Print kurs og deltagere");
            Console.WriteLine("[4] Søk på kurs");
            Console.WriteLine("[5] Søk på bok");
            Console.WriteLine("[6] Lån bok");
            Console.WriteLine("[7] Returner bok");
            Console.WriteLine("[8] Registrer bok");
            Console.WriteLine("[9] Meld student av kurs");
            Console.WriteLine("[10] Vis aktive lån");
            Console.WriteLine("[11] Vis lånehistorikk");
            Console.WriteLine("[0] Avslutt");

            Console.Write("Velg: ");
            string choice = Console.ReadLine()!;

            // Switch brukes for å håndtere menyvalg
            switch (choice)
            {
                case "1":
                    service.CreateCourse();
                    break;

                case "2":
                    Console.Write("StudentID: ");
                    string sid = Console.ReadLine()!;
                    Console.Write("Kurskode: ");
                    string code = Console.ReadLine()!;
                    service.EnrollStudentInCourse(sid, code);
                    break;

                case "3":
                    service.PrintCoursesAndParticipants();
                    break;

                case "4":
                    Console.Write("Søk: ");
                    service.SearchCourse(Console.ReadLine()!);
                    break;

                case "5":
                    Console.Write("Søk: ");
                    service.SearchBook(Console.ReadLine()!);
                    break;

                case "6":
                    Console.Write("Bok-ID: ");
                    string bookId = Console.ReadLine()!;
                    Console.Write("Type (student/employee): ");
                    string type = Console.ReadLine()!;
                    Console.Write("ID: ");
                    string id = Console.ReadLine()!;
                    service.BorrowBook(bookId, type, id);
                    break;

                case "7":
                    Console.Write("Bok-ID: ");
                    string bid = Console.ReadLine()!;
                    Console.Write("Navn: ");
                    string name = Console.ReadLine()!;
                    service.ReturnBook(bid, name);
                    break;

                case "8":
                    service.RegisterBook();
                    break;

                case "9":
                    Console.Write("StudentID: ");
                    string s = Console.ReadLine()!;
                    Console.Write("Kurskode: ");
                    string c = Console.ReadLine()!;
                    service.UnenrollStudentFromCourse(s, c);
                    break;

                case "10":
                    service.PrintActiveLoans();
                    break;

                case "11":
                    service.PrintLoanHistory();
                    break;

                case "0":
                    running = false;
                    break;
            }
        }
    }
}