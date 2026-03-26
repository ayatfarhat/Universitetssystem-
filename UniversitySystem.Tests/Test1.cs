using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniversitySystem.Models;
using UniversitySystem.Services;

namespace UniversitySystem.Tests;

[TestClass]
public class UniversityTests
{
    [TestMethod]
    public void Student_Can_Enroll_In_Course()
    {
        UniversityService service = new UniversityService();

        Student student = new Student("S001", "Ayat", "ayat@uia.no", "ayat", "1234");
        Employee teacher = new Employee("A001", "Per", "per@uia.no", "per", "1234", "Faglærer", "IT");

        service.AddStudent(student);
        service.AddEmployee(teacher);

        service.CreateCourseDirect("IS110", "Objektorientert programmering", 10, 30, teacher);

        service.EnrollStudentInCourse(student, "IS110");

        Assert.AreEqual(1, student.PåmeldteKurs.Count);
    }

    [TestMethod]
    public void Student_Cannot_Enroll_Twice_In_Same_Course()
    {
        UniversityService service = new UniversityService();

        Student student = new Student("S001", "Ayat", "ayat@uia.no", "ayat", "1234");
        Employee teacher = new Employee("A001", "Per", "per@uia.no", "per", "1234", "Faglærer", "IT");

        service.AddStudent(student);
        service.AddEmployee(teacher);

        service.CreateCourseDirect("IS110", "Objektorientert programmering", 10, 30, teacher);

        service.EnrollStudentInCourse(student, "IS110");
        service.EnrollStudentInCourse(student, "IS110");

        Assert.AreEqual(1, student.PåmeldteKurs.Count);
    }

    [TestMethod]
    public void Book_Cannot_Be_Borrowed_When_No_Copies_Available()
    {
        UniversityService service = new UniversityService();

        Student student1 = new Student("S001", "Ayat", "ayat@uia.no", "ayat", "1234");
        Student student2 = new Student("S002", "Ali", "ali@uia.no", "ali", "1234");

        Book book = new Book("B001", "Clean Code", "Robert C. Martin", 2008, 1);

        service.AddStudent(student1);
        service.AddStudent(student2);
        service.Books.Add(book);

        service.BorrowBook("B001", student1);
        service.BorrowBook("B001", student2);

        Assert.AreEqual(0, book.AvailableCopies);
        Assert.AreEqual(1, service.Loans.Count);
    }
}