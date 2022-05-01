// Phase 2
// An implementation of MovieCollection ADT
// 2022


using System;

//A class that models a node of a binary search tree
//An instance of this class is a node in a binary search tree 
public class BTreeNode
{
	private IMovie movie; // movie
	private BTreeNode lchild; // reference to its left child 
	private BTreeNode rchild; // reference to its right child

	public BTreeNode(IMovie movie)
	{
		this.movie = movie;
		lchild = null;
		rchild = null;
	}

	public IMovie Movie
	{
		get { return movie; }
		set { movie = value; }
	}

	public BTreeNode LChild
	{
		get { return lchild; }
		set { lchild = value; }
	}

	public BTreeNode RChild
	{
		get { return rchild; }
		set { rchild = value; }
	}
}

// invariant: no duplicates in this movie collection
public class MovieCollection : IMovieCollection
{
	private BTreeNode root; // movies are stored in a binary search tree and the root of the binary search tree is 'root' 
	private int count; // the number of (different) movies currently stored in this movie collection 



	// get the number of movies in this movie colllection 
	// pre-condition: nil
	// post-condition: return the number of movies in this movie collection and this movie collection remains unchanged
	public int Number { get { return count; } }

	public string Root { get { return root.Movie.Title; } } // DEBUGGING ONLY, DELETE BEFORE SUBMITTING

	// constructor - create an object of MovieCollection object
	public MovieCollection()
	{
		root = null;
		count = 0;	
	}

	// Check if this movie collection is empty
	// Pre-condition: nil
	// Post-condition: return true if this movie collection is empty; otherwise, return false.
	public bool IsEmpty()
	{
		// author: Johnny Madigan

		return root == null;
	}

	// Insert a movie into this movie collection
	// Pre-condition: nil
	// Post-condition: the movie has been added into this movie collection and return true, if the movie is not in this movie collection; otherwise, the movie has not been added into this movie collection and return false.
	public bool Insert(IMovie movie)
	{
		// author: Johnny Madigan

		if (!Search(movie)) // no duplicates
		{
			// If tree is empty? set movie as first node...
			// otherwise kick off recursive travel to insert as leaf
			if (IsEmpty()) root = new BTreeNode(movie); 
			else Insert(movie, root); 
			count++;
			return true;
		}
		else return false;
	}

	// PRIVATE recursive insert, to be initiated by public insert (ensures starting parent is not null)
	// Pre-condition: parent != null
	// Post-condition: item is inserted to the BST based on it's title
	private void Insert(IMovie movie, BTreeNode parent)
	{
		// author: Johnny Madigan

		// If movie belongs in the left subtree, insert as leaf otherwise continue travel left
		if (movie.CompareTo(parent.Movie) < 0)
		{
			if (parent.LChild == null) parent.LChild = new BTreeNode(movie);
			else Insert(movie, parent.LChild);
		}
		// If movie belongs in the right subtree, insert as leaf otherwise continue travel right
		else
		{
			if (parent.RChild == null) parent.RChild = new BTreeNode(movie);
			else Insert(movie, parent.RChild);
		}
	}

