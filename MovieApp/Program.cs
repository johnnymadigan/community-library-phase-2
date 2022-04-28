using System;

namespace MovieApp
{
    class Program
    {
		public static IMovieCollection lib = new MovieCollection();

		public static IMovie eeaao = new Movie("eeaao", MovieGenre.Drama, MovieClassification.M15Plus, 999, 10);
		public static IMovie handmaiden = new Movie("handmaiden", MovieGenre.Drama, MovieClassification.M15Plus, 999, 1);
		public static IMovie batman = new Movie("batman", MovieGenre.Action, MovieClassification.M15Plus, 999, 1);
		public static IMovie arcane = new Movie("arcane", MovieGenre.Drama, MovieClassification.M15Plus, 999, 1);
		public static IMovie ricknmorty = new Movie("ricknmorty", MovieGenre.Comedy, MovieClassification.M15Plus, 999, 1);
		public static IMovie extracurricular = new Movie("extracurricular", MovieGenre.Drama, MovieClassification.M15Plus, 999, 1);

		static void Main(string[] args)
        {
			MovieTests();
			AddTests();
			DeleteTests();
			ClearTests();
		}





		public static void Print()
        {
			// IN-ORDER TRAVERSAL TESTS
			Console.WriteLine("\n━━┅━━━┅━━┅━━╾╮");
			foreach (IMovie m in lib.ToArray()) Console.Write((m).Title + "\n");
			Console.WriteLine("━━┅━━━┅━━┅━━╾╯");
		}

		public static void MovieTests()
        {
			// MOVIE'S COMPARETO HAS BEEN IMPLICITY TESTED IN THE CREATION OF THE BST
			// TESTING ADD/REMOVE/TOSTRING
			IMember dude = new Member("bofa", "dem");
			Console.WriteLine($"{eeaao.ToString()} available copies\n");
			eeaao.AddBorrower(dude);
			Console.WriteLine($"{eeaao.ToString()} available copies (-1)\n");
			eeaao.RemoveBorrower(dude);
			Console.WriteLine($"{eeaao.ToString()} available copies (+1)\n");
		}

		public static void AddTests()
        {
			Console.WriteLine("Adding 6 movies to BST...");
			lib.Insert(eeaao);
			lib.Insert(handmaiden);
			lib.Insert(batman);
			lib.Insert(arcane);
			lib.Insert(ricknmorty);
			lib.Insert(extracurricular);
			Print();
		}

		public static void DeleteTests()
        {
			Console.WriteLine($"\nDeleting node (w 2 children) ({handmaiden.Title}) from BST...");
			lib.Delete(handmaiden);
			Print();

			Console.WriteLine($"\nDeleting leaf (no children) ({arcane.Title}) from BST...");
			lib.Delete(arcane);
			Print();

			Console.WriteLine($"\nAlready deleted ({arcane.Title}) no change...");
			lib.Delete(arcane);
			Print();

			Console.Write($"\nDeleting root ({eeaao.Title}) from BST...");
			lib.Delete(eeaao);
			Console.Write($"root is now ({lib.Root})\n");
			Print();

			Console.Write($"\nAdding old root ({eeaao.Title}) back as node...");
			lib.Insert(eeaao);
			Console.Write($"root is still ({lib.Root})\n");
			Print();
		}

		public static void SearchTests()
        {
			string s;

			s = lib.Search("eeaao") != null ? $"✔ (found via string)" : $"✘ (!found via string)";
			Console.WriteLine($"\n✔ = {s}");

			s = lib.Search("arcane") != null ? $"✔ (found via string)" : $"✘ (!found via string)";
			Console.WriteLine($"✘ = {s}");

			s = lib.Search(eeaao) ? $"✔ (found via obj)" : $"✘ (!found via obj)";
			Console.WriteLine($"✔ = {s}");

			s = lib.Search(arcane) ? $"✔ (found via obj)" : $"✘ (!found via obj)";
			Console.WriteLine($"✘ = {s}");
		}

		public static void ClearTests()
        {
			Console.WriteLine("\nclearing BST...");
			lib.Clear();
			Print();
		}
	}
}

