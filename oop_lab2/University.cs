namespace oop_lab2;

public class University
{
    public List<Faculty> Faculties = new List<Faculty>();

    public void CreateFaculty(string name, string abbreviation, StudyField studyField)
    {
        Faculty newFaculty = new Faculty(name, abbreviation, studyField);
        Faculties.Add(newFaculty);
        Console.WriteLine($"Faculty {name} created and added to the university.");
    }
    public Faculty FindFacultyByStudentEmail(string email)
    {
        return Faculties.FirstOrDefault(faculty => faculty.GetStudentByEmail(email) != null);
    }

    public Faculty FindFacultyByStudentId(uint studentID)
    {
        return Faculties.FirstOrDefault(faculty => faculty.GetStudentById(studentID) != null);
    }
    
    public void DisplayAllFaculties()
    {
        Console.WriteLine("All Faculties in the University:");
        foreach (var faculty in Faculties)
        {
            Console.WriteLine($"- {faculty.Name} ({faculty.Abbreviation}) - Field: {faculty.StudyField}");
        }
    }
    
    public void DisplayFacultiesByField(StudyField field)
    {
        Console.WriteLine($"Faculties in the field of {field}:");
        var facultiesInField = Faculties.Where(faculty => faculty.StudyField == field);

        if (!facultiesInField.Any())
        {
            Console.WriteLine("No faculties found in this field.");
            return;
        }

        foreach (var faculty in facultiesInField)
        {
            Console.WriteLine($"- {faculty.Name} ({faculty.Abbreviation})");
        }
    }
}