namespace lab3;

using System;
using System.IO;
using System.Linq;

public class FileAttributes
{
    private long FileSize { get; set; }
    private long LastModified { get; set; }

    public FileAttributes(FileInfo file)
    {
        try
        {
            FileSize = file.Length;
            LastModified = file.LastWriteTimeUtc.Ticks;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public bool IsSameAs(FileAttributes other)
    {
        return FileSize == other.FileSize && LastModified == other.LastModified;
    }
}
