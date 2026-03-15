using UniversitySystem.Models;
using UniversitySystem.Services;
// Main menu for the university system console application
// Handles user interaction and calls the service layer
class Program
{
    static void Main()
    {
        UniversityService service = new UniversityService();

        // Startdata
        service.AddStudent(new Student("S001", "Ayat", "ayat@uia.no"));
        service.AddStudent(new Student("S002", "Ali", "ali@uia.no"));
        service.AddEmployee(new Employee("E001", "Per Hansen", "per@uia.no", "Bibliotekar", "Bibliotek"));

        bool running = true;

        while (running)
        {
            Console.WriteLine("\n===== Universitetssystem =====");
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

            switch (choice)
            {
                case "1":
                    service.CreateCourse();
                    break;

                case "2":
                    Console.Write("StudentID: ");
                    string studentId = Console.ReadLine()!;

                    Console.Write("Kurskode: ");
                    string courseCode = Console.ReadLine()!;

                    service.EnrollStudentInCourse(studentId, courseCode);
                    break;

                case "3":
                    service.PrintCoursesAndParticipants();
                    break;

                case "4":
                    Console.Write("Søk etter kurskode eller kursnavn: ");
                    string courseSearch = Console.ReadLine()!;
                    service.SearchCourse(courseSearch);
                    break;

                case "5":
                    Console.Write("Søk etter boktittel eller forfatter: ");
                    string bookSearch = Console.ReadLine()!;
                    service.SearchBook(bookSearch);
                    break;

                case "6":
                    Console.Write("Bok-ID: ");
                    string borrowBookId = Console.ReadLine()!;

                    Console.Write("Låner-type (student/employee): ");
                    string borrowerType = Console.ReadLine()!;

                    Console.Write("Låner-ID: ");
                    string borrowerId = Console.ReadLine()!;

                    service.BorrowBook(borrowBookId, borrowerType, borrowerId);
                    break;

                case "7":
                    Console.Write("Bok-ID: ");
                    string returnBookId = Console.ReadLine()!;

                    Console.Write("Navn på låner: ");
                    string borrowerName = Console.ReadLine()!;

                    service.ReturnBook(returnBookId, borrowerName);
                    break;

                case "8":
                    service.RegisterBook();
                    break;

                case "9":
                    Console.Write("StudentID: ");
                    string removeStudentId = Console.ReadLine()!;

                    Console.Write("Kurskode: ");
                    string removeCourseCode = Console.ReadLine()!;

                    service.UnenrollStudentFromCourse(removeStudentId, removeCourseCode);
                    break;

                case "10":
                    service.PrintActiveLoans();
                    break;

                case "11":
                    service.PrintLoanHistory();
                    break;

                case "0":
                    running = false;
                    Console.WriteLine("Programmet avsluttes.");
                    break;

                default:
                    Console.WriteLine("Ugyldig valg. Prøv igjen.");
                    break;
            }
        }
    }
}