using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IWishListBL
    {
        public bool AddToWishlist(int UserId, int BookId);
        public bool DeleteFromWishlist(int UserId, int WishlistId);
        public List<WishListModel> GetWishlisDetails(int UserId);


    }
}
