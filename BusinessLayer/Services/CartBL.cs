using BusinessLayer.Interface;
using CommonLayer.Model;
using RepoLayer.Interface;
using RepoLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CartBL:ICartBL
    {
        private readonly ICartRL cartrl;

        public CartBL(ICartRL cartrl)
        {
            this.cartrl = cartrl;
        }
        public bool AddCart(int UserId, int BookId, int Quantity)
        {
            try
            {
                return this.cartrl.AddCart(UserId,BookId,Quantity);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool DeleteCart(int UserId, int CartId)
        {
            try
            {
                return this.cartrl.DeleteCart(UserId, CartId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool UpdateCart(int UserId, int CartId, int Quantity)
        {
            try
            {
                return this.cartrl.UpdateCart(UserId, CartId, Quantity);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<CartModel> GetCartDetails(int Userid)
        {
            try
            {
                return this.cartrl.GetCartDetails(Userid);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
