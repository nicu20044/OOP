using oop_lab2;

public class Program
{
    public static void Main(string[] args)
    {
        SaveManager saveManager = new SaveManager();
        University university = saveManager.LoadUniversity();
        bool execution = true;

        while (execution)
        {
            Console.WriteLine("University Management System");
            Console.WriteLine("1. Create Faculty");
            Console.WriteLine("2. Enroll Student in Faculty");
            Console.WriteLine("3. Search Faculty by Student ID");
            Console.WriteLine("4. Display All Faculties");
            Console.WriteLine("5. Display Faculties by Study Field");
            Console.WriteLine("6. Display all students from a Faculty");
            Console.WriteLine("7. Exit and Save");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter Faculty Name: ");
                    string facultyName = Console.ReadLine();
                    Console.Write("Enter Faculty Abbreviation: ");
                    string abbreviation = Console.ReadLine();
                    Console.Write("Enter Study Field: ");
                    StudyField studyField = Enum.Parse<StudyField>(Console.ReadLine(), true);

                    university.CreateFaculty(facultyName, abbreviation, studyField);
                    break;

                case "2":
                    Console.Write("Enter Faculty Name to Enroll Student In: ");
                    string targetFacultyName = Console.ReadLine();
                    Faculty faculty = university.Faculties.FirstOrDefault(f =>
                        f.Name.Equals(targetFacultyName, StringComparison.OrdinalIgnoreCase));

                    if (faculty == null)
                    {
                        Console.WriteLine("Faculty not found.");
                        break;
                    }

                    Console.Write("Enter Student First Name: ");
                    string firstName = Console.ReadLine();
                    Console.Write("Enter Student Last Name: ");
                    string lastName = Console.ReadLine();
                    Console.Write("Enter Student Email: ");
                    string email = Console.ReadLine();
                    Console.Write("Enter Student Date of Birth (dd-mm-yyyy): ");
                    DateTime dob = DateTime.Parse(Console.ReadLine());

                    faculty.CreateAndEnrollStudent(firstName, lastName, email, dob);
                    break;

                case "3":
                    Console.Write("Enter Student ID to Find Faculty: ");
                    uint searchStudentId = uint.Parse(Console.ReadLine());
                    Faculty studentFaculty = university.FindFacultyByStudentId(searchStudentId);

                    if (studentFaculty != null)
                    {
                        Console.WriteLine($"Student belongs to Faculty: {studentFaculty.Name}");
                    }
                    else
                    {
                        Console.WriteLine("No faculty found for this student ID.");
                    }

                    break;

                case "4":
                    university.DisplayAllFaculties();
                    break;

                case "5":
                    Console.WriteLine("Select Study Field:");
                    foreach (var field in Enum.GetValues(typeof(StudyField)))
                    {
                        Console.WriteLine($"- {field}");
                    }

                    Console.Write("Enter Study Field: ");
                    StudyField selectedField = (StudyField)Enum.Parse(typeof(StudyField), Console.ReadLine().ToUpper());
                    university.DisplayFacultiesByField(selectedField);
                    break;

                case "6":
                    Console.Write("Enter Faculty Name to Display Students: ");
                    string targetFacultyNameForStudents = Console.ReadLine();
                    Faculty targetFaculty = university.Faculties.FirstOrDefault(f => 
                        f.Name.Equals(targetFacultyNameForStudents, StringComparison.OrdinalIgnoreCase));

                    if (targetFaculty!= null)
                    {
                        targetFaculty.DisplayEnrolledStudents();
                    }
                    else
                    {
                        Console.WriteLine("Faculty not found.");
                    }
                    break;
                
                case "7":
                    saveManager.SaveUniversity(university);
                    execution = false;
                    Console.WriteLine("Exiting and saving data.");
                    break;

            }
        }
    }
}
