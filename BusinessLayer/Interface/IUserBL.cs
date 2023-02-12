using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        public UserModel AddEmployees(UserModel user);
        public string Login(string EmailId, string Password);
        public string ForgotPassword(string EmailId);
        public bool ResetPassword(string EmailId, string Password, string ConfirmPassword);

        public UserModel GetUserById(int UserId);
        public List<UserModel> GetAllUser();


    }
}
