using ElZino.Models.BookLib;
using System;
using System.Collections.Generic;
using Xunit;

namespace BookLibTest
{
    public class AuthorTest
    {
        public static IEnumerable<object[]> AuthorDataPass =>
            new List<object[]>
            {
                new object[] { true, "Max","Mustermann"},
                new object[] { true, "Alice","MusterFrau"},
                new object[] { false, "Max","Mustermann" },
                new object[] { false, "Alice","MusterFrau"}
            };

        [Theory]
        [MemberData(nameof(AuthorDataPass))]
        public void CreateConstructor_Passing(bool withConst, string fname, string lname)
        {
            Author author;
            if (withConst)
                author = new(fname, lname);
            else
                author = new()
                {
                    FirstName = fname,
                    LastName = lname
                };

            //Test
            Assert.NotNull(author);
            Assert.Equal(fname, author.FirstName);
            Assert.Equal(lname, author.LastName);
        }

        public static IEnumerable<object[]> AuthorDataFail =>
           new List<object[]>
           {
                new object[] { true, string.Empty,"Mustermann"},
                new object[] { true, "Alice",string.Empty},
                new object[] { true, string.Empty, string.Empty},
                new object[] { false, string.Empty,"Mustermann"},
                new object[] { false, "Alice",string.Empty},
                new object[] { false, string.Empty, string.Empty}
           };

        [Theory]
        [MemberData(nameof(AuthorDataFail))]
        public void CreateConstructor_Failing(bool withConst, string fname, string lname)
        {
            if (withConst)
            {
                Author author;
                Assert.Throws<ArgumentNullException>(() => author = new(fname, lname));
            }
            else
            {
                Author author = new();

                Assert.Throws<ArgumentNullException>(() =>
                {
                    author.FirstName = fname;
                    author.LastName = lname;
                });
            }
        }

        public static IEnumerable<object[]> ToStringPass =>
           new List<object[]>
           {
                new object[] {"Max","Mustermann"},
                new object[] {"Alice","MusterFrau"},
                new object[] {"Mahmoud","Zino"},
                new object[] {"Simon","Masooglu"},
                new object[] {"yay","WOW"},
           };

        [Theory]
        [MemberData(nameof(ToStringPass))]
        public void ToString_Passing(string fname, string lname)
        {
            Author authro = new(fname, lname);

            Assert.Equal($"{fname} {lname}", authro.ToString());
        }
    }
}
