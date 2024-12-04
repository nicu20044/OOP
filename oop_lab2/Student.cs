namespace oop_lab2;

public class Student
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public DateTime? GraduationDate { get; set; }
    public uint StudentID { get; set; }

    public Student(string firstName, string lastName, string email, DateTime enrollmentDate, DateTime dateOfBirth,
        uint studentId)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        EnrollmentDate = enrollmentDate;
        DateOfBirth = dateOfBirth;
        StudentID = studentId;
    }
}