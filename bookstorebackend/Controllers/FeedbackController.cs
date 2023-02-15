using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using BusinessLayer.Services;

namespace bookstorebackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController: ControllerBase
    {
        public IFeedbackBL feedbl;
        public FeedbackController(IFeedbackBL feedbl)
        {
            this.feedbl = feedbl;
       
        //public ActionResult AddFeedback(int BookId, FeedbackModel feedbackModel)
        //{
        //    try
        //    {
        //        var UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
        //        var result = feedbl.AddFeedback(UserId, BookId, feedbackModel);
        //        if (result != null)
        //        {
        //            return this.Ok(new { success = true, message = "Feedback Added Successfully", Response = result });
        //        }
        //        else
        //        {
        //            return this.BadRequest(new { success = false, message = "Feedback Adding Failed" });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        }
        [Authorize]
        [HttpPost("AddFeedback")]
        public IActionResult AddFeedback(FeedbackModel addFeedback)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var res = feedbl.AddFeedback(addFeedback, userId);
                if (res != null)
                {
                    return Ok(new { success = true, message = "Feedback Added sucessfully", data = res });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Faild to Add Feedback" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet("GetAllFeedback")]
        public ActionResult GetAllFeedback(int BookId)
        {
            try
            {
                var result = feedbl.GetAllFeedbacks(BookId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Getting All Feedbacks", Response = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = " Getting Feedbacks Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}


