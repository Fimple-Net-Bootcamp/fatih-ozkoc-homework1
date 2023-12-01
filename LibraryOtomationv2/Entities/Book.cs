using LibraryOtomationv2.Abstract_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryOtomationv2.Entities
{
    public class Book:WrittenWork
    {
        public int BookID { get; private set; }
        public Book(string title, string author, int publicationYear, int bookID)
            : base(title, author, publicationYear)
        {
            BookID = bookID;
        }
    }
}
