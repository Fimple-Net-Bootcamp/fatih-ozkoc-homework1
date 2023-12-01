using LibraryOtomationv2.Entities;
using LibraryOtomationv2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryOtomationv2.Lending_Process
{
    public class LongTermLending : ILendingPolicy
    {
        public string PolicyName => "30 GÜN";

        // Ödünç verme süresini belirler. Bu politika için 30 gün.
        public TimeSpan GetReturnDuration()
        {
            return TimeSpan.FromDays(30);
        }

        // Kitabı belirtilen üyeye ödünç verir.
        public void LendBook(Member member, Book book)
        {
            // Üyenin ödünç aldığı kitaplar listesine, kitabı ve ilgili ödünç alma bilgilerini ekler.
            // Bu bilgiler kitabın ödünç alındığı tarihi ve kullanılan ödünç verme politikasını içerir.
            member.BorrowedBooks.Add(book, new LendingInfo(DateTime.Now, this));
        }
    }
}
