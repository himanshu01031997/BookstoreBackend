using CommonLayer.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Text;

namespace RepoLayer.Services
{
    public class FeedbackRL:IFeedbackRL
    {
        private readonly IConfiguration configuration;

        public FeedbackRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
       
        public FeedbackModel AddFeedback(FeedbackModel addFeedback, int userId)
        {
            SqlConnection sqlConnection = new SqlConnection(this.configuration.GetConnectionString("BookStoreConnection"));
            using (sqlConnection)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("AddFeedback", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();

                    cmd.Parameters.AddWithValue("@Rating", addFeedback.Rating);
                    cmd.Parameters.AddWithValue("@Comment", addFeedback.Comment);
                    cmd.Parameters.AddWithValue("@BookId", addFeedback.BookId);
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    int result = cmd.ExecuteNonQuery();
                    if (result != 0)
                    {
                        return addFeedback;
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
        }
        public List<FeedbackModel> GetAllFeedbacks(int bookId)
        {
            SqlConnection sqlConnection = new SqlConnection(this.configuration.GetConnectionString("BookStoreConnection"));

            //using (sqlConnection)
            //{
                //try
                //{
                //    List<FeedbackModel> feedbackResponse = new List<FeedbackModel>();
                //    SqlCommand cmd = new SqlCommand("GetAllFeedback", sqlConnection);
                //    cmd.CommandType = CommandType.StoredProcedure;
                //    cmd.Parameters.AddWithValue("@BookId", bookId);

                //    sqlConnection.Open();
                //    SqlDataReader rdr = cmd.ExecuteReader();

                //    if (rdr.HasRows)
                //    {
                //        while (rdr.Read())
                //        {
                //            FeedbackResponse feedback = new FeedbackResponse();
                //            feedback.FeedbackId = Convert.ToInt32(rdr["FeedbackId"]);
                //            feedback.BookId = Convert.ToInt32(rdr["BookId"]);
                //            feedback.UserId = Convert.ToInt32(rdr["UserId"]);
                //            feedback.Comment = Convert.ToString(rdr["Comment"]);
                //            feedback.Rating = Convert.ToInt32(rdr["Rating"]);
                //            feedback.FullName = Convert.ToString(rdr["FullName"]);
                //            feedbackResponse.Add(feedback);
                //        }
                //        con.Close();
                //        return feedbackResponse;
                //    }
                //    else
                //    {
                //        con.Close();
                //        return null;
                //    }
                //}
                //catch (Exception ex)
                //{
                //    throw ex;
                //}
                try
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand("GetAllFeedback", sqlConnection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@BookId", bookId);
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        List<FeedbackModel> feedbackModel = new List<FeedbackModel>();
                        while (sqlDataReader.Read())
                        {
                            FeedbackModel feedback = new FeedbackModel();
                            UserModel user = new UserModel();
                            feedback.FeedbackId = Convert.ToInt32(sqlDataReader["FeedbackId"]);
                            feedback.Comment = sqlDataReader["Comment"].ToString();
                            feedback.Rating = Convert.ToInt32(sqlDataReader["Rating"]);
                            feedback.BookId = Convert.ToInt32(sqlDataReader["BookId"]);
                            user.FullName = sqlDataReader["FullName"].ToString();
                            feedback.UserModel = user;
                            feedbackModel.Add(feedback);
                        }
                        return feedbackModel;
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
        }
    }
