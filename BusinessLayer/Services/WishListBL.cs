using BusinessLayer.Interface;
using CommonLayer.Model;
using RepoLayer.Interface;
using RepoLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class WishListBL:IWishListBL
    {
        private readonly IWishListRL wishrl;

        public WishListBL(IWishListRL wishrl)
        {
            this.wishrl = wishrl;
        }

        public bool AddToWishlist(int UserId, int BookId)
        {
            try
            {
                return this.wishrl.AddToWishlist(UserId, BookId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool DeleteFromWishlist(int UserId, int WishlistId)
        {
            try
            {
                return this.wishrl.DeleteFromWishlist(UserId, WishlistId);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public List<WishListModel> GetWishlisDetails(int UserId)
        {
            try
            {
                return this.wishrl.GetWishlisDetails(UserId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }



    }
}
