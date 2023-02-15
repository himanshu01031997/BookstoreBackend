using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using BusinessLayer.Interface;

namespace bookstorebackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController:ControllerBase
    {
        public IWishListBL wishbl;
        public WishListController(IWishListBL wishbl)
        {
            this.wishbl = wishbl;
        }
        [Authorize]
        [HttpPost("AddToWishlist")]
        public ActionResult AddToWishlist(int BookId)
        {
            try
            {
                var UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                bool result = wishbl.AddToWishlist(UserId, BookId);
                if (result)
                {
                    return Ok(new { success = true, message = "Book added to Wishlist" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Book not Wishlist !!" });

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [Authorize]
        [HttpDelete("DeleteFromWishlist")]
        public ActionResult DeleteFromWishlist(int WishlistId)
        {
            try
            {
                var UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                bool result = wishbl.DeleteFromWishlist(UserId, WishlistId);
                if (result)
                {
                    return Ok(new { success = true, message = "Wishlist Deleted Successfully" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Wishlist Deletion Failed" });

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [Authorize]
        [HttpGet("GetWishlistDetails")]
        public ActionResult GetWishlistDetails()
        {
            try
            {
                var UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = wishbl.GetWishlisDetails(UserId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Getting Wishlist Details", Response = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Getting Wishlist Details Failed" });

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}



   