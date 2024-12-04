namespace oop_lab2;
using System.IO;

public class SaveManager
{
    string SaveFile = @"C:\Users\nicud\Desktop\OOP\lab2\oop_lab2\save_manager.txt";

    public void SaveUniversity(University university)
    {
        try
        {
            using StreamWriter writer = new StreamWriter(SaveFile);
            
            foreach (var faculty in university.Faculties)
            {
                writer.WriteLine($"FACULTY:{faculty.Name}|{faculty.Abbreviation}|{faculty.StudyField}");
                foreach (var student in faculty.Students)
                {
                    writer.WriteLine($"STUDENT:Name:{student.FirstName} {student.LastName}| email:{student.Email}|" +
                                     $"{student.DateOfBirth:dd-mm-yyyy}|{student.EnrollmentDate:dd-mm-yyyy}|" +
                                     $"{student.StudentID}|{student.GraduationDate:dd-mm-yyyy}");
                }
            }

            Console.WriteLine("University data saved successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving university data: {ex.Message}");
        }
    }

    public University LoadUniversity()
    {
        University university = new University();

        if (!File.Exists(SaveFile))
        {
            Console.WriteLine("No saved data found. Starting fresh.");
            return university;
        }

        try
        {
            using StreamReader reader = new StreamReader(SaveFile);
            Faculty? currentFaculty = null;

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.StartsWith("FACULTY:"))
                {
                    string[] facultyParts = line.Substring(8).Split('|');
                    string name = facultyParts[0];
                    string abbreviation = facultyParts[1];
                    StudyField studyField = Enum.Parse<StudyField>(facultyParts[2]);

                    currentFaculty = new Faculty(name, abbreviation, studyField);
                    university.Faculties.Add(currentFaculty);
                }
                else if (line.StartsWith("STUDENT:") && currentFaculty != null)
                {
                    string[] studentParts = line.Substring(8).Split('|');
                    string firstName = studentParts[0];
                    string lastName = studentParts[1];
                    string email = studentParts[2];
                    DateTime dateOfBirth = DateTime.Parse(studentParts[3]);
                    DateTime enrollmentDate = DateTime.Parse(studentParts[4]);
                    uint studentId = uint.Parse(studentParts[5]);
                    DateTime? graduationDate = string.IsNullOrEmpty(studentParts[6]) ? null : DateTime.Parse(studentParts[6]);

                    Student student = new Student(firstName, lastName, email, enrollmentDate, dateOfBirth, studentId)
                    {
                        GraduationDate = graduationDate
                    };

                    currentFaculty.Students.Add(student);
                }
            }

            Console.WriteLine("University data loaded successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading university data: {ex.Message}");
        }

        return university;
    }
}
