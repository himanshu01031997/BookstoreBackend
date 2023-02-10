using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace bookstorebackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController:ControllerBase
    {
        private readonly IBookBL bookbl;
        public BookController(IBookBL bookbl)
        {
            this.bookbl = bookbl;
        }
        [Authorize(Roles = Role.Admin)]
        [HttpPost("AddBook")]
        public ActionResult AddBook(BookModel bookModel)
        {
            try
            {
                var result = bookbl.AddBook(bookModel);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Book Added Successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Book Adding Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
    }
}
