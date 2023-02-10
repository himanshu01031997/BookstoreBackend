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

    }
}
