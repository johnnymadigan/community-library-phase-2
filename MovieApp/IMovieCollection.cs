//CAB301 project - Phase 2 
//The specification of MovieCollection ADT
//2022

using System;

// invariant: no duplicates in this movie collection
public interface IMovieCollection
{


	// get the number of (different) movies in this movie collection
	int Number 
	{
		get;
	}

	// Check if this movie collection is empty
	// Pre-condition: nil
	// Post-condition: return true if this movie collection is empty; otherwise, return false.
	bool IsEmpty();


	// Insert a movie into this movie collection
	// Pre-condition: nil
	// Post-condition: the movie has been added into this movie collection
	bool Insert(IMovie movie);

	// Delete a movie from this movie collection
	// Pre-condition: nil
	// Post-condition: the movie is removed out of this BST, if it is in this BST
	bool Delete(IMovie movie);

	// Search for a movie in this movie collection
	// pre: nil
	// post: return true if the movie is in this movie collection;
	//	     otherwise, return false.
	bool Search(IMovie movie);

	// Search for a movie by its title in this movie collection  
	// pre: nil
	// post: return the reference of the movie object if the movie is in this movie collection;
	//	     otherwise, return null.
	public IMovie Search(string title);


	// Store all the movies in this movie collection in an array in the dictionary order by their titles
	// Pre-condition: nil
	// Post-condition: return an array of movies that are stored in dictionary order by their titles
	IMovie[] ToArray();

	// Clear this BST
	// Pre-condotion: nil
	// Post-condition: all the movies have been removed from this movie collection 
	void Clear();

}

