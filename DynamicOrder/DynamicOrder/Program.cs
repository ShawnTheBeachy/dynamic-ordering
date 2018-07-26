using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Dynamic.Core.Exceptions;

namespace DynamicOrder
{
	public class Program
	{
		private class Book
		{
			public string Author { get; set; }
			public string Title { get; set; }

			public Book(string author, string title)
			{
				Author = author;
				Title = title;
			}
		}

		private static readonly IList<Book> _books = new List<Book>
		{
			new Book("J.R.R. Tolkien", "Two Towers, The"),
			new Book("J.R.R. Tolkien", "Fellowship of the Ring, The"),
			new Book("Jonathan Renshaw", "Dawn of Wonder"),
			new Book("Agatha Christie", "And Then There Were None"),
			new Book("C.S. Lewis", "Out of the Silent Planet")
		};

		static void Main(string[] args)
		{
			Order(book => book.Title);
			ReadInput();
		}

		private static void Order(string properties)
		{
			try
			{
				var ordered = _books.AsQueryable().OrderBy(properties).ToList();
				WriteBooks(ordered);
			}

			catch (ParseException e)
			{
				Console.WriteLine(e.Message);
				ReadInput();
			}
		}

		private static void Order<TKey>(Func<Book, TKey> order)
		{
			var ordered = _books.OrderBy(order);
			WriteBooks(ordered);
		}

		private static void ReadInput()
		{
			Console.WriteLine();
			Console.Write("Enter a property or properties by which to order: ");
			var input = Console.ReadLine();
			Order(input);
			ReadInput();
		}

		private static void WriteBooks(IEnumerable<Book> books)
		{
			foreach (var book in books)
			{
				Console.WriteLine(book.Title + " by " + book.Author);
			}
		}
	}
}
