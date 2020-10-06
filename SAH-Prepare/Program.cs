using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SAH_Prepare
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welkom");
            Console.WriteLine("Dit is de SAH Prepare tool!");
            Console.WriteLine("Gemaakt door Justin Pooters");
            Console.WriteLine("Typ `ja` om door te gaan of typ `nee` om te stoppen.");
            string yesno = Console.ReadLine();
            if (yesno == "nee")
            {
                Console.WriteLine("Druk op Enter om af te sluiten");
                Console.ReadLine();
            } 
            else
            {
                Console.WriteLine("De installatie kan nu beginnen");
                Console.WriteLine("Kies een van de volgende opties");
                Console.WriteLine("1: Full install, dit installeert: ThunderBird, CCleaner, Malwarebytes, Avast anti-virus en FireFox web browser");
                Console.WriteLine("2: AntiVirus, dit installeert Malwarebytes en Avast anti-virus");
                Console.WriteLine("3: Standaard, Dit installeert Firefox en ThunderBird");
                Console.WriteLine("Kis 1, 2 of 3");
                string gekozennummer = Console.ReadLine();
                if(gekozennummer == "1")
                {
                    Console.WriteLine("Wil je avast ook installeren?");
                    Console.WriteLine("Kies: ja of nee (*GEEN HOOFDLETTERS*)");
                    if (Console.ReadLine() == "ja")
                    {
                        //avast

                        Console.WriteLine("Starting avast download");
                        DownloadAndExecute("https://cdn.thatgeek.dev/sah/antivirus.exe", "antivirus.exe");

                        // MalwareBytes

                        Console.WriteLine("Starting MalewareBytes download");
                        DownloadAndExecute("https://cdn.thatgeek.dev/sah/malwarebytes.exe", "malwarebytes.exe");

                        Console.WriteLine("FireFox & Thunderbird are next! :D");
                        Console.WriteLine("Wie wil er nog internet explorer / edge gebruiken 🤮");


                        //Firefox

                        Console.WriteLine("Starting firefox download");
                        DownloadAndExecute("https://cdn.thatgeek.dev/sah/firefox.exe", "firefox.exe");

                        //Thunderbird

                        Console.WriteLine("Starting thunderbird download");
                        DownloadAndExecute("https://cdn.thatgeek.dev/sah/thunderbird.exe", "thunderbird.exe");

                        Console.WriteLine("Extra handy dandy tools ;P");


                        // CCleaner

                        Console.WriteLine("Starting ccleaner download");
                        DownloadAndExecute("https://cdn.thatgeek.dev/sah/ccleaner.exe", "ccleaner.exe");

                    }
                    else
                    {
                        // MalwareBytes

                        Console.WriteLine("Starting MalewareBytes download");
                        DownloadAndExecute("https://cdn.thatgeek.dev/sah/malwarebytes.exe", "malwarebytes.exe");

                        Console.WriteLine("FireFox & Thunderbird are next! :D");
                        Console.WriteLine("Wie wil er nog internet explorer / edge gebruiken 🤮");


                        //Firefox

                        Console.WriteLine("Starting firefox download");
                        DownloadAndExecute("https://cdn.thatgeek.dev/sah/firefox.exe", "firefox.exe");

                        //Thunderbird

                        Console.WriteLine("Starting thunderbird download");
                        DownloadAndExecute("https://cdn.thatgeek.dev/sah/thunderbird.exe", "thunderbird.exe");

                        Console.WriteLine("Extra handy dandy tools ;P");


                        // CCleaner

                        Console.WriteLine("Starting ccleaner download");
                        DownloadAndExecute("https://cdn.thatgeek.dev/sah/ccleaner.exe", "ccleaner.exe");

                    }
                }
                if (gekozennummer == "2")
                {
                    Console.WriteLine("Cool! Antivirus, safety first ;)");
                    
                    // Avast    

                    Console.WriteLine("Starting avast download");
                    DownloadAndExecute("https://cdn.thatgeek.dev/sah/antivirus.exe", "antivirus.exe");

                    // MalwareBytes

                    Console.WriteLine("Starting malwarebytes download");
                    DownloadAndExecute("https://cdn.thatgeek.dev/sah/malwarebytes.exe", "malwarebytes.exe");

                    // CCleaner

                    Console.WriteLine("Starting ccleaner download");
                    DownloadAndExecute("https://cdn.thatgeek.dev/sah/ccleaner.exe", "ccleaner.exe");

                }

                if (gekozennummer == "3")
                {
                    Console.WriteLine("Cool! internet en email zijn belangerijk hoor :P");

                    //Firefox

                    Console.WriteLine("Starting Browser download");
                    DownloadAndExecute("https://cdn.thatgeek.dev/sah/firefox.exe", "firefox.exe");

                    //Thunderbird
                    Console.WriteLine("Starting Thunderbird download");
                    DownloadAndExecute("https://cdn.thatgeek.dev/sah/thunderbird.exe", "thunderbird.exe");

                }
            }
        }
        private static void DownloadAndExecute(string url, string outputFileName)
        {
            AutoResetEvent are = new AutoResetEvent(false);
            var client = new WebClient();
            client.DownloadFileCompleted += new AsyncCompletedEventHandler((sender, args) =>
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), outputFileName);
                    System.Diagnostics.Process.Start(path);
                    are.Set();
                }); 
                
            client.DownloadFileAsync(new Uri(url), outputFileName);

            are.WaitOne();
            client.Dispose();
        }

    }
}
