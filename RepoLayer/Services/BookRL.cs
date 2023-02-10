using CommonLayer.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Services
{
    public class BookRL:IBookRL
    {
        private readonly IConfiguration configuration;

        public BookRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public BookModel AddBook(BookModel bookModel)
        {
            SqlConnection sqlConnection = new SqlConnection(this.configuration.GetConnectionString("BookStoreConnection"));

            try
            {
                sqlConnection.Open();
                string query = $"Insert Into Books Values('{bookModel.BookName}','{bookModel.AuthorName}'," +
                    $"'{bookModel.BookDescription}','{bookModel.BookImage}',{bookModel.Rating},{bookModel.TotalPersonRated}," +
                    $"{bookModel.Quantity},{bookModel.OriginalPrice},{bookModel.DiscountPrice})";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                var result = sqlCommand.ExecuteNonQuery();
                if (result != 0)
                {
                    return bookModel;
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
}
