using System;

namespace MovieApp
{
    class Program
    {
		public static IMovieCollection lib = new MovieCollection();

		static void Main(string[] args)
        {
            IMovie eeaao = new Movie("eeaao", MovieGenre.Drama, MovieClassification.M15Plus, 999, 1);
			IMovie handmaiden = new Movie("handmaiden", MovieGenre.Drama, MovieClassification.M15Plus, 999, 1);
			IMovie batman = new Movie("batman", MovieGenre.Action, MovieClassification.M15Plus, 999, 1);
            IMovie arcane = new Movie("arcane", MovieGenre.Drama, MovieClassification.M15Plus, 999, 1);
            IMovie ricknmorty = new Movie("ricknmorty", MovieGenre.Comedy, MovieClassification.M15Plus, 999, 1);
            IMovie extracurricular = new Movie("extracurricular", MovieGenre.Drama, MovieClassification.M15Plus, 999, 1);

			Console.WriteLine("Adding 6 movies to BST...");
            lib.Insert(eeaao);
			lib.Insert(handmaiden);
			lib.Insert(batman);
			lib.Insert(arcane);
			lib.Insert(ricknmorty);
			lib.Insert(extracurricular);
			Print();

			Console.WriteLine($"Deleting node (w 2 children) ({handmaiden.Title}) from BST...");
			lib.Delete(handmaiden);
			Print();

			Console.WriteLine($"Deleting leaf (no children) ({arcane.Title}) from BST...");
			lib.Delete(arcane);
			Print();
			
			Console.Write($"Deleting root ({eeaao.Title}) from BST...");
			lib.Delete(eeaao);
			Console.Write($"root is now ({lib.Root})\n");
			Print();

			Console.Write($"Adding old root ({eeaao.Title}) back as node...");
			lib.Insert(eeaao);
			Console.Write($"root is still ({lib.Root})\n");
			Print();

			//// SEARCH TESTS HERE (Obj n Title)

			Console.WriteLine("clearing BST...");
			lib.Clear();
			Print();
		}

		public static void Print()
        {
			// demonstrates in-order traversal to get the array
			Console.WriteLine("\n━━┅━━━┅━━┅━━╾╮");
			foreach (IMovie m in lib.ToArray()) Console.Write((m).Title + "\n");
			Console.WriteLine("━━┅━━━┅━━┅━━╾╯");
		}

		public static bool MovieTests()
        {
			return false;
        }
	}
}

