using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface IWishListRL
    {
        public bool AddToWishlist(int UserId, int BookId);
        public bool DeleteFromWishlist(int UserId, int WishlistId);
        public List<WishListModel> GetWishlisDetails(int UserId);


    }
}
