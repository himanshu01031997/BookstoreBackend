using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepoLayer.Services
{
    public class AdminRL: IAdminRL
    {


        private static SqlConnection sqlConnection;
        private readonly IConfiguration configuration;

        public AdminRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string AdminLogin(string EmailId, string Password)
        {
            sqlConnection = new SqlConnection(this.configuration.GetConnectionString("BookStoreConnection"));
            //string query = $"Select * From AdminTable Where EmailId='{EmailId}' And Password='{Password}'";

            try
            {
                SqlCommand sqlCommand = new SqlCommand("AdminLogin", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlConnection.Open();
                sqlCommand.Parameters.AddWithValue("@EmailId", EmailId);
                sqlCommand.Parameters.AddWithValue("@Password", Password);

                var result = sqlCommand.ExecuteScalar();

                if (result != null)
                {
                    string query="select AdminId from AdminTable where EmailID='"+ result + "'";
                    SqlCommand cmd = new SqlCommand(query, sqlConnection);
                    var id= Convert.ToInt32(cmd.ExecuteScalar());
                    var token = GenerateToken(EmailId, id);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public string GenerateToken(string Email, int AdminId)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration[("JWT:Key")]));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Role, "Admin"),
                        new Claim(ClaimTypes.Email, Email),
                        new Claim("Admin", AdminId.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(2),
                    SigningCredentials = new SigningCredentials(tokenKey, SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}

