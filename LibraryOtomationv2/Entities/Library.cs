using LibraryOtomationv2.Abstract_Classes;
using LibraryOtomationv2.Interfaces;
using LibraryOtomationv2.Lending_Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryOtomationv2.Entities
{
    public enum DeleteType
    {
        Book,
        Member
    }
    public class Library:IPrintible
    {
        public List<Book> books = new List<Book>();
        public List<Member> members = new List<Member>();
        private ILendingPolicy lendingPolicy;

        public Library()
        {
            lendingPolicy = new ShortTermLending();
        }

        public void SetLendingPolicy(ILendingPolicy newPolicy)
        {
            lendingPolicy = newPolicy;
        }

        // Ödünç Verme işlemleri
        public void LendBook(Member member, Book book)
        {
            // Kitabın kütüphanede olup olmadığını kontrol et
            if (!books.Contains(book))
            {
                Console.WriteLine("Kitap kütüphanede mevcut değil.");
                return;
            }

            // Üyenin zaten bu kitabı ödünç alıp almadığını kontrol et
            if (member.BorrowedBooks.ContainsKey(book))
            {
                Console.WriteLine("Üye bu kitabı zaten ödünç almış.");
                return;
            }

            // Ödünç verme işlemini gerçekleştir
            lendingPolicy.LendBook(member, book);

            // Kitabın kütüphanedeki durumunu güncelle (örneğin, mevcut kitap listesinden çıkar)
            books.Remove(book);
        }

        // Ekleme İşlemleri
        public void AddProcess(object item)
        {
            if (item is Book book)
            {
                books.Add(book);
            }
            else if (item is Member member)
            {
                members.Add(member);
            }
            else
            {
                throw new ArgumentException("Item type is not supported.");
            }
        }

        // Silme işlemleri
        public void DeleteProcess(int id, DeleteType deleteType)
        {
            if (deleteType == DeleteType.Book)
            {
                books.RemoveAll(b => b.BookID == id);
            }
            else if (deleteType == DeleteType.Member)
            {
                members.RemoveAll(m => m.MembershipNumber == id);
            }
            else
            {
                throw new ArgumentException("Item type is not supported.");
            }
        }

        // Kitapları ekrana yazdırmak için kullanılan fonksiyon.
        public void PrintBooks()
        {
            Console.WriteLine("{0,-10} {1,-30} {2,-25} {3,-15}", "ID", "KİTAP İSMİ", "YAZAR", "YAYIM YILI");
            Console.WriteLine("{0,-10} {1,-30} {2,-25} {3,-15}", "**", "*****", "******", "***********");
            foreach (var book in books)
            {
                Console.WriteLine("{0,-10} {1,-30} {2,-25} {3,-15}", book.BookID, book.Title, book.Author, book.PublicationYear);
            }
        }

        // Üyeleri ekrana yazdırmak için kullanılan fonksiyon.
        public void PrintMembers()
        {
            Console.WriteLine("{0,-10} {1,-30} {2,-25}", "ID", "İSİM", "SOYİSİM");
            Console.WriteLine("{0,-10} {1,-30} {2,-25}", "**", "*****", "******");
            foreach (var member in members)
            {
                Console.WriteLine("{0,-10} {1,-30} {2,-25}", member.MembershipNumber ,member.FirstName, member.LastName);
            }
        }

        // Ödünç Alınan Kitapaları yazdırmak için kullanılan fonksiyon
        public void PrintBorrowedBooks()
        {
            Console.WriteLine("{0,-20} {1,-15} {2,-25} {3,-15} {4,-5}", "ÜYE", "KİTAP", "TARİH", "SÜRE", "TESLİM TARİHİ");
            foreach (var member in members)
            {
                foreach (var entry in member.BorrowedBooks)
                {
                    string name=member.FirstName+" "+member.LastName;
                    Console.WriteLine("{0,-20} {1,-15} {2,-25} {3,-15} {4,-5}", name, entry.Key.Title, entry.Value.BorrowedDate, entry.Value.LendingPolicy.PolicyName,entry.Value.ReturnDate);
                }
            }
        }
    }
}
