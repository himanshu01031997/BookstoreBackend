using BusinessLayer.Interface;
using CommonLayer.Model;
using RepoLayer.Interface;
using RepoLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL:IUserBL
    {
        private readonly IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
        ///add user
        public UserModel AddEmployees(UserModel user)
        {
            try
            {
                return this.userRL.AddEmployees(user);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public string Login(string EmailId, string Password)
        {
            try
            {
                return this.userRL.Login(EmailId,Password);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string ForgotPassword(string EmailId)
        {
            try
            {
                return this.userRL.ForgotPassword(EmailId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool ResetPassword(string EmailId, string Password, string ConfirmPassword)
        {
            try
            {
                return this.userRL.ResetPassword(EmailId,Password,ConfirmPassword);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public UserModel GetUserById(int UserId)
        {
            try
            {
                return this.userRL.GetUserById(UserId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<UserModel> GetAllUser()
        {
            try
            {
                return this.userRL.GetAllUser();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public UserTicket CreateTicketForPw(string emailid, string token)
        {

            try
            {
                return this.userRL.CreateTicketForPw(emailid, token);
            }
            catch (Exception e)
            {
                throw e;
            }
        }




    }
}
