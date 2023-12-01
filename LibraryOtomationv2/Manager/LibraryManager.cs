using LibraryOtomationv2.Entities;
using LibraryOtomationv2.Interfaces;
using LibraryOtomationv2.Lending_Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryOtomationv2.Manager
{
    public class LibraryManager
    {
        static ILendingPolicy longTermPolicy = new LongTermLending();
        static ILendingPolicy shortTermPolicy = new ShortTermLending();
        private Library library;

        public LibraryManager(Library library)
        {
            this.library = library;
        }

        public static void LendBook(Library library)
        {
            // Kütüphanedeki kitapları listele
            library.PrintBooks();
            int bookID;
            Book book = null;
            bool inputIsValid;

            // Kitap ID'sini doğru formatta al
            do
            {
                Console.Write("Ödünç Verilecek Kitap ID: ");
                inputIsValid = int.TryParse(Console.ReadLine(), out bookID);
                if (!inputIsValid)
                {
                    Console.WriteLine("Geçersiz Kitap ID. Lütfen bir sayı giriniz.");
                    continue;
                }

                book = library.books.FirstOrDefault(b => b.BookID == bookID);
                if (book == null)
                {
                    Console.WriteLine("Kitap bulunamadı. Lütfen geçerli bir Kitap ID giriniz.");
                    inputIsValid = false;
                }
            } while (!inputIsValid);

            Console.Clear();

            // Kütüphanedeki üyeleri listele
            library.PrintMembers();
            int memberNumber;
            Member member = null;

            // Üye numarasını doğru formatta al
            do
            {
                Console.Write("Ödünç Alacak Üye Numarası: ");
                inputIsValid = int.TryParse(Console.ReadLine(), out memberNumber);
                if (!inputIsValid)
                {
                    Console.WriteLine("Geçersiz Üye Numarası. Lütfen bir sayı giriniz.");
                    continue;
                }

                member = library.members.FirstOrDefault(m => m.MembershipNumber == memberNumber);
                if (member == null)
                {
                    Console.WriteLine("Üye bulunamadı. Lütfen geçerli bir Üye Numarası giriniz.");
                    inputIsValid = false;
                }
            } while (!inputIsValid);

            Console.Clear();

            int policy;
            ILendingPolicy chosenPolicy = null;

            // Ödünç verme politikasını seç
            do
            {
                Console.WriteLine("Ödünç Verme Süresini Giriniz: 1- Kısa Dönem || 2- Uzun Dönem");
                inputIsValid = int.TryParse(Console.ReadLine(), out policy);
                if (!inputIsValid || (policy != 1 && policy != 2))
                {
                    Console.WriteLine("Lütfen Belirtilen Seçimlerden Birini Seçiniz!");
                    inputIsValid = false;
                    continue;
                }

                chosenPolicy = policy == 1 ? shortTermPolicy : longTermPolicy;
            } while (!inputIsValid);

            library.SetLendingPolicy(chosenPolicy);

            try
            {
                // Kitabı ödünç ver
                library.LendBook(member, book);
                Console.WriteLine("Kitap başarıyla ödünç verildi.");
            }
            catch (ArgumentException ex)
            {
                // LendBook metodu tarafından fırlatılan ArgumentException'ları yakala ve hata mesajını yazdır
                Console.WriteLine(ex.Message);
            }
        }
        public static void AddBook(Library library)
        {
            int bookID, publicationYear;
            string title, author;
            bool inputIsValid;

            // Kitap ID'sini doğru formatta al
            do
            {
                Console.Write("Kitap ID: ");
                inputIsValid = int.TryParse(Console.ReadLine(), out bookID);
                if (!inputIsValid)
                {
                    Console.WriteLine("Geçersiz Kitap ID. Lütfen bir sayı giriniz.");
                }
            } while (!inputIsValid);

            // Kitap başlığını al
            do
            {
                Console.Write("Kitap Başlığı: ");
                title = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(title))
                {
                    Console.WriteLine("Geçerli bir kitap başlığı giriniz.");
                }
            } while (string.IsNullOrWhiteSpace(title));

            // Yazar adını al
            do
            {
                Console.Write("Yazar: ");
                author = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(author))
                {
                    Console.WriteLine("Geçerli bir yazar adı giriniz.");
                }
            } while (string.IsNullOrWhiteSpace(author));

            // Yayın yılını doğru formatta al
            do
            {
                Console.Write("Yayın Yılı: ");
                inputIsValid = int.TryParse(Console.ReadLine(), out publicationYear);
                if (!inputIsValid)
                {
                    Console.WriteLine("Geçersiz Yayın Yılı. Lütfen bir sayı giriniz.");
                }
            } while (!inputIsValid);

            try
            {
                // Yeni kitap oluştur ve kütüphaneye ekle
                Book newBook = new Book(title, author, publicationYear, bookID);
                library.AddProcess(newBook);
                Console.WriteLine("Kitap başarıyla eklendi.");
            }
            catch (ArgumentException ex)
            {
                // Ekleme işlemi sırasında bir hata oluşursa (örneğin, ID geçersizse) hata mesajını yazdır
                Console.WriteLine(ex.Message);
            }
        }
        public static void DeleteMember(Library library)
        {
            int membershipNumber;
            bool inputIsValid;

            do
            {
                Console.Write("Silinecek Üye Numarası: ");

                // Girişi bir tamsayıya dönüştürmeyi dene. Başarılı olursa inputIsValid true, aksi takdirde false olur.
                inputIsValid = int.TryParse(Console.ReadLine(), out membershipNumber);

                // Girdi geçerli bir tamsayı değilse kontrol et
                if (!inputIsValid)
                {
                    Console.WriteLine("Geçersiz Üye Numarası Girişi! Tekrar Giriniz!");
                }
                // Üye numarasının sistemde kayıtlı olmadığını kontrol et
                else if (!library.members.Any(m => m.MembershipNumber == membershipNumber))
                {
                    Console.WriteLine("Üye numarası sistemde kayıtlı değil. Tekrar deneyin.");
                    inputIsValid = false;
                }
            } while (!inputIsValid); // Geçerli ve kayıtlı bir üye numarası girilene kadar döngüyü sürdür

            try
            {
                // Verilen üye numarasına sahip üyeyi silmeye çalış
                library.DeleteProcess(membershipNumber, DeleteType.Member);
                Console.WriteLine("Üye başarıyla silindi.");
            }
            catch (ArgumentException ex)
            {
                // DeleteProcess metodu tarafından fırlatılan ArgumentException'ları yakala ve hata mesajını yazdır
                Console.WriteLine(ex.Message);
            }

        }
        public static void AddMember(Library library)
        {
            string firstName, lastName;
            int membershipNumber;

            // Kullanıcıdan üye adını al
            do
            {
                Console.Write("Üye Adı: ");
                firstName = Console.ReadLine();
                // Ad boş ise uyarı ver ve tekrar dene
                if (string.IsNullOrWhiteSpace(firstName))
                {
                    Console.WriteLine("Geçerli bir ad giriniz.");
                }
            } while (string.IsNullOrWhiteSpace(firstName)); // Geçerli bir ad girilene kadar bu döngüyü tekrarla

            // Kullanıcıdan üye soyadını al
            do
            {
                Console.Write("Üye Soyadı: ");
                lastName = Console.ReadLine();
                // Soyad boş ise uyarı ver ve tekrar dene
                if (string.IsNullOrWhiteSpace(lastName))
                {
                    Console.WriteLine("Geçerli bir soyad giriniz.");
                }
            } while (string.IsNullOrWhiteSpace(lastName)); // Geçerli bir soyad girilene kadar bu döngüyü tekrarla

            // Kullanıcıdan üyelik numarasını al
            bool inputIsValid;
            do
            {
                Console.Write("Üyelik Numarası: ");
                // Girişi int'e çevirmeyi dene, başarılı olursa inputIsValid true olur
                inputIsValid = int.TryParse(Console.ReadLine(), out membershipNumber);
                // Giriş int'e çevrilemezse uyarı ver
                if (!inputIsValid)
                {
                    Console.WriteLine("Geçersiz üyelik numarası. Lütfen bir sayı giriniz.");
                }
            } while (!inputIsValid); // Geçerli bir üyelik numarası girilene kadar bu döngüyü tekrarla

            try
            {
                // Yeni üye oluştur ve kütüphaneye ekle
                Member newMember = new Member(firstName, lastName, membershipNumber);
                library.AddProcess(newMember);
                Console.WriteLine("Üye başarıyla eklendi.");
            }
            catch (ArgumentException ex)
            {
                // Ekleme işlemi sırasında bir hata oluşursa (örneğin, ID geçersizse) hata mesajını yazdır
                Console.WriteLine(ex.Message);
            }

        }
        public static void DeleteBook(Library library)
        {
            int bookID;
            bool inputIsValid;

            // Kitap ID'sini doğru formatta al
            do
            {
                Console.Write("Silinecek Kitap ID: ");
                inputIsValid = int.TryParse(Console.ReadLine(), out bookID);
                if (!inputIsValid)
                {
                    // Eğer giriş sayısal bir değer değilse uyarı ver
                    Console.WriteLine("Geçersiz Kitap ID. Lütfen bir sayı giriniz.");
                }
                else if (!library.books.Any(m => m.BookID == bookID))
                {
                    Console.WriteLine("Kitap numarası sistemde kayıtlı değil. Tekrar deneyin.");
                    inputIsValid = false;
                }
            } while (!inputIsValid); // Geçerli bir kitap ID'si girilene kadar bu döngüyü tekrarla

            try
            {
                // Girilen ID ile kitabı silmeye çalış
                library.DeleteProcess(bookID, DeleteType.Book);
                // Kitabın başarıyla silindiğini bildir
                Console.WriteLine("Kitap başarıyla silindi.");
            }
            catch (ArgumentException ex)
            {
                // Silme işlemi sırasında bir hata oluşursa (örneğin, ID geçersizse) hata mesajını yazdır
                Console.WriteLine(ex.Message);
            }

        }
    }
}
