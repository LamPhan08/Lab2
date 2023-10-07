using ODataBookStore.Models;

namespace ODataBookStore
{
    public class DataSource
    {
        private static IList<Book> listBooks { get; set; }

        public static IList<Book> GetBooks()
        {
            if (listBooks != null) 
            {
                return listBooks;
            }

            listBooks = new List<Book>();
            Book book = new Book
            {
                Id = 1,
                ISBN = "978-0-321-87758-1",
                Title = "Essential C#5.0",
                Author = "Mark Michaelis",
                Price = 59.99m,
                Location = new Address
                {
                    City = "Ho Chi Minh City",
                    Street = "D2, Thu Duc City"
                },
                Press = new Press
                {
                    Id = 1,
                    Name = "Addison-Wesley",
                    Category = Category.Book,
                }
            };

            listBooks.Add(book);

            return listBooks;
        }
    }
}
