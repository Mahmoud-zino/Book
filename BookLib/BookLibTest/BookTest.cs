using ElZino.Models.BookLib;
using System;
using System.Collections.Generic;
using Xunit;

namespace BookLibTest
{
    public class BookTest
    {
        public static IEnumerable<object[]> BookDataPass =>
            new List<object[]>
            {
                new object[] { true, "To Kill a Mockingbird", "0136019706", new Author("Harper","Lee")},
                new object[] { true, "The Great Gatsby", "0198534531", new Author("Scott", "Fitzgerald") },
                new object[] { false, "One Hundred Years of Solitude", "0137239254", new Author("Gabriel", "Márquez") },
                new object[] { false, "A Passage to India", "0575717807", new Author("E.M.", "Forster") },
            };

        [Theory]
        [MemberData(nameof(BookDataPass))]
        public void CreateConstructor_Passing(bool withConst, string title, string isbn, Author author)
        {
            Book book;
            if (withConst)
                book = new Book(title, isbn, author);
            else
                book = new()
                {
                    Title = title,
                    ISBN = isbn,
                    Author = author
                };

            Assert.NotNull(book);
            Assert.Equal(title, book.Title);
            Assert.Equal(isbn, book.ISBN);
            Assert.Equal(author.FirstName, book.Author.FirstName);
            Assert.Equal(author.LastName, book.Author.LastName);
        }

        public static IEnumerable<object[]> BookDataFailing_NULL =>
            new List<object[]>
            {
                new object[] { true, string.Empty, "0136019706", new Author("Harper","Lee")},
                new object[] { true, "The Great Gatsby", string.Empty, new Author("Scott", "Fitzgerald") },
                new object[] { true, string.Empty, string.Empty, new Author("Harper","Lee")},
                new object[] { false, string.Empty, "0136019706", new Author("Harper","Lee")},
                new object[] { false, "The Great Gatsby", string.Empty, new Author("Scott", "Fitzgerald") },
                new object[] { true, string.Empty, string.Empty, new Author("Harper","Lee")},
            };

        [Theory]
        [MemberData(nameof(BookDataFailing_NULL))]
        public void CreateConstructor_Failing(bool withConst, string title, string isbn, Author author)
        {
            if (withConst)
            {
                Book book;
                Assert.Throws<ArgumentNullException>(() => book = new(title, isbn, author));
            }
            else
            {
                Book book = new();

                Assert.Throws<ArgumentNullException>(() =>
                {
                    book.Title = title;
                    book.ISBN = isbn;
                });
            }
        }
        public static IEnumerable<object[]> ISBN_Invalid =>
            new List<object[]>
            {
                new object[] { true, "To Kill a Mockingbird", "013601976X", new Author("Harper","Lee")},
                new object[] { true, "To Kill a Mockingbird", "013601906", new Author("Harper","Lee")},
                new object[] { true, "To Kill a Mockingbird", "0136501906", new Author("Harper","Lee")},
                new object[] { true, "To Kill a Mockingbird", "01360196y", new Author("Harper","Lee")},
                new object[] { false, "To Kill a Mockingbird", "013601970a", new Author("Harper","Lee")},
                new object[] { false, "To Kill a Mockingbird", "0146019706", new Author("Harper","Lee")},
                new object[] { false, "To Kill a Mockingbird", "1146019706", new Author("Harper","Lee")},
            };

        [Theory]
        [MemberData(nameof(ISBN_Invalid))]
        public void CreateConstructor_ISBN_Failing(bool withConst, string title, string isbn, Author author)
        {
            if (withConst)
            {
                Book book;
                Assert.Throws<ArgumentException>(() => book = new(title, isbn, author));
            }
            else
            {
                Book book = new();

                Assert.Throws<ArgumentException>(() =>
                {
                    book.Title = title;
                    book.ISBN = isbn;
                });
            }
        }

        public static IEnumerable<object[]> ToString_Pass =>
            new List<object[]>
            {
                new object[] {"To Kill a Mockingbird", "0136019706", new Author("Harper","Lee")},
                new object[] {"The Great Gatsby", "0198534531", new Author("Scott", "Fitzgerald") },
                new object[] {"One Hundred Years of Solitude", "0137239254", new Author("Gabriel", "Márquez") },
                new object[] {"A Passage to India", "0575717807", new Author("E.M.", "Forster") },
            };

        [Theory]
        [MemberData(nameof(ToString_Pass))]
        public void ToString_Passing(string title, string isbn, Author author)
        {
            Book book = new(title, isbn, author);

            Assert.Equal($"{title} By {author} ({isbn})", book.ToString());
        }
    }
}
