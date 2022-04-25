using System;

namespace MovieApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            IMovie handmaiden = new Movie("handmaiden", MovieGenre.Drama, MovieClassification.M15Plus, 999, 1);
            IMovie eeaao = new Movie("eeaao", MovieGenre.Drama, MovieClassification.M15Plus, 999, 1);
            IMovie batman = new Movie("batman", MovieGenre.Action, MovieClassification.M15Plus, 999, 1);
            IMovie arcane = new Movie("arcane", MovieGenre.Drama, MovieClassification.M15Plus, 999, 1);

            IMovieCollection lib = new MovieCollection();

            lib.Insert(handmaiden);
            lib.Insert(eeaao);
            lib.Insert(batman);
            lib.Insert(arcane);

            foreach (IMovie m in lib.ToArray()) Console.Write((m).Title + "\n");
        }
    }
}

