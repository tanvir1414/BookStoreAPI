using BookStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Data.Interfaces
{
    public interface IBookRepository
    {
        List<Book> GetAllBooks();
        Book GetBook(int id);

        bool AddNewBook(Book book);
        bool Remove(int id);
        List<Book> UpdateBook(int id, Book book);
    }
}
