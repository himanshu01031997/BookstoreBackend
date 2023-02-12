using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICartBL
    {
        public bool AddCart(int UserId, int BookId, int Quantity);
        public bool DeleteCart(int UserId, int CartId);
        public bool UpdateCart(int UserId, int CartId, int Quantity);
        public List<CartModel> GetCartDetails(int Userid);




    }
}
