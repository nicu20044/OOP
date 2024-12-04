namespace ConsoleApp1;

internal static class Program
{
    public static void Main(string[] args)
    {
        var library = new Library();
        library.AddBook("Book1", "author1", 12312);
        library.AddBook("Book2", "author2", 12313);
        library.AddBook("Book3", "author3", 12314);
        library.AddBook("Book4", "author4", 12315);
        library.AddBook("Book5", "author5", 12316);

        library.RemoveBook("Book5");

        DisplayBooks(library.GetBooks());
    }


    private static void DisplayBooks(ICollection<Book> books)
    {
        foreach (var book in books)
        {
            Console.WriteLine(book.ToString());
        }
    }
}