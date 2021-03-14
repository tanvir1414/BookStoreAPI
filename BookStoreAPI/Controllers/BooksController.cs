using BookStore.Data.Interfaces;
using BookStore.Data.Models;
using BookStore.Data.Repositories;
using BookStoreAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        //public List<Book> books = new List<Book>() {
        //new Book { Id = 1, Title = "The Girl on the Train", Author = "Hawkins, Paula", PublicationYear = 2015, CallNumber = "F HAWKI"},
        //new Book { Id = 2, Title = "Rogue Lawyer", Author = "Grisham, John", PublicationYear = 2015, CallNumber = "F GRISH"},
        //new Book { Id = 3, Title = "After You", Author = "Moyes, Jojo", PublicationYear = 2015, CallNumber = "F MOYES"},
        //new Book { Id = 4, Title = "All the Light We Cannot See", Author = "Doerr, Anthony", PublicationYear = 2014, CallNumber = "F DOERR"},
        //new Book { Id = 5, Title = "The Girls", Author = "Cline, Emma", PublicationYear = 2016, CallNumber = "F CLINE"},
        //new Book { Id = 6, Title = "The Martian", Author = "Weir, Andy", PublicationYear = 2011, CallNumber = "SF WEIR"},
        //new Book { Id = 7, Title = "Me Before You", Author = "Moyes, Jojo", PublicationYear = 2012, CallNumber = "F MOYES"},
        //new Book { Id = 8, Title = "Alexander Hamilton", Author = "Chernow, Ron", PublicationYear = 2004, CallNumber = "B HAMILTO A"},
        //new Book { Id = 9, Title = "Before the Fall", Author = "Hawley, Noah", PublicationYear = 2016, CallNumber = "F HAWLE"}
        //};

        // private IBookRepository books = new BookRepository();
        private IBookRepository books;

        public BooksController(IBookRepository _books)
        {
            this.books = _books;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAllBooks()
        {
            return books.GetAllBooks();
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            // var book = books.FirstOrDefault(x => x.Id == id);
            var book = books.GetBook(id);

            if(book == null)
            {
                return NotFound();
            }
            return book;
        }

        //use ([FromForm] Book book) when using UI
        [HttpPost]
        public ActionResult<Book> PostBook(Book book)
        {
            if (books.AddNewBook(book))
            {
                return book;
            }
            return BadRequest();
        }


        [HttpDelete("{id}")]
        public ActionResult<IEnumerable<Book>> DeleteBook(int id)
        {
            if (books.Remove(id))
            {
                return books.GetAllBooks();
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public ActionResult<IEnumerable<Book>> UpdateBook(int id,Book book)
        {
            var ubook = books.UpdateBook(id, book);
            if (ubook != null)
            {
                return ubook;
            }
            return NotFound();
        }


    }
}
