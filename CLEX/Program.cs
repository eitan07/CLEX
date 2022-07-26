using System.IO;

namespace CLEX
{
	internal class Program
	{
		static string current_path = "C:/Users/user/Desktop";

		// ----------
		static string[] absolute_files_paths = Directory.GetFiles(current_path);
		static string[] files_names = new string[absolute_files_paths.Length - 1];
		static string[] files = Directory.GetFiles(current_path);
		static int itemIndex = 0;

		static void Main(string[] args)
		{
			Console.Clear();
			Console.WriteLine("\n");
			Console.ForegroundColor = ConsoleColor.Gray;
			
			for (int i = 0; i < absolute_files_paths.Length -1; i++)
			{
				files_names[i] = Path.GetFileName(absolute_files_paths[i]);
			}

			while (true)
			{
				printList();

				ConsoleKeyInfo cKI = Console.ReadKey();
				ConsoleKey cK = cKI.Key;

				if (cK == ConsoleKey.UpArrow)
				{
					itemIndex--;
				}
				else if (cK == ConsoleKey.DownArrow)
				{
					itemIndex++;
				}
				else if (cK == ConsoleKey.Q)
				{
					Environment.Exit(0);
				}
			}

		}

		static public void printList()
		{
			Console.Clear();
			for (int i = 0; i < files_names.Length - 1; i++)
			{
				if (i == itemIndex)
				{
					Console.ForegroundColor = ConsoleColor.White;
					Console.WriteLine($"{multiText(" ", 4)}>{multiText(" ", 2)}{files_names[i]}");
					Console.ForegroundColor = ConsoleColor.Gray;
				} else
				{
					Console.WriteLine($"{multiText(" ", 6)}{files_names[i]}");
				}
			}
		}

		static public string multiText(string text, int times)
		{
			string final = "";
			for (int i = 0; i < times - 1; i++)
			{
				final += text;
			}
			return final;
		}
	}
}