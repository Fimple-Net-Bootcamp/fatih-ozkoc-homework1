using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryOtomationv2.Interfaces
{
    public interface IPrintible
    {
        void PrintBooks();
        void PrintMembers();
        void PrintBorrowedBooks();
    }
}
