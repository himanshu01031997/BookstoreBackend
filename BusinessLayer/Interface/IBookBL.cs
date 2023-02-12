using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IBookBL
    {
        public BookModel AddBook(BookModel bookModel);
        public BookModel UpdateBook(BookModel bookModel);
        public bool DeleteBook(int BookId);
        public List<BookModel> GetAllBooks();
        public BookModel GetBookByBookId(int BookId);





    }
}
