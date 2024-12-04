namespace ConsoleApp1;

class Book(string title, string author, int isbn)
{
    public string Title = title;
    public string Author = author;
    private int ISBN = isbn;

    public override string ToString()
    {
        return $"{Title} {Author} {ISBN}";
    }
}