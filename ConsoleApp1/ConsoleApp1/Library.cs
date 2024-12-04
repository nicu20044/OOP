namespace ConsoleApp1;

class Library
{
    private readonly List<Book> _books = [];

    public Book AddBook(string title, string author, int ISBN)
    {
        var book = new Book(title, author, ISBN);

        _books.Add(book);
        return book;
    }

    public void RemoveBook(string title)
    {
        var bookToRemove = _books.FirstOrDefault(book => book.Title.ToLower() == title.ToLower());
        if (bookToRemove != null)
        {
            _books.Remove(bookToRemove);
        }
    }

    public ICollection<Book> GetBooks()
    {
        return _books;
    }
}