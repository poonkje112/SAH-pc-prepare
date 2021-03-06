﻿using System;
using System.Diagnostics;
using System.Security.Principal;

namespace SAH_Prepare
{
	class Program
	{
		static void Main(string[] args)
		{
			if (!IsAdmin())
			{
				ProcessStartInfo asAdmin = new ProcessStartInfo(System.Reflection.Assembly.GetEntryAssembly().Location);
				asAdmin.UseShellExecute = true;
				asAdmin.Verb = "runas";
				Process.Start(asAdmin);
				return;
			}
			
			Console.WriteLine("Welkom");
			Console.WriteLine("Dit is de SAH Prepare tool!");
			Console.WriteLine("Gemaakt door Justin Pooters");
			Console.WriteLine("\nContributors:");
			Console.WriteLine("Aaron Knoop");
			Console.WriteLine();

			string choice = null;

			while (string.IsNullOrEmpty(choice) || !ContainsAny(choice, new[] {"j", "n"}))
			{
				Console.WriteLine("Typ `ja` om door te gaan of typ `nee` om te stoppen.");
				choice = Console.ReadLine();
				Console.Clear();
			}
			
			if(choice.ToLower().Contains("j"))
			{
				Console.WriteLine("De installatie kan nu beginnen");
				Console.WriteLine("Kies een van de volgende opties");
				Console.WriteLine(
					"1: Full install, dit installeert: ThunderBird, CCleaner, Malwarebytes, Avast anti-virus en FireFox web browser");
				Console.WriteLine("2: AntiVirus, dit installeert Malwarebytes en Avast anti-virus");
				Console.WriteLine("3: Standaard, Dit installeert Firefox en ThunderBird");
				Console.WriteLine("Kies 1, 2 of 3");

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

						Console.WriteLine("Avast download wordt gestart!");
						DownloadAndExecute("https://cdn.thatgeek.dev/sah/antivirus.exe", "antivirus.exe");
					}

					// MalwareBytes

					Console.WriteLine("MalwareBytes download wordt gestart!");
					DownloadAndExecute("https://cdn.thatgeek.dev/sah/malwarebytes.exe", "malwarebytes.exe");
				}


				if (installType != 2)
				{
					//Firefox

					Console.WriteLine("Firefox download wordt gestart!");
					DownloadAndExecute("https://cdn.thatgeek.dev/sah/firefox.exe", "firefox.exe");

					//Thunderbird

					Console.WriteLine("Thunderbird download wordt gestart!");
					DownloadAndExecute("https://cdn.thatgeek.dev/sah/thunderbird.exe", "thunderbird.exe");


					if (installType == 1)
					{
						// CCleaner

						Console.WriteLine("CCleaner download wordt gestart!");
						DownloadAndExecute("https://cdn.thatgeek.dev/sah/ccleaner.exe", "ccleaner.exe");
					}
				}
			}
			
			Console.WriteLine("Druk op een toets om deze programma te sluiten");
			Console.Read();
		}

		private static void DownloadAndExecute(string url, string outputFileName)
		{
#if !DEBUG
			AutoResetEvent are = new AutoResetEvent(false);
			string path = Path.Combine(Directory.GetCurrentDirectory(), outputFileName);
			
			if(File.Exists(path)) File.Delete(path);
			
			var client = new WebClient();
			client.DownloadFileCompleted += new AsyncCompletedEventHandler((sender, args) =>
			{
				try
				{
					System.Diagnostics.Process.Start(path);
				}
				catch (Exception e)
				{
					Console.WriteLine($"Error: {e.Message}");
				}

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

		private static bool IsAdmin()
		{
			return (new WindowsPrincipal(WindowsIdentity.GetCurrent())).IsInRole(WindowsBuiltInRole.Administrator);
		}
	}
}