using BooksCatalog;

namespace BookCatalogTests
{
    public class BookCatalogTests
    {
        [Fact]
        public void NewBookAddedToBooks()
        {
            var books = new List<Book>();
            var catalog = new BookCatalog(books);
            int count = catalog.Books.Count;
            Assert.Equal(0, count);
            catalog.AddBook("Title", "Author", "0000-000", "key, work, word", DateTime.Now, "Roman");
            Assert.Equal(1, catalog.Books.Count);
        }

        [Theory]
        [InlineData(FindByEnum.Title, "Title", 2)]
        [InlineData(FindByEnum.Author, "Somebody", 1)]
        [InlineData(FindByEnum.Author, "Author2", 1)]
        [InlineData(FindByEnum.ISBN, "0200-000", 1)]
        [InlineData(FindByEnum.Keyword, "key, work", 2)]
        public void FindBooksReturnsRightResults(FindByEnum findByEnum, string keyword, int count)
        {
            var catalog = GetBookCatalog();
            var books = catalog.FindBooks(findByEnum, keyword);
            Assert.True(books.Any());
            Assert.Equal(count, books.Count());
        }

        [Fact]
        public void FindBooksReturnsSortedResults()
        {
            var catalog = GetBookCatalog();
            var books = catalog.FindBooks(FindByEnum.Keyword, "work, goods");
            Assert.True(books.Any());
            Assert.Equal(2, books.Count());
            Assert.Equal("Test", books.First().Title);
            Assert.Equal("Title", books.Last().Title);

        }

        private BookCatalog GetBookCatalog()
        {
            var books = new List<Book>();
            var catalog = new BookCatalog(books);
            catalog.AddBook("Title", "Author", "0000-000", "key, work, word", DateTime.Now, "Roman");
            catalog.AddBook("Title 2", "Author2","0200-000", "sea sky grass", DateTime.Now, "Poem");
            catalog.AddBook("Test", "Somebody", "1000-000", "goods product work", DateTime.Now, "Detective");
            return catalog;
        }
    }
}