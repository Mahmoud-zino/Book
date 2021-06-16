using System;

namespace ElZino.Models.BookLib
{
    public class Book
    {
        public Book(string title, string isbn, Author author)
        {
            this.Title = title;
            this.ISBN = isbn;
            this.Author = author;
        }

        public Book() { }

        private string title;
        public string Title
        {
            get => title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(nameof(Title));
                title = value;
            }
        }
        private string isbn;
        public string ISBN
        {
            get => isbn;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(nameof(ISBN));
                if (!IsValidISBN(value))
                    throw new ArgumentException($"Invalid {nameof(ISBN)}");
                isbn = value;
            }
        }

        public Author Author { get; set; }


        private static bool IsValidISBN(string isbn)
        {
            if (isbn.Length != 10)
                return false;

            int sum = 0;
            for (int i = 0; i < 9; i++)
            {
                int digit = isbn[i] - '0';

                sum += (digit * (10 - i));
            }

            if (isbn[9] != 'X' && (isbn[9] < '0' || isbn[9] > '9'))
                return false;

            sum += ((isbn[9] == 'X') ? 10 : (isbn[9] - '0'));

            return (sum % 11 == 0);
        }

        public override string ToString()
        {
            return $"{this.Title} By {this.Author} ({this.ISBN})";
        }

    }
}
