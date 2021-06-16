using System;

namespace ElZino.Models.BookLib
{
    public class Author
    {
        public Author(string fname, string lname)
        {
            this.FirstName = fname;
            this.LastName = lname;
        }
        public Author() { }
        private string firstName;
        private string lastName;
        public string FirstName
        {
            get => firstName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(nameof(FirstName));
                firstName = value;
            }
        }
        public string LastName
        {
            get => lastName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(nameof(LastName));
                lastName = value;
            }
        }
        public override string ToString()
        {
            return $"{this.FirstName} {this.LastName}";
        }
    }
}
