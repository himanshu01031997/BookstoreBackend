using CommonLayer.Model;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Configuration;
using NLog.Internal;
using RepoLayer.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using NLog.Fluent;

namespace RepoLayer.Services
{
    public class UserRL : IUserRL
    {
        private readonly IConfiguration configuration;
        public static string name;

        public UserRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public UserModel AddEmployees(UserModel user)
        {

            {

                SqlConnection con = new SqlConnection(this.configuration.GetConnectionString("BookStoreConnection"));
                using (con)
                    try
                    {

                        SqlCommand cmd = new SqlCommand("UserRegister", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();

                        cmd.Parameters.AddWithValue("@FullName", user.FullName);
                        cmd.Parameters.AddWithValue("@EmailId", user.EmailId);
                        cmd.Parameters.AddWithValue("@Password", EncryptPassword(user.Password));
                        cmd.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                        var result = cmd.ExecuteNonQuery();

                        if (result != 0)
                        {
                            return user;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    finally
                    {
                        con.Close();
                    }

            }
        }
        public string Login(string EmailId, string Password)
        {
            SqlConnection con = new SqlConnection(this.configuration.GetConnectionString("BookStoreConnection"));
                try
                {
                    con.Open();
                string query = $"Select * From Users Where EmailId='{EmailId}' And Password='{EncryptPassword(Password)}'";
                //var result = Users.Where(u => u.EmailId == EmailId && u.Password == EncryptPassword(Password)).FirstOrDefault();


                SqlCommand sqlCommand = new SqlCommand(query, con);
                    var UserId = Convert.ToInt32(sqlCommand.ExecuteScalar());

                    if (UserId != 0)
                    {
                        var token = GenerateToken(EmailId, UserId);
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
                    con.Close();
                }
        }



        public string GenerateToken(string Email, long UserId)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration[("JWT:Key")]));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                            new Claim(ClaimTypes.Role, "User"),
                            new Claim(ClaimTypes.Email, Email),
                            new Claim("UserId", UserId.ToString())
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
        }
        public string EncryptPassword(string Password)
        {
            try
            {
                byte[] enData_byte = new byte[Password.Length];
                enData_byte = System.Text.Encoding.UTF8.GetBytes(Password);
                string encodedData = Convert.ToBase64String(enData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public string ForgotPassword(string EmailId)
        {
            SqlConnection con = new SqlConnection(this.configuration.GetConnectionString("BookStoreConnection"));

            try
            {
                con.Open();
                string query = $"Select * From Users Where EmailId='{EmailId}'";
                SqlCommand sqlCommand = new SqlCommand(query, con);
                var UserId = Convert.ToInt32(sqlCommand.ExecuteScalar());
                SqlDataReader dr = sqlCommand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        name = dr.GetString(1);
                    }
                }
                if (UserId != 0)
                {
                    var token = GenerateToken(EmailId, UserId);
                    Msmq mSMQ = new Msmq();
                    mSMQ.sendData2Queue(token,EmailId, name);
                    return token;
                }
                return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
        public bool ResetPassword(string EmailId, string Password, string ConfirmPassword)
        {
            SqlConnection con = new SqlConnection(this.configuration.GetConnectionString("BookStoreConnection"));

            try
            {
                con.Open();
                string query = $"Select * From Users Where EmailId='{EmailId}'";
                SqlCommand sqlCommand = new SqlCommand(query, con);
                var UserId = Convert.ToInt32(sqlCommand.ExecuteScalar());
                if (UserId != 0)
                {
                    query = $"Update Users Set Password='{EncryptPassword(Password)}' Where EmailId='{EmailId}'";
                    sqlCommand = new SqlCommand(query, con);
                    sqlCommand.ExecuteNonQuery();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
        public UserModel GetUserById(int UserId)
        {
            SqlConnection sqlConnection = new SqlConnection(this.configuration.GetConnectionString("BookStoreConnection"));

            try
            {
                sqlConnection.Open();
                string query = $"Select * From Users Where UserId={UserId}";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    UserModel model = new UserModel();
                    while (reader.Read())
                    {
                        model.FullName = reader["FullName"].ToString();
                        model.EmailId = reader["EmailId"].ToString();
                        model.Password = DecryptPassword(reader["Password"].ToString());
                        model.PhoneNumber = Convert.ToInt64(reader["PhoneNumber"]);
                    }
                    return model;
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
        }
        public string DecryptPassword(string Password)
        {
            System.Text.UTF8Encoding encoder = new UTF8Encoding();
            System.Text.Decoder decoder = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(Password);
            int charCount = decoder.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decodedchar = new char[charCount];
            decoder.GetChars(todecode_byte, 0, todecode_byte.Length, decodedchar, 0);
            string result = new string(decodedchar);
            return result;
        }


    }
}

