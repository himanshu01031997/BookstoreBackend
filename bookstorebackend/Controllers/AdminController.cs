using BusinessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using System;

namespace bookstorebackend.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IAdminBL adminBL;
        public AdminController(IAdminBL adminBL)
        {
            this.adminBL = adminBL;
        }

        [HttpPost("AdminLogin")]
        public ActionResult AdminLogin(string EmailId, string Password)
        {
            try
            {
                var result = adminBL.AdminLogin(EmailId, Password);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Login Successfull", Response = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Login Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
