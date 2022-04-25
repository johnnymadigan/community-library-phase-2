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
		return root == null;
	}

	// Insert a movie into this movie collection
	// Pre-condition: nil
	// Post-condition: the movie has been added into this movie collection
	public bool Insert(IMovie movie)
	{
		if (!Search(movie)) // invariant: no duplicates in this movie collection
		{
			if (root == null) root = new BTreeNode(movie); // empty tree? first node
			else Insert(movie, root); // kick off recursive travel to insert as leaf
			count++;
			return true;
		}
		else return false;
	}

	// PUBLIC INSERT ensures the first call of this method is with a root that isn't null
	//
	// pre: ptr != null
	// post: item is inserted to the binary search tree rooted at ptr
	// recursive
	private void Insert(IMovie movie, BTreeNode parent)
	{
		// 
		if (movie.CompareTo(parent.Movie) < 0)
		{
			if (parent.LChild == null) parent.LChild = new BTreeNode(movie);
			else Insert(movie, parent.LChild);
		}
		else
		{
			if (parent.RChild == null) parent.RChild = new BTreeNode(movie);
			else Insert(movie, parent.RChild);
		}
	}


	// Delete a movie from this movie collection
	// Pre-condition: nil
	// Post-condition: the movie is removed out of this BST, if it is in this BST
	public bool Delete(IMovie movie)
	{
		//To be completed
		// there are three cases to consider:
		// 1. the node to be deleted is a leaf
		// 2. the node to be deleted has only one child 
		// 3. the node to be deleted has both left and right children



		// search for item and its parent
		BTreeNode curr = root; // current search reference
		BTreeNode parent = null; // parent of current

		// while current aint null AND the queried movie does not match the current's movie (found)
		// we need this kind of search here to keep track of the parent, but all g as BST super quick
		while ((curr != null) && (movie.CompareTo(curr.Movie) != 0))
		{
			// Traverse to a child node (updating current) and remember the parent
			parent = curr;
			if (movie.CompareTo(curr.Movie) < 0) curr = curr.LChild;	// Movie in left subtree
			else curr = curr.RChild;									// Movie in right subtree
		}

		if (curr != null) // if the search was successful aka current is now the node to DELETE
		{
			// case 3: ITEM HAS 2 CHILDREN
			if ((curr.LChild != null) && (curr.RChild != null))
			{
				// find the right-most node in left subtree of current
				if (curr.LChild.RChild == null) // a special case: the right subtree of ptr.LChild is empty
				{
					// replace current's movie with it's left node's movie (keeps correct order)
					// set pointer to point to left child's left child (right is null remember) effectively dropping,
					// C#'s garbage collection is better than C
					curr.Movie = curr.LChild.Movie;
					curr.LChild = curr.LChild.LChild;
				}
				else // if the left child's subtree has both left and right children again
				{
					// quickly store left child subtree parent and left child
					BTreeNode p = curr.LChild;
					BTreeNode pp = curr; // parent of p (kinda uselesss making it current but good practice for default value)

					// travel to most right leaf node starting from tHAT subtree and keep track of the parent
					while (p.RChild != null)
					{
						pp = p;
						p = p.RChild;
					}
					// replace current's movie with this far right (keeps correct order)
					// set pointer to point to left child's left child (right is null remember) effectively dropping,
					// C#'s garbage collection is better than C
					// copy the item at p to ptr
					curr.Movie = p.Movie;
					pp.RChild = p.LChild;
				}
			}
			else // cases 1 & 2: item has no or only one child
			{
				// temp store the single child
				BTreeNode c;
				if (curr.LChild != null)
					c = curr.LChild;
				else
					c = curr.RChild;

				// remove node current
				if (curr == root) //need to change root
					root = c;
				else
				{
					if (curr == parent.LChild)
						parent.LChild = c;
					else
						parent.RChild = c;
				}
			}
			count--;
			return true; // deletion complete
		}
		else return false; // not found
	}

	// Search for a movie in this movie collection
	// pre: nil
	// post: return true if the movie is in this movie collection;
	//	     otherwise, return false.
	public bool Search(IMovie movie)
	{
		return Search(movie.Title, root) != null;
	}

	// Search for a movie by its title in this movie collection  
	// pre: nil
	// post: return the reference of the movie object if the movie is in this movie collection;
	//	     otherwise, return null.
	public IMovie Search(string movietitle)
	{
		return Search(movietitle, root);
	}

	private IMovie Search(string title, BTreeNode parent)
	{
		// recursive, return if the object if found, treats each recursion's root as the root
		// of 0,1,2 children (subtree) r is the subtree's root, NOT THE ENTIRE TREE
		// but obviously starts with the root of the entire tree as the first one
		if (parent != null)
		{
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
		//To be completed
		// in-order traversal
		// pass through the collection and index
		IMovie[] arr = new IMovie[count];
		return InOrderTraverse(root, arr);
	}

	// recursive in-order traversal
	private IMovie[] InOrderTraverse(BTreeNode current, IMovie[] arr)
	{
		if (current != null)
		{
			InOrderTraverse(current.LChild, arr);
			int i = 0;
			while (arr[i] != null) i++; // skip to null
			arr[i] = current.Movie;
			Console.Write("inserted @ " + i + "\n");
			InOrderTraverse(current.RChild, arr);
		}
		return arr;
	}



	// Clear this BST
	// Pre-condotion: nil
	// Post-condition: all the movies have been removed from this movie collection 
	public void Clear()
	{
		root = null;
	}
}





