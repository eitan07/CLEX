using System.Diagnostics;
using System.Collections.Generic;

namespace CLEX
{
	internal class Program
	{
		static string current_path = "C:/Users/user/Desktop";

		// ----------
		static string[] absolute_files_paths = Directory.GetFiles(current_path);
		static List<string> files_names = new List<string>();
		static int itemIndex = 0;
		static readonly string[] fileMenuOptions = { "Open", "Read", "Write", "Properties", "Copy", "Move", "Delete", "Back to main menu" };
		static bool FIRST_RUN = true;
		static void Main(string[] args)
		{
			if (FIRST_RUN)
			{
				Initialize();
				FIRST_RUN = false;
			}

			itemIndex = 0;
			if (OperatingSystem.IsWindows())
			{
				Console.SetWindowSize(120, 55);
			}
			
			Console.Title = "CLEX";

			while (true)
			{
				Console.Clear();
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine($"\n{MultiText(" ", 6)}               ,--,                               \n{MultiText(" ", 6)}            ,---.'|                               \n{MultiText(" ", 6)}  ,----..   |   | :        ,---,.  ,--,     ,--,  \n{MultiText(" ", 6)} /   /   \\  :   : |      ,'  .' |  |'. \\   / .`|  \n{MultiText(" ", 6)}|   :     : |   ' :    ,---.'   |  ; \\ `\\ /' / ;  \n{MultiText(" ", 6)}.   |  ;. / ;   ; '    |   |   .'  `. \\  /  / .'  \n{MultiText(" ", 6)}.   ; /--`  '   | |__  :   :  |-,   \\  \\/  / ./   \n{MultiText(" ", 6)};   | ;     |   | :.'| :   |  ;/|    \\  \\.'  /    \n{MultiText(" ", 6)}|   : |     '   :    ; |   :   .'     \\  ;  ;     \n{MultiText(" ", 6)}.   | '___  |   |  ./  |   |  |-,    / \\  \\  \\    \n{MultiText(" ", 6)}'   ; : .'| ;   : ;    '   :  ;/|   ;  /\\  \\  \\   \n{MultiText(" ", 6)}'   | '/  : |   ,/     |   |    \\ ./__;  \\  ;  \\  \n{MultiText(" ", 6)}|   :    /  '---'      |   :   .' |   : / \\  \\  ; \n{MultiText(" ", 6)} \\   \\ .'              |   | ,'   ;   |/   \\  ' | \n{MultiText(" ", 6)}  `---`                `----'     `---'     `--`   By Eitan07");
				Console.WriteLine("\nPress Q to leave at any menu");
				Console.WriteLine("\nPress C to change directory");
				Console.ResetColor();

				printList();

				ConsoleKeyInfo cKI = Console.ReadKey();
				ConsoleKey cK = cKI.Key;

				if (cK == ConsoleKey.UpArrow)
				{
					if (!(itemIndex == 0)) {
						itemIndex--;
					} else
					{
						itemIndex = files_names.Count - 1;
					}
				}
				else if (cK == ConsoleKey.DownArrow)
				{
					if (!(itemIndex == files_names.Count - 1))
					{
						itemIndex++;
					} else
					{
						itemIndex = 0;
					}
				}
				else if (cK == ConsoleKey.Enter)
				{
					OpenFileMenuFor(absolute_files_paths[itemIndex], args);
				} 
				else if (cK == ConsoleKey.C)
				{
					// TODO: Make a change directory option (menu)
				}
				else if (cK == ConsoleKey.Q)
				{
					Environment.Exit(0);
				}
			}

		}

		static public void Initialize()
		{
			absolute_files_paths = Directory.GetFiles(current_path);

			for (int i = 0; i < absolute_files_paths.Length - 1; i++)
			{
				FileInfo fI = new FileInfo(absolute_files_paths[i]);
				DateTime creationTime = fI.CreationTime;
				files_names.Add($"{i + 1}. {Path.GetFileName(absolute_files_paths[i])}{MultiText(" ", 40 - Path.GetFileName(absolute_files_paths[i]).Length - (i + 1).ToString().Length)}{creationTime}");
			}
		}

		static public void printList(string menu = "main")
		{			
			switch (menu)
			{
				case "main":
					
					Console.WriteLine("\n");
					for (int i = 0; i < files_names.Count; i++)
					{

						if (i == itemIndex)
						{
							Console.ForegroundColor = ConsoleColor.White;
							Console.WriteLine($"{MultiText(" ", 4)}>{MultiText(" ", 2)}{files_names[i]}");
							Console.ForegroundColor = ConsoleColor.Gray;
						}
						else
						{
							Console.WriteLine($"{MultiText(" ", 6)}{files_names[i]}");
						}
					}
					break;


				case "file":
					Console.WriteLine("\n");
					for (int i = 0; i < fileMenuOptions.Length; i++)
					{
						if (i == itemIndex)
						{
							Console.ForegroundColor = ConsoleColor.White;
							Console.WriteLine($"{MultiText(" ", 4)}>{MultiText(" ", 2)}{fileMenuOptions[i]}");
							Console.ForegroundColor = ConsoleColor.Gray;
						}
						else
						{
							Console.WriteLine($"{MultiText(" ", 6)}{fileMenuOptions[i]}");
						}
					}
					break;

			}
		}

