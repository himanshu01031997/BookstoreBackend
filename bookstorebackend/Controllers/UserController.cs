using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Linq;
using System.Security.Claims;

namespace bookstorebackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        //add user
        [HttpPost("Registration")]
        public ActionResult Registration(UserModel userModel)
        {
            try
            {
                var result = userBL.AddEmployees(userModel);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Registration Successfull" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Registration Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost("Login")]
        public ActionResult Login(string EmailId, string Password)
        {
            try
            {
                var result = userBL.Login(EmailId, Password);
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
        
        [HttpPost("ForgetPassword")]
        public ActionResult ForgetPassword(string email)
        {
            try
            {
                var result = userBL.ForgotPassword(email);
                if (result != null)
                {
                    return this.Ok(new { success = true, Message = $"Forget Password success" });
                }
                return this.BadRequest(new { success = false, Messsage = $"forget password can not work" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [HttpPost("ResetPassword")]
        public ActionResult ResetPassword(string Password, string ConfirmPassword)
        {
            var EmailId = User.FindFirst(ClaimTypes.Email).Value.ToString();
            var result = userBL.ResetPassword(EmailId, Password, ConfirmPassword);
            if (result)
            {
                return Ok(new { success = true, message = "Password Reset SuccessFull" });
            }
            else
            {
                return BadRequest(new { success = false, message = "Password Resetting Failed" });
            }
        }
        [HttpGet("GetUserById")]
        public ActionResult GetUserById(int UserId)
        {
            try
            {
                var result = userBL.GetUserById(UserId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Data retive Successfully", Response = result });
                }
                else
                {
                    return Ok(new { success = false, message = "Didnt get data" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
