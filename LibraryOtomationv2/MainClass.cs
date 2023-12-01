using LibraryOtomationv2.Entities;
using LibraryOtomationv2.Interfaces;
using LibraryOtomationv2.Lending_Process;
using LibraryOtomationv2.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibraryOtomationv2
{
    public class MainClass
    {
        static ILendingPolicy longTermPolicy = new LongTermLending();
        static ILendingPolicy shortTermPolicy = new ShortTermLending();

        private static void Main(string[] args)
        {
            Library library = new Library();
            Processes processes = new Processes();

            bool running = true;
            while (running)
            {
                Console.WriteLine("Otomasyon Sistemi");
                Console.WriteLine("1. Kitap İşlemleri");
                Console.WriteLine("2. Üye İşlemleri");
                Console.WriteLine("3. Ödünç Verme İşlemleri");
                Console.WriteLine("X. Çıkış");

                Console.Write("Lütfen bir işlem seçin: ");
                string input = Console.ReadLine();

                switch (input.ToUpper())
                {
                    case "1":
                        Console.Clear();
                        Processes.BookProcesses(library);
                        break;
                    case "2":
                        Console.Clear();
                        Processes.MemberProcesses(library);
                        break;
                    case "3":
                        Console.Clear();
                        Processes.LendingProcess(library);
                        break;
                    case "X":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
                        break;
                }
            }
        }
    }
}
