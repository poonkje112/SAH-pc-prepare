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
			Console.WriteLine("Contributors:");
			Console.WriteLine("Aaron Knoop");

			string choice = null;

			while (string.IsNullOrEmpty(choice) || !ContainsAny(choice, new[] {"j", "n"}))
			{
				Console.WriteLine("Typ `ja` om door te gaan of typ `nee` om te stoppen.");
				choice = Console.ReadLine();
				Console.Clear();
			}

			if (choice.ToLower().Contains("n"))
			{
				Console.WriteLine("Druk op Enter om af te sluiten");
				Console.ReadLine();
			}
			else
			{
				Console.WriteLine("De installatie kan nu beginnen");
				Console.WriteLine("Kies een van de volgende opties");
				Console.WriteLine(
					"1: Full install, dit installeert: ThunderBird, CCleaner, Malwarebytes, Avast anti-virus en FireFox web browser");
				Console.WriteLine("2: AntiVirus, dit installeert Malwarebytes en Avast anti-virus");
				Console.WriteLine("3: Standaard, Dit installeert Firefox en ThunderBird");
				Console.WriteLine("Kis 1, 2 of 3");

				int installType = -1;

				while (installType == -1)
				{
					if (Int32.TryParse(Console.ReadLine(), out installType))
					{
						if (installType > 3 || installType < 1)
						{
							installType = -1;
						}
					}

					if (installType == -1)
					{
						Console.WriteLine("Ongeldige Input!");
					}
				}

				Console.Clear();

				bool installAvast = false;

				if (installType == 1)
				{
					Console.WriteLine("Wil je avast ook installeren?");
					Console.WriteLine("Kies: ja of nee");
					string input = Console.ReadLine();

					while (string.IsNullOrEmpty(input) || !ContainsAny(input, new[] {"j", "n"}))
					{
						Console.WriteLine("Ongeldige input!");
						input = Console.ReadLine();
					}

					installAvast = input.ToLower().Contains("j");
				}

				if (installType < 3)
				{
					if (installAvast || installType == 2)
					{
						//avast

						Console.WriteLine("Starting avast download");
						DownloadAndExecute("https://cdn.thatgeek.dev/sah/antivirus.exe", "antivirus.exe");
					}

					// MalwareBytes

					Console.WriteLine("Starting MalewareBytes download");
					DownloadAndExecute("https://cdn.thatgeek.dev/sah/malwarebytes.exe", "malwarebytes.exe");
				}


				if (installType != 2)
				{
					//Firefox

					Console.WriteLine("Starting firefox download");
					DownloadAndExecute("https://cdn.thatgeek.dev/sah/firefox.exe", "firefox.exe");

					//Thunderbird

					Console.WriteLine("Starting thunderbird download");
					DownloadAndExecute("https://cdn.thatgeek.dev/sah/thunderbird.exe", "thunderbird.exe");


					if (installType == 1)
					{
						// CCleaner

						Console.WriteLine("Starting ccleaner download");
						DownloadAndExecute("https://cdn.thatgeek.dev/sah/ccleaner.exe", "ccleaner.exe");
					}
				}
			}
		}

		private static void DownloadAndExecute(string url, string outputFileName)
		{
#if !DEBUG
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
#endif
		}

		private static bool ContainsAny(string target, string[] chars)
		{
			foreach (string character in chars)
			{
				if (target.Contains(character)) return true;
			}

			return false;
		}
	}
}