	// Delete a movie from this movie collection
	// Pre-condition: nil
	// Post-condition: the movie is removed out of this movie collection and return true, if it is in this movie collection; return false, if it is not in this movie collection
	public bool Delete(IMovie movie)
	{
		// author: Johnny Madigan

		BTreeNode toDelete = root;
		BTreeNode toDeleteParent = null;

		// While we haven't reached end of tree AND the queried movie hasn't been found...
		// we will perform a non-recursive search for the movie (cannot use SEARCH as it doesn't keep track of parent)
		while ((toDelete != null) && (movie.CompareTo(toDelete.Movie) != 0))
		{
			toDeleteParent = toDelete;												// Remember the parent
			if (movie.CompareTo(toDelete.Movie) < 0) toDelete = toDelete.LChild;	// Movie in LEFT subtree
			else toDelete = toDelete.RChild;										// Movie in RIGHT subtree
		}

		// IF SEARCH WAS SUCCESSFUL, THERE ARE 3 SCENARIOS TO HANDLE:
		// 1. node to delete has 2 children (left n right)
		// 2. node to delete has only 1 child 
		// 3. node to delete is a leaf
		if (toDelete != null)
		{
			// CASE 1
			if ((toDelete.LChild != null) && (toDelete.RChild != null))
			{
				if (toDelete.LChild.RChild == null) // if no right nodes in LEFT subtree...
				{
					// replace toDelete's movie with its left child's movie (keeps correct order)
					// set toDelete's left pointer to skip over its left child effectively dropping it...
					// C#'s garbage collector will clean up the dropped node
					toDelete.Movie = toDelete.LChild.Movie;
					toDelete.LChild = toDelete.LChild.LChild;
				}
				else
				{
					BTreeNode p = toDelete.LChild; // LEFT subtree's root
					BTreeNode pp = toDelete; // parent of p

					// in the LEFT subtree, travel to the most RIGHT node (not necessarily leaf) keeping track of the parent
					while (p.RChild != null)
					{
						pp = p;
						p = p.RChild;
					}
					// replace toDelete's movie with this far-right node's movie (keeps correct order)
					// set parent of far-right's RIGHT pointer to skip to far-right's LEFT child...
					// effectively dropping far-right while keeping any subtrees connected
					// C#'s garbage collector will clean up the dropped node
					toDelete.Movie = p.Movie;
					pp.RChild = p.LChild;
				}
			}
			// CASE 2 & 3
			else
			{
				BTreeNode c;

				// Temporarily store toDelete's only child (prioritise left then right/null)
				if (toDelete.LChild != null) c = toDelete.LChild;
				else c = toDelete.RChild;

				// If toDelete is the root of the BST, toDelete's child is now the root...
				// otherwise check toDelete's parent to see if toDelete is in the LEFT or RIGHT subtree
				// to correctly reconnect toDelete's child back to its parent, effectively dropping toDelete
				if (toDelete == root) root = c;
				else
				{
					if (toDelete == toDeleteParent.LChild) toDeleteParent.LChild = c;	// toDelete is in LEFT subtree
					else toDeleteParent.RChild = c;                                     // toDelete is in RIGHT subtree
				}
			}
			count--;
			return true; // deletion completed
		}
		else return false; // not found
	}

	// Search for a movie in this movie collection
	// pre: nil
	// post: return true if the movie is in this movie collection;
	//	     otherwise, return false.
	public bool Search(IMovie movie)
	{
		// author: Johnny Madigan

		// use private SEARCH that takes a title and returns the object...
		// return true if the object is found (kills 2 birds with 1 stone)
		return Search(movie.Title, root) != null;
	}

	// Search for a movie by its title in this movie collection  
	// pre: nil
	// post: return the reference of the movie object if the movie is in this movie collection;
	//	     otherwise, return null.
	public IMovie Search(string movietitle)
	{
		// author: Johnny Madigan

		return Search(movietitle, root);
	}

	// PRIVATE recursive search for a movie in the BST via its title
	// pre: nil
	// post: return the the movie object if found within this BST, otherwise return null
	private IMovie Search(string title, BTreeNode parent)
	{
		// author: Johnny Madigan

		if (parent != null)
		{
			// Return the object if found, otherwise travel to the LEFT or RIGHT subtree and repeat
			if (title.CompareTo(parent.Movie.Title) == 0) return parent.Movie;
			else if (title.CompareTo(parent.Movie.Title) < 0) return Search(title, parent.LChild);
			else return Search(title, parent.RChild);
		}
		else return null;
	}

	// Store all the movies in this movie collection in an array in the dictionary order by their titles
	// Pre-condition: nil
	// Post-condition: return an array of movies that are stored in dictionary order by their titles
	public IMovie[] ToArray()
	{
		// author: Johnny Madigan

		// Kick off recursive in-order traversal...
        // passing an array throughout to be modified along the way
		IMovie[] arr = new IMovie[count];
		return InOrderTravel(root, arr);
	}

	// PRIVATE recursive in-order traversal, modifying an array throughout the journey
	// Pre-condition: nil
	// Post-condition: return the given array modified with a movie
	private IMovie[] InOrderTravel(BTreeNode current, IMovie[] arr)
	{
		// author: Johnny Madigan

		// While not a leaf, travel down to LEFT subtree...
		// then bubble back up 1 node and travel RIGHT...
        // repeating this pattern effectively travelling to all nodes in-order
		if (current != null)
		{
			InOrderTravel(current.LChild, arr);

			int i = 0;
			while (arr[i] != null) i++;	// skip to next null space in array
			arr[i] = current.Movie;		// insert movie into null space

			InOrderTravel(current.RChild, arr);
		}
		return arr; // bubble back up and bring modified array
	}



	// Clear this movie collection
	// Pre-condotion: nil
	// Post-condition: all the movies have been removed from this movie collection 
	public void Clear()
	{
		// author: Johnny Madigan
		
		root = null; // Drop entire tree... C#'s garbage collector will clean up these unused nodes
		count = 0;
	}
}





