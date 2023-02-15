using BusinessLayer.Interface;
using CommonLayer.Model;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class OrderBL:IOrderBL
    {
        private readonly IOrderRL orderrl;

        public OrderBL(IOrderRL orderrl)
        {
            this.orderrl = orderrl;
        }

        public string AddOrder(OrderModel addOrder, int userId)
        {
            try
            {
                return this.orderrl.AddOrder(addOrder, userId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
