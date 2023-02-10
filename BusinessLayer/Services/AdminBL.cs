using BusinessLayer.Interface;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class AdminBL:IAdminBL
    {
        private readonly IAdminRL adminRL;
        public AdminBL(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }
        public string AdminLogin(string EmailId, string Password)
        {
            try
            {
                return this.adminRL.AdminLogin(EmailId, Password);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
