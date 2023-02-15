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
    public class AddressRL:IAddressRL
    {
        private readonly IConfiguration configuration;

        public AddressRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public bool AddAddress(int UserId, AddressModel addressModel)
        {
            SqlConnection sqlConnection = new SqlConnection(this.configuration.GetConnectionString("BookStoreConnection"));

            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("AddAddress", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@FullAddress", addressModel.FullAddress);
                sqlCommand.Parameters.AddWithValue("@City", addressModel.City);
                sqlCommand.Parameters.AddWithValue("@State", addressModel.State);
                sqlCommand.Parameters.AddWithValue("@TypeId", addressModel.TypeId);
                sqlCommand.Parameters.AddWithValue("@UserId", UserId);
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
        }
        public bool UpdateAddress(int UserId, AddressModel addressModel)
        {
            SqlConnection sqlConnection = new SqlConnection(this.configuration.GetConnectionString("BookStoreConnection"));

            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("UpdateAddress", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@AddressId", addressModel.AddressId);
                sqlCommand.Parameters.AddWithValue("@FullAddress", addressModel.FullAddress);
                sqlCommand.Parameters.AddWithValue("@City", addressModel.City);
                sqlCommand.Parameters.AddWithValue("@State", addressModel.State);
                sqlCommand.Parameters.AddWithValue("@TypeId", addressModel.TypeId);
                sqlCommand.Parameters.AddWithValue("@UserId", UserId);
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
        }
        public bool DeleteAddress(int UserId, int AddressId)
        {
            SqlConnection sqlConnection = new SqlConnection(this.configuration.GetConnectionString("BookStoreConnection"));

            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("DeleteAddress", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@AddressId", AddressId);
                sqlCommand.Parameters.AddWithValue("@UserId", UserId);
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
        }
        public List<AddressModel> GetAllAddress(int UserId)
        {
            SqlConnection sqlConnection = new SqlConnection(this.configuration.GetConnectionString("BookStoreConnection"));

            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("GetAllAddress", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@UserId", UserId);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.HasRows)
                {
                    List<AddressModel> addressModels = new List<AddressModel>();
                    while (sqlDataReader.Read())
                    {
                        AddressModel address = new AddressModel();
                        AddressTypeModel Type = new AddressTypeModel();
                        address.AddressId = Convert.ToInt32(sqlDataReader["AddressId"]);
                        address.FullAddress = sqlDataReader["FullAddress"].ToString();
                        address.City = sqlDataReader["City"].ToString();
                        address.State = sqlDataReader["State"].ToString();
                        address.UserId = Convert.ToInt32(sqlDataReader["UserId"]);
                        //address.addressType = Type;
                        Type.TypeId = Convert.ToInt32(sqlDataReader["TypeId"]);
                        addressModels.Add(address);
                    }
                    return addressModels;
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
