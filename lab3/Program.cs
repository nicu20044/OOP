using System;

public class Program
{
    public static void Main(string[] args)
    {
        string folderPath = @"C:\Users\nicud\Desktop\OOP\lab3\git";
        UpdateFile fileMonitor = new UpdateFile(folderPath);

        while (true)
        {
            Console.WriteLine("Enter a command (commit, info, status, checkout, exit):");
            string command = Console.ReadLine().Trim();

         switch (command)
            {
                case "commit":
                    fileMonitor.Commit();
                    break;
                case "info":
                    Console.Write("Enter filename: ");
                    string filename = Console.ReadLine();
                    fileMonitor.Info(filename);
                    break;
                case "status":
                    fileMonitor.Status();
                    break;
                case "checkout":
                    Console.Write("Enter commit number: ");
                    if (int.TryParse(Console.ReadLine(), out int commitNumber))
                    {
                        fileMonitor.Checkout(commitNumber);
                    }
                    else
                    {
                        Console.WriteLine("Invalid commit number.");
                    }
                    break;
                case "exit":
                    Console.WriteLine("Exiting...");
                    return;
                default:
                    Console.WriteLine("Invalid command. Try again.");
                    break;
            }
        }
    }
}