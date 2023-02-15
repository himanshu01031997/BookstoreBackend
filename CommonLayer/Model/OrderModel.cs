using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public int TotalPrice { get; set; }
        public int BookQuantity { get; set; }
        public DateTime OrderDate { get; set; }
        public int UserId { get; set; }
         public int AddressId { get; set; }
        public int CartId { get; set; }
        public BookModel bookModel { get; set; }
    }
}
