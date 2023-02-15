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
    public class OrderRL:IOrderRL
    {
        private readonly IConfiguration configuration;

        public OrderRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        //public OrderModel AddOrder(int UserId, OrderModel orderModel)
        //{

        //    try
        //    {
        //        sqlConnection.Open();
        //        SqlCommand sqlCommand = new SqlCommand("AddOrder", sqlConnection)
        //        {
        //            CommandType = CommandType.StoredProcedure
        //        };
        //        sqlCommand.Parameters.AddWithValue("@UserId", UserId);
        //        sqlCommand.Parameters.AddWithValue("@BookId", orderModel.BookId);
        //        sqlCommand.Parameters.AddWithValue("@AddressId", orderModel.AddressId);
        //        int result = sqlCommand.ExecuteNonQuery();
        //        if (result != 0)
        //        {
        //            return orderModel;
        //        }
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public string AddOrder(OrderModel addOrder, int userId)
        {
            SqlConnection con = new SqlConnection(this.configuration.GetConnectionString("BookStoreConnection"));

            using (con)
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                try
                {
                    List<Cartresponce> cartList = new List<Cartresponce>();
                    List<string> orderList = new List<string>();

                    cmd = new SqlCommand("spGetAllCart", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Cartresponce cart = new Cartresponce();
                            cart.UserId = Convert.ToInt32(reader["UserId"]);
                            cart.BookId = Convert.ToInt32(reader["BookId"]);
                            cartList.Add(cart);
                        }
                        reader.Close();

                        foreach (var cart in cartList)
                        {
                            cmd = new SqlCommand("spAddOrders", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@BookId", cart.BookId);
                            cmd.Parameters.AddWithValue("@UserId", userId);
                            cmd.Parameters.AddWithValue("@AddressId", addOrder.AddressId);
                            int result = Convert.ToInt32(cmd.ExecuteScalar());
                            if (result != 2 && result != 3 && result != 4)
                            {
                                orderList.Add("Item Added to OrderList");
                            }
                            else
                            {
                                return null;
                            }
                        }
                        con.Close();
                        return "Congratulations! Order Placed Successfully";
                    }
                    else
                        return null;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


    }
}
