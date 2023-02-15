using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IAddressBL
    {
        public bool AddAddress(int UserId, AddressModel addressModel);
        public bool UpdateAddress(int UserId, AddressModel addressModel);
        public bool DeleteAddress(int UserId, int AddressId);
        public List<AddressModel> GetAllAddress(int UserId);




    }
}
