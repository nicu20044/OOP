namespace oop_lab2;

public class Faculty
{
    public string Name { get; set; }
    public string Abbreviation { get; set; }
    public StudyField StudyField { get; set; }
    public List<Student> Students { get; set; } = new List<Student>();
    public List<Student> GraduatedStudents { get; set; } = new List<Student>();
    private static uint StudentId = 0;

    public Faculty(string name, string abbreviation, StudyField studyField)
    {
        Name = name;
        Abbreviation = abbreviation;
        StudyField = studyField;
    }

    public void CreateAndEnrollStudent(string firstName, string lastName, string email, DateTime dateOfBirth)
    {
        Student newStudent = new Student(firstName, lastName, email, DateTime.Now, dateOfBirth, ++StudentId);
        Students.Add(newStudent);
        Console.WriteLine($"Student {newStudent.FirstName} {newStudent.LastName} enrolled in {Name}.");
    }

    public void GraduateStudent(Student student)
    {
        if (Students.Remove(student))
        {
            student.GraduationDate = DateTime.Now;
            GraduatedStudents.Add(student);
            Console.WriteLine($"Student {student.FirstName} {student.LastName} graduated from {Name}.");
        }
        else
        {
            Console.WriteLine($"Student {student.FirstName} {student.LastName} not found in {Name}.");
        }
    }

    public void DisplayEnrolledStudents()
    {
        Console.WriteLine($"Currently enrolled students in {Name}:");
        if (Students.Count == 0)
        {
            Console.WriteLine("No students currently enrolled.");
            return;
        }

        foreach (var student in Students)
        {
            Console.WriteLine($"- ID: {student.StudentID}, Name: {student.FirstName} {student.LastName}");
        }
    }

    public void DisplayGraduatedStudents()
    {
        Console.WriteLine($"Graduated students from {Name}:");
        if (GraduatedStudents.Count == 0)
        {
            Console.WriteLine("No students have graduated yet.");
            return;
        }

        foreach (var student in GraduatedStudents)
        {
            Console.WriteLine($"- {student.FirstName} {student.LastName} (Graduated on: {student.GraduationDate?.ToShortDateString()})");
        }
    }

    public bool IsStudentInFaculty(Student student)
    {
        return Students.Contains(student) || GraduatedStudents.Contains(student);
    }

    public Student GetStudentById(uint studentId)
    {
        return Students.FirstOrDefault(st => st.StudentID == studentId) ??
               GraduatedStudents.FirstOrDefault(st => st.StudentID == studentId);
    }

    public Student GetStudentByEmail(string email)
    {
        return Students.FirstOrDefault(st => st.Email == email) ??
               GraduatedStudents.FirstOrDefault(st => st.Email == email);
    }
}
