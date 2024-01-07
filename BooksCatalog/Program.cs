using System.IO;
using System.Numerics;
using System.Text.Json;
using BooksCatalog;
using Newtonsoft.Json;

internal class Program
{
    static void Main(string[] args)
    {
        BookCatalog bookCatalog = new BookCatalog();
        Start(bookCatalog);
        bookCatalog.Save();
    }

    private static void Start(BookCatalog bookCatalog)
    {
        Console.WriteLine(
            "Выберите команду: 1 - добавить книгу, 2 - найти книгу, 3 - вернуться в главное меню, любое другое - выход");
        string n = Console.ReadLine();
        if (n == "1")
        {
            AddBook(bookCatalog);
        }
        else if (n == "2")
        {
            FindBook(bookCatalog);
        }
        else if (n == "3")
        {
            Start(bookCatalog);
        }
    }

    private static void FindBook(BookCatalog bookCatalog)
    {
        Console.WriteLine(
            "Выберите команду: 1 - найти по названию, 2 - найти по ISBN, 3 - найти по автору, 4 - найти по ключевым словам");
        string n = Console.ReadLine();
        if (n == "1")
        {
            Search("Название", FindByEnum.Title, bookCatalog);
        }
        else if (n == "2")
        {
            Search("ISBN", FindByEnum.ISBN, bookCatalog);
        }
        else if (n == "3")
        {
            Search("Автора", FindByEnum.Author, bookCatalog);

        }
        else if (n == "4")
        {
            Search("Ключевые слова через запятую", FindByEnum.Keyword, bookCatalog);

        }

        Start(bookCatalog);
    }

    private static void Search(string name, FindByEnum findByEnum, BookCatalog bookCatalog)
    {
        Console.WriteLine($"Введите {name}");
        string value = Console.ReadLine();
        var books = bookCatalog.FindBooks(findByEnum, value);
        PrintBooks(books);
    }

    private static void PrintBooks(IEnumerable<Book> books)
    {
        if (!books.Any())
        {
            Console.WriteLine("Ничего не найдено");
        }
        else
        {
            Console.WriteLine("Найденные результаты.");
        }
       
        foreach (Book book in books)
        {
            Console.WriteLine($"Название: {book.Title}, Автор:{book.Author},  ISBN: {book.ISBN},  Дата публикации:{book.Date}, Жанр:{book.Genre}");
        }
    }

    private static string GetFromUser(string title)
    {
        Console.WriteLine($"Введите {title}");
        return Console.ReadLine();
    }

    private static DateTime GetPublicationDate()
    {
        string date = GetFromUser("дату публикации");

        if (!DateTime.TryParse(date, out DateTime pubication))
        {
            Console.WriteLine("Введите валидную дату");
            return GetPublicationDate();
        }
        else
        {
            return pubication;
        }
    }
    private static void AddBook(BookCatalog bookCatalog)
    {
        string title = GetFromUser("название");
        string author = GetFromUser("автора");
        string isbn = GetFromUser("ISBN");
        string brief = GetFromUser("аннатоцию");
        string genre = GetFromUser("жанр");
        DateTime date = GetPublicationDate();
        bookCatalog.AddBook(title, author, isbn, brief, date, genre);
        Start(bookCatalog);
    }
}