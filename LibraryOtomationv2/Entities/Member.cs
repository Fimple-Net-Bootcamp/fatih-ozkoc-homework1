using LibraryOtomationv2.Lending_Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryOtomationv2.Entities
{
    public class Member
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int MembershipNumber { get; set; }   
        public Dictionary<Book, LendingInfo> BorrowedBooks { get; set; }

        public Member(string firstName, string lastName, int membershipNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            MembershipNumber = membershipNumber;
            BorrowedBooks = new Dictionary<Book, LendingInfo>();
        }
    }
}
