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
    public class WishListRL:IWishListRL
    {
        private readonly IConfiguration configuration;

        public WishListRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool AddToWishlist(int UserId, int BookId)
        {
            SqlConnection sqlConnection = new SqlConnection(this.configuration.GetConnectionString("BookStoreConnection"));

            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("AddInWishlist", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@UserId", UserId);
                sqlCommand.Parameters.AddWithValue("@BookId", BookId);
                int result = sqlCommand.ExecuteNonQuery();
                if (result != 0)
                {
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
                sqlConnection.Close();
            }
        }
        public bool DeleteFromWishlist(int UserId, int WishlistId)
        {
            SqlConnection sqlConnection = new SqlConnection(this.configuration.GetConnectionString("BookStoreConnection"));

            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("DeleteFromWishlist", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@UserId", UserId);
                sqlCommand.Parameters.AddWithValue("@WishlistId", WishlistId);

                int result = sqlCommand.ExecuteNonQuery();
                if (result != 0)
                {
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
                sqlConnection.Close();
            }
        }
        public List<WishListModel> GetWishlisDetails(int UserId)
        {
            SqlConnection sqlConnection = new SqlConnection(this.configuration.GetConnectionString("BookStoreConnection"));

            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("GetAllRecordFromWishlist", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("UserId", UserId);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.HasRows)
                {
                    List<WishListModel> wishlistDetails = new List<WishListModel>();
                    while (sqlDataReader.Read())
                    {
                        BookModel book = new BookModel();
                        WishListModel wishlist = new WishListModel();
                        book.BookName = sqlDataReader["BookName"].ToString();
                        book.AuthorName = sqlDataReader["AuthoreName"].ToString();
                        book.DiscountPrice = Convert.ToDouble(sqlDataReader["DiscountPrice"]);
                        book.OriginalPrice = Convert.ToDouble(sqlDataReader["OriginalPrice"]);
                        book.BookImage = sqlDataReader["BookImage"].ToString();
                        wishlist.WishlistId = Convert.ToInt32(sqlDataReader["WishlistId"]);
                        wishlist.BookId = Convert.ToInt32(sqlDataReader["BookId"]);
                        wishlist.UserId = Convert.ToInt32(sqlDataReader["UserId"]);
                        wishlist.bookModel = book;
                        wishlistDetails.Add(wishlist);
                    }
                    return wishlistDetails;
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

