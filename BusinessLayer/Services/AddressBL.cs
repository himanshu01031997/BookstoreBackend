using BusinessLayer.Interface;
using CommonLayer.Model;
using RepoLayer.Interface;
using RepoLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class AddressBL:IAddressBL
    {
        private readonly IAddressRL addressrl;

        public AddressBL(IAddressRL addressrl)
        {
            this.addressrl = addressrl;
        }
        
        public bool AddAddress(int UserId, AddressModel addressModel)
        {
            try
            {
                return this.addressrl.AddAddress(UserId, addressModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool UpdateAddress(int UserId, AddressModel addressModel)
        {
            try
            {
                return this.addressrl.UpdateAddress(UserId, addressModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool DeleteAddress(int UserId, int AddressId)
        {
            try
            {
                return this.addressrl.DeleteAddress(UserId, AddressId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<AddressModel> GetAllAddress(int UserId)
        {
            try
            {
                return this.addressrl.GetAllAddress(UserId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
