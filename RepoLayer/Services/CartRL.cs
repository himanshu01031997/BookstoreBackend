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
    public class CartRL:ICartRL
    {
        private readonly IConfiguration configuration;

        public CartRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public bool AddCart(int UserId, int BookId, int Quantity)
        {
            SqlConnection sqlConnection = new SqlConnection(this.configuration.GetConnectionString("BookStoreConnection"));

            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("AddCart", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Quantity", Quantity);
                sqlCommand.Parameters.AddWithValue("@UserId", UserId);
                sqlCommand.Parameters.AddWithValue("@BookId", BookId);

                int result = sqlCommand.ExecuteNonQuery();
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
        public bool DeleteCart(int UserId, int CartId)
        {
            SqlConnection sqlConnection = new SqlConnection(this.configuration.GetConnectionString("BookStoreConnection"));

            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("DeleteCart", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@UserId", UserId);
                sqlCommand.Parameters.AddWithValue("@CartId", CartId);

                int result = sqlCommand.ExecuteNonQuery();
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
        public bool UpdateCart(int UserId, int CartId, int Quantity)
        {
            SqlConnection sqlConnection = new SqlConnection(this.configuration.GetConnectionString("BookStoreConnection"));

            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("UpdateCart", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Quantity", Quantity);
                sqlCommand.Parameters.AddWithValue("@UserId", UserId);
                sqlCommand.Parameters.AddWithValue("@CartId", CartId);

                int result = sqlCommand.ExecuteNonQuery();
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

        public List<CartModel> GetCartDetails(int Userid)
        {
            SqlConnection sqlConnection = new SqlConnection(this.configuration.GetConnectionString("BookStoreConnection"));

            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("GetCartByUserId", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Userid", Userid);

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.HasRows)
                {
                    List<CartModel> cartDetails = new List<CartModel>();
                    while (sqlDataReader.Read())
                    {
                        BookModel bookModel = new BookModel();
                        CartModel cartModel = new CartModel();
                        bookModel.BookName = sqlDataReader["BookName"].ToString();
                        bookModel.AuthorName = sqlDataReader["AuthoreName"].ToString();
                        bookModel.DiscountPrice = Convert.ToDouble(sqlDataReader["DiscountPrice"]);
                        bookModel.OriginalPrice = Convert.ToDouble(sqlDataReader["OriginalPrice"]);
                        bookModel.BookImage = sqlDataReader["BookImage"].ToString();
                        cartModel.CartId = Convert.ToInt32(sqlDataReader["CartId"]);
                        cartModel.UserId = Convert.ToInt32(sqlDataReader["UserId"]);
                        cartModel.BookId = Convert.ToInt32(sqlDataReader["BookId"]);
                        cartModel.Quantity = Convert.ToInt32(sqlDataReader["Quantity"]);
                        cartModel.bookModel = bookModel;
                        cartDetails.Add(cartModel);
                    }
                    return cartDetails;
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
