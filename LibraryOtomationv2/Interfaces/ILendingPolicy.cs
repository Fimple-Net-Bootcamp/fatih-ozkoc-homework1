using LibraryOtomationv2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryOtomationv2.Interfaces
{
    public interface ILendingPolicy
    {
        // Ödünç verme politikasının adını tutan bir özellik.
        // Bu özellik, ödünç verme politikasının adını döndürür.
        string PolicyName { get; }

        // Bir üyeye kitap ödünç verme işlemini gerçekleştiren metot.
        // Bu metot, belirli bir üyeye ve kitaba göre ödünç verme işlemini tanımlar.
        void LendBook(Member member, Book book);

        // Ödünç verme süresini belirleyen metot.
        // Bu metot, belirli bir ödünç verme politikasına göre ödünç süresini (TimeSpan olarak) döndürür.
        TimeSpan GetReturnDuration();
    }
}
