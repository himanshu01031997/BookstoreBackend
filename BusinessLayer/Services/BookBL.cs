using BusinessLayer.Interface;
using CommonLayer.Model;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class BookBL:IBookBL
    {
        private readonly IBookRL bookRL;
        public BookBL(IBookRL bookRL)
        {
            this.bookRL = bookRL;
        }
        public BookModel AddBook(BookModel bookModel)
        {
            try
            {
                return this.bookRL.AddBook(bookModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public BookModel UpdateBook(BookModel bookModel)
        {
            try
            {
                return this.bookRL.UpdateBook(bookModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool DeleteBook(int BookId)
        {
            try
            {
                return this.bookRL.DeleteBook(BookId);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public List<BookModel> GetAllBooks()
        {
            try
            {
                return this.bookRL.GetAllBooks();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public BookModel GetBookByBookId(int BookId)
        {
            try
            {
                return this.bookRL.GetBookByBookId(BookId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }



    }
}
