using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using BusinessLayer.Interface;
using BusinessLayer.Services;
using Experimental.System.Messaging;

namespace bookstorebackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController: ControllerBase
    {
        public ICartBL cartbl;
        public CartController(ICartBL cartbl)
        {
            this.cartbl = cartbl;
        }

        [Authorize]
        [HttpPost("AddtoCart")]
        public ActionResult AddCart(int BookId, int Quantity)
        {
            try
            {
                var UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                bool result = cartbl.AddCart(UserId, BookId, Quantity);
                if (result)
                {
                    return Ok(new { success = true, message = "Book added to cart" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Book not added !!" });

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpDelete("Delete")]
        public IActionResult RemoveFromlist(int cartId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = cartbl.DeleteCart(userId,cartId);
                if (result != null)
                {
                    return this.Ok(new { success = true,message= "cart deleted" });

                }
                else
                {
                    return this.BadRequest(new{ success = false, message = "cart didnot deleted"});
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpPut("Updatecart")]
        public IActionResult UpdateQtyInCart(int cartId, int quantity)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var res = cartbl.UpdateCart(userId,cartId, quantity);
                if (res != null)
                {
                    return Ok(new { success = true, message = "Update cart sucessfull" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Faild to update cart" });
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [Authorize]
        [HttpGet("GetCartDetails")]
        public ActionResult GetCartDetails()
        {
            try
            {
                var UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = cartbl.GetCartDetails(UserId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Getting Cart Details", Response = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Getting Cart Details Failed" });

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