		static public string MultiText(string text, int times)
		{
			string final = "";
			for (int i = 0; i < times - 1; i++)
			{
				final += text;
			}
			return final;
		}

		// ----------
		static public void OpenFileMenuFor(string path, string[] args)
		{
			itemIndex = 0;
			Console.Clear();

			while (true)
			{
				Console.Clear();
				Console.WriteLine("\n");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine($"{MultiText(" ", 6)}{path}");
				Console.ResetColor();

				printList("file");

				ConsoleKeyInfo cKI = Console.ReadKey();
				ConsoleKey cK = cKI.Key;

				if (cK == ConsoleKey.UpArrow)
				{
					if (!(itemIndex == 0))
					{
						itemIndex--;
					}
					else
					{
						itemIndex = fileMenuOptions.Length - 1;
					}
				}
				else if (cK == ConsoleKey.DownArrow)
				{
					if (!(itemIndex == fileMenuOptions.Length - 1))
					{
						itemIndex++;
					}
					else
					{
						itemIndex = 0;
					}
				}
				else if (cK == ConsoleKey.Enter)
				{
					switch (itemIndex)
					{
						case 0:
							Process.Start("cmd", $"/c start {path}");
							break;

						case 1:
							string ex = Path.GetExtension(path);
							if (ex == ".zip" || ex == ".rar" || ex == ".bin" || ex == ".lnk")
							{
								Console.WriteLine("The file isn't a text file.\nAre you sure you want to load it? (Y/N)");
								ConsoleKey _k = Console.ReadKey().Key;
								if (_k == ConsoleKey.Y)
								{
									string content = File.ReadAllText(path);
									Console.Clear();

									Console.WriteLine("\n\n" + content);
									Console.WriteLine("\n Tap to return to menu....");
									Console.Read();
								}
								else break;
							}
							else
							{
								string content = File.ReadAllText(path);
								Console.Clear();

								Console.WriteLine("\n\n" + content);
								Console.WriteLine("\nTap to return to menu....");
								Console.Read();
							}
							
							break;

						case 2:
							Console.Write("\nOpen File In Vim? (Y/N): ");
							ConsoleKey consoleKey = Console.ReadKey().Key;
							if (consoleKey == ConsoleKey.Y) {
								Console.Clear();
								Process vim = Process.Start("cmd.exe", $"/c vim {path}");
								if (vim != null)
								{
									vim!.WaitForExit();
								} else
								{
									Console.ForegroundColor = ConsoleColor.Red;
									Console.WriteLine("Error on starting Vim :(");
									Console.ResetColor();
								}
							}
							break;

						case 3:
							Console.Clear();
							Console.WriteLine($"\n\n{MultiText(" ", 6)}File properties for {path}\n");
							FileInfo fInfo = new FileInfo(path);
							Console.WriteLine($"{MultiText(" ", 6)}Path: {fInfo.FullName}");
							Console.WriteLine($"{MultiText(" ", 6)}File Name: {fInfo.Name}");
							Console.WriteLine($"{MultiText(" ", 6)}Size: {fInfo.Length / 1000}MB ({fInfo.Length}B)");
							Console.WriteLine($"{MultiText(" ", 6)}Creation Time: {fInfo.CreationTime}");
							Console.WriteLine($"{MultiText(" ", 6)}Last modified time: {fInfo.LastWriteTime}");
							Console.WriteLine($"{MultiText(" ", 6)}Last Access time: {fInfo.LastAccessTime}");
							Console.WriteLine($"{MultiText(" ", 6)}Attributes: {fInfo.Attributes}");
							Console.WriteLine($"{MultiText(" ", 6)}Extension: {fInfo.Extension}");
							Console.WriteLine($"{MultiText(" ", 6)}Read Only: {fInfo.IsReadOnly}");
							Console.WriteLine($"{MultiText(" ", 6)}Exists: {fInfo.Exists}");
							Console.Read();
							break;

						case 4:
							// TODO: Implement Copy Function
							break;

						case 5:
							// TODO: Implement Move Function
							break;

						case 6:
							Console.Write("\nDelete File? (Y/N): ");
							ConsoleKey consoleKey1 = Console.ReadKey().Key;
							if (consoleKey1 == ConsoleKey.Y)
							{
								File.Delete(path);
								Main(args);
							}
							break;

						case 7:
							Main(args);
							break;
					}
				}
				else if (cK == ConsoleKey.Q)
				{
					Environment.Exit(0);
				}
			}
	
		}
	}
}