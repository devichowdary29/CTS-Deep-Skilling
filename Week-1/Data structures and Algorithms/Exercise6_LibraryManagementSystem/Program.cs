using System;

namespace Exercise6_LibraryManagementSystem
{
    public class Book : IComparable<Book>
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        public int CompareTo(Book other)
        {
            return string.Compare(this.Title, other.Title, StringComparison.OrdinalIgnoreCase);
        }

        public override string ToString()
        {
            return $"[{BookId}] {Title} by {Author}";
        }
    }

    class Program
    {
        static Book LinearSearch(Book[] books, string title)
        {
            foreach (var book in books)
            {
                if (book.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                {
                    return book;
                }
            }
            return null;
        }

        static Book BinarySearch(Book[] books, string title)
        {
            int left = 0;
            int right = books.Length - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                int comparison = string.Compare(books[mid].Title, title, StringComparison.OrdinalIgnoreCase);

                if (comparison == 0) return books[mid];
                
                if (comparison < 0) left = mid + 1;
                else right = mid - 1;
            }

            return null;
        }

        static void Main(string[] args)
        {
            Book[] books = {
                new Book { BookId = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald" },
                new Book { BookId = 2, Title = "1984", Author = "George Orwell" },
                new Book { BookId = 3, Title = "To Kill a Mockingbird", Author = "Harper Lee" },
                new Book { BookId = 4, Title = "Pride and Prejudice", Author = "Jane Austen" }
            };

            Console.WriteLine("--- Linear Search ---");
            Book foundLinear = LinearSearch(books, "1984");
            Console.WriteLine(foundLinear != null ? $"Found: {foundLinear}" : "Not Found");

            Console.WriteLine("\n--- Binary Search ---");
            // Must sort before binary search
            Array.Sort(books);
            Console.WriteLine("(Books sorted by Title for Binary Search)");
            Book foundBinary = BinarySearch(books, "1984");
            Console.WriteLine(foundBinary != null ? $"Found: {foundBinary}" : "Not Found");
        }
    }
}
