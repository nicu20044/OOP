using System;
using System.IO;

public static class FileInfoAnalyzer
{
    public static void DisplayGeneralInfo(FileInfo file)
    {
        Console.WriteLine($"File Name: {file.Name}");
        Console.WriteLine($"Extension: {file.Extension}");
        Console.WriteLine($"Creation Date: {file.CreationTime}");
        Console.WriteLine($"Last Modified Date: {file.LastWriteTime}");

        if (file.Extension.Equals(".png", StringComparison.OrdinalIgnoreCase) ||
            file.Extension.Equals(".jpg", StringComparison.OrdinalIgnoreCase))
        {
            DisplayImageInfo(file);
        }
        else if (file.Extension.Equals(".txt", StringComparison.OrdinalIgnoreCase))
        {
            DisplayTextInfo(file);
        }
        else if (file.Extension.Equals(".cs", StringComparison.OrdinalIgnoreCase) ||
                 file.Extension.Equals(".java", StringComparison.OrdinalIgnoreCase))
        {
            DisplayProgramFileInfo(file);
        }
        else
        {
            Console.WriteLine("No additional information available for this file type.");
        }
    }

    private static void DisplayImageInfo(FileInfo file)
    {
        Console.WriteLine($"Image File Detected: {file.Name}");
        Console.WriteLine("Dimensions: Cannot be calculated without additional libraries.");
    }

    private static void DisplayTextInfo(FileInfo file)
    {
        int lineCount = 0;
        int wordCount = 0;
        int charCount = 0;

        try
        {
            using (var reader = new StreamReader(file.FullName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lineCount++;
                    charCount += line.Length;
                    wordCount += line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;
                }
            }

            Console.WriteLine($"Line Count: {lineCount}");
            Console.WriteLine($"Word Count: {wordCount}");
            Console.WriteLine($"Character Count: {charCount}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error reading text file: {e.Message}");
        }
    }

    private static void DisplayProgramFileInfo(FileInfo file)
    {
        int lineCount = 0;
        int classCount = 0;
        int methodCount = 0;

        try
        {
            using (var reader = new StreamReader(file.FullName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lineCount++;
                    if (line.Contains("class ")) classCount++;
                    if (line.Contains("void ") || line.Contains("def ") || line.Contains("function ")) methodCount++;
                }
            }

            Console.WriteLine($"Line Count: {lineCount}");
            Console.WriteLine($"Class Count: {classCount}");
            Console.WriteLine($"Method Count: {methodCount}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error reading program file: {e.Message}");
        }
    }
}
