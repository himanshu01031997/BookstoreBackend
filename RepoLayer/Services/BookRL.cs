using CommonLayer.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
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
        public BookModel UpdateBook(BookModel bookModel)
        {
            SqlConnection sqlConnection = new SqlConnection(this.configuration.GetConnectionString("BookStoreConnection"));

            try
            {

                sqlConnection.Open();
                //string query = $"Update Books Set BookName = '{bookModel.BookName}',AuthoreName = '{bookModel.AuthorName}',BookDescription = '{bookModel.BookDescription}', BookImage = '{bookModel.BookImage}',Rating = '{bookModel.Rating}', TotalPersonRated = '{bookModel.TotalPersonRated}', Quantity = '{bookModel.Quantity}',OriginalPrice = '{bookModel.OriginalPrice}',DiscountPrice = '{bookModel.DiscountPrice}' Where BookId = '{bookModel.BookId}'";
                //SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);



                SqlCommand sqlCommand = new SqlCommand("UpdateBook", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@BookId", bookModel.BookId);
                sqlCommand.Parameters.AddWithValue("@BookName", bookModel.BookName);
                sqlCommand.Parameters.AddWithValue("@AuthoreName", bookModel.AuthorName);
                sqlCommand.Parameters.AddWithValue("@BookDescription", bookModel.BookDescription);
                sqlCommand.Parameters.AddWithValue("@BookImage", bookModel.BookImage);
                sqlCommand.Parameters.AddWithValue("@Rating", bookModel.Rating);
                sqlCommand.Parameters.AddWithValue("@TotalPersonRated", bookModel.TotalPersonRated);
                sqlCommand.Parameters.AddWithValue("@Quantity", bookModel.Quantity);
                sqlCommand.Parameters.AddWithValue("@OriginalPrice", bookModel.OriginalPrice);
                sqlCommand.Parameters.AddWithValue("@DiscountPrice", bookModel.DiscountPrice);
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
        public bool DeleteBook(int BookId)
        {
            SqlConnection sqlConnection = new SqlConnection(this.configuration.GetConnectionString("BookStoreConnection"));

            try
            {
                sqlConnection.Open();
                string query = $"Delete From Books Where BookId={BookId}";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                var result = sqlCommand.ExecuteNonQuery();
                if (result != 0)
                {
                    return true;
                }
                else
                {
                    return false;
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
        public List<BookModel> GetAllBooks()
        {
            List<BookModel> books = new List<BookModel>();
            SqlConnection sqlConnection = new SqlConnection(this.configuration.GetConnectionString("BookStoreConnection"));

            try
            {
                sqlConnection.Open();
                string query = $"Select * From Books";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlDataReader sqlDataReaderreader = sqlCommand.ExecuteReader();
                if (sqlDataReaderreader.HasRows)
                {
                    while (sqlDataReaderreader.Read())
                    {
                        BookModel bookModel = new BookModel()
                        {
                            BookId = sqlDataReaderreader.GetInt32(0),
                            BookName = sqlDataReaderreader.GetString(1),
                            AuthorName = sqlDataReaderreader.GetString(2),
                            BookDescription = sqlDataReaderreader.GetString(3),
                            BookImage = sqlDataReaderreader.GetString(4),
                            Rating = (float)sqlDataReaderreader.GetDouble(5),
                            TotalPersonRated = sqlDataReaderreader.GetInt32(6),
                            Quantity = sqlDataReaderreader.GetInt32(7),
                            OriginalPrice = sqlDataReaderreader.GetDouble(8),
                            DiscountPrice = sqlDataReaderreader.GetDouble(9),
                        };
                        books.Add(bookModel);
                    }
                    return books;
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
        public BookModel GetBookByBookId(int BookId)
        {
            SqlConnection sqlConnection = new SqlConnection(this.configuration.GetConnectionString("BookStoreConnection"));

            try
            {
                sqlConnection.Open();
                string query = $"Select * From Books Where BookId={BookId}";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlDataReader sqlDataReaderreader = sqlCommand.ExecuteReader();
                if (sqlDataReaderreader.HasRows)
                {
                    BookModel bookModel = new BookModel();
                    while (sqlDataReaderreader.Read())
                    {
                        bookModel.BookName = sqlDataReaderreader.GetString(1);
                        bookModel.AuthorName = sqlDataReaderreader.GetString(2);
                        bookModel.BookDescription = sqlDataReaderreader.GetString(3);
                        bookModel.BookImage = sqlDataReaderreader.GetString(4);
                        bookModel.Rating = (float)sqlDataReaderreader.GetDouble(5);
                        bookModel.TotalPersonRated = sqlDataReaderreader.GetInt32(6);
                        bookModel.Quantity = sqlDataReaderreader.GetInt32(7);
                        bookModel.OriginalPrice = sqlDataReaderreader.GetDouble(8);
                        bookModel.DiscountPrice = sqlDataReaderreader.GetDouble(9);
                    }
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
