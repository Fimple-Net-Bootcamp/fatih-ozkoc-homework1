using LibraryOtomationv2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryOtomationv2.Manager
{
    public class Processes
    {
        public static void BookProcesses(Library library)
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("Yapmak İstediğiniz İşlemi Seçin");

                Console.WriteLine("1. Kitap Ekle");
                Console.WriteLine("2. Kitap Sil");
                Console.WriteLine("3. Kitapları Listele");
                Console.WriteLine("X. Çıkış");

                string input = Console.ReadLine();

                switch (input.ToUpper())
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Lütfen Eklenecek Üt Bilgilerini Giriniz");
                        LibraryManager.AddBook(library);
                        Console.Clear();
                        break;
                    case "2":
                        Console.Clear();
                        library.PrintBooks();
                        Console.WriteLine();
                        LibraryManager.DeleteBook(library);
                        Console.Clear();
                        break;
                    case "3":
                        Console.Clear();
                        library.PrintBooks();
                        Console.WriteLine();
                        break;
                    case "X":
                        Console.Clear();
                        running = false;
                        //GetBack();
                        break;
                    default:
                        Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
                        break;
                }
            }
        }

        public static void MemberProcesses(Library library)
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("Yapmak İstediğiniz İşlemi Seçin");

                Console.WriteLine("1. Üye Ekle");
                Console.WriteLine("2. Üye Sil");
                Console.WriteLine("3. Üyeleri Listele");
                Console.WriteLine("X. Çıkış");

                string input = Console.ReadLine();

                switch (input.ToUpper())
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Lütfen Eklenecek üye  Bilgilerini Giriniz");
                        LibraryManager.AddMember(library);
                        Console.Clear();
                        break;
                    case "2":
                        Console.Clear();
                        library.PrintMembers();
                        Console.WriteLine();
                        LibraryManager.DeleteMember(library);
                        Console.Clear();
                        break;
                    case "3":
                        Console.Clear();
                        library.PrintMembers();
                        Console.WriteLine();
                        break;
                    case "X":
                        Console.Clear();
                        running = false;
                        //GetBack();
                        break;
                    default:
                        Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
                        break;
                }
            }
        }

        public static void LendingProcess(Library library)
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("1. Ödünç Ver");
                Console.WriteLine("2. Ödünç Verilen Kitapları Listele");
                Console.WriteLine("X. Çıkış");

                Console.Write("Seçiminiz: ");
                string choice = Console.ReadLine();

                switch (choice.ToUpper())
                {
                    case "1":
                        Console.Clear();
                        LibraryManager.LendBook(library);
                        Console.Clear();
                        break;
                    case "2":
                        Console.Clear();
                        library.PrintBorrowedBooks();
                        Console.WriteLine();
                        //Console.Clear();
                        break;
                    case "X":
                        Console.Clear();
                        running = false;
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Geçersiz seçim.");
                        break;
                }
            }
        }

    }
}
