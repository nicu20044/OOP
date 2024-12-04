using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
public class UpdateFile
{
    private readonly string folderPath;
    private readonly List<Dictionary<string, FileAttributes>> history;

    public UpdateFile(string folderPath)
    {
        this.folderPath = folderPath;
        this.history = new List<Dictionary<string, FileAttributes>>();
    }

    public void Commit()
    {
        var snapshot = new Dictionary<string, FileAttributes>();
        string commitFolder = Path.Combine(folderPath, "commits", $"commit_{history.Count}");

        Directory.CreateDirectory(commitFolder);

        var folder = new DirectoryInfo(folderPath);
        foreach (var file in folder.GetFiles())
        {
            var fileAttributes = File.GetAttributes(file.FullName);
            snapshot[file.Name] = fileAttributes;

            try
            {
                File.Copy(file.FullName, Path.Combine(commitFolder, file.Name), true);
            }
            catch (IOException e)
            {
                Console.WriteLine($"Error copying file: {file.Name}. {e.Message}");
            }
        }

        history.Add(snapshot);
        Console.WriteLine($"Commit successful. Commit number: {history.Count - 1}");
    }

    public void Checkout(int commitNumber)
    {
        if (commitNumber < 0 || commitNumber >= history.Count)
        {
            Console.WriteLine("Invalid commit number.");
            return;
        }

        var selectedSnapshot = history[commitNumber];
        var folder = new DirectoryInfo(folderPath);

        // Delete existing files
        foreach (var file in folder.GetFiles())
        {
            file.Delete();
        }

        string commitFolder = Path.Combine(folderPath, "commits", $"commit_{commitNumber}");
        foreach (var fileName in selectedSnapshot.Keys)
        {
            string sourceFile = Path.Combine(commitFolder, fileName);
            string targetFile = Path.Combine(folderPath, fileName);

            try
            {
                File.Copy(sourceFile, targetFile, true);
            }
            catch (IOException e)
            {
                Console.WriteLine($"Error restoring file: {fileName}. {e.Message}");
            }
        }

        Console.WriteLine($"Checked out to commit {commitNumber}");
    }

    public void Status()
    {
        var folder = new DirectoryInfo(folderPath);
        var currentFiles = folder.GetFiles()
            .ToDictionary(file => file.Name, file => File.GetAttributes(file.FullName)); // Corrected line

        if (history.Count > 0)
        {
            var lastCommitSnapshot = history[^1];

            foreach (var fileName in currentFiles.Keys)
            {
                var currentAttributes = currentFiles[fileName];
                if (lastCommitSnapshot.ContainsKey(fileName))
                {
                    var lastAttributes = lastCommitSnapshot[fileName];
                    if (currentAttributes == lastAttributes)
                    {
                        Console.WriteLine($"{fileName} has not changed.");
                    }
                    else
                    {
                        Console.WriteLine($"{fileName} has been modified.");
                    }
                }
                else
                {
                    Console.WriteLine($"{fileName} is a new file.");
                }
            }

            foreach (var fileName in lastCommitSnapshot.Keys)
            {
                if (!currentFiles.ContainsKey(fileName))
                {
                    Console.WriteLine($"{fileName} was deleted.");
                }
            }
        }
        else
        {
            Console.WriteLine("No commits yet.");
        }
    }

    public void Info(string fileName)
    {
        string filePath = Path.Combine(folderPath, fileName);
        var file = new FileInfo(filePath);

        if (file.Exists)
        {
            FileInfoAnalyzer.DisplayGeneralInfo(file);
        }
        else
        {
            Console.WriteLine($"File {fileName} does not exist.");
        }
    }
}
