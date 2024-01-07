using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace BooksCatalog
{
    public class BookCatalog
    {
        public List<Book> Books { get; set; }
        public BookCatalog()
        {
            Books = ReadBooks() ?? new List<Book>();
        }

        public BookCatalog(IEnumerable<Book> books)
        {
            Books = books.ToList();
        }

        public void Save()
        {
            string json = JsonConvert.SerializeObject(Books);
            File.WriteAllText("books.json", json);
        }
        private static List<Book> ReadBooks()
        {
            string path = "books.json";
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<List<Book>>(json);
            }
            else
            {
                return null;
            }
        }

        public void AddBook(string title, string author, string isbn, string annotaion, DateTime date, string genre)
        {
            var book = new Book(title, author, isbn, annotaion, date, genre);
            Books.Add(book);
        }

        public IEnumerable<Book> FindByAuthor(string author)
        {
            return Books.Where(x => x.Author == author);
        }

        public IEnumerable<Book> FindByISBN(string isbn)
        {
            return Books.Where(x => x.ISBN == isbn);
        }

        public IEnumerable<Book> FindByTitle(string title)
        {
            return Books.Where(x => x.Title.Contains(title));
        }

        public IEnumerable<Book> FindByKeyWords(string[] keyWords)
        {
            Dictionary<Book, int> books = new Dictionary<Book, int>();
            foreach (string word in keyWords)
            {
                string keyWord = word.Trim();
                foreach (var book in Books)
                {
                    if (book.Brief.Contains(keyWord))
                    {
                        if (books.ContainsKey(book))
                        {
                            books[book] = books[book] + 1;
                        }
                        else
                        {
                            books.Add(book, 1);
                        }
                    }
                }
            }

            return books.OrderByDescending(x => x.Value).Select(x => x.Key).ToList();
        }

        public IEnumerable<Book> FindBooks(FindByEnum findByEnum, string value)
        {
            switch (findByEnum)
            {
                case FindByEnum.Author: return FindByAuthor(value);
                case FindByEnum.ISBN:  return FindByISBN(value);
                case FindByEnum.Title: return FindByTitle(value);
                case FindByEnum.Keyword: return FindByKeyWords(value.Split(','));
                default: return new List<Book>();
            }
        }
    }
}
