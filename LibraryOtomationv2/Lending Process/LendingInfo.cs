using LibraryOtomationv2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryOtomationv2.Lending_Process
{
    public class LendingInfo
    {
        // LendingInfo sınıfı, bir kütüphane sistemindeki ödünç verme işlemiyle ilgili bilgileri tutmak ve yönetmek için bir veri yapısı olarak hizmet eder.

        // Kitabın ödünç alındığı tarih.
        public DateTime BorrowedDate { get; set; }

        // Kitabın ödünç alındığı politikayı belirten ILendingPolicy arayüzü.
        public ILendingPolicy LendingPolicy { get; set; }

        // Kitabın geri dönüş tarihi. Sadece okunabilir, dışarıdan değiştirilemez.
        public DateTime ReturnDate { get; private set; }

        // LendingInfo nesnesinin yapıcı metodu.
        // Bu metod, ödünç alınan tarih ve ödünç verme politikası alır.
        public LendingInfo(DateTime borrowedDate, ILendingPolicy lendingPolicy)
        {
            BorrowedDate = borrowedDate; // Ödünç alınan tarih atanıyor.
            LendingPolicy = lendingPolicy; // Ödünç verme politikası atanıyor.
            ReturnDate = borrowedDate + lendingPolicy.GetReturnDuration(); // Geri dönüş tarihi, ödünç alma süresine göre hesaplanıyor.
        }
    }
}
