using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using BusinessLayer.Interface;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace bookstorebackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBL orderbl;
        public OrderController(IOrderBL orderbl)
        {
            this.orderbl = orderbl;
        }

        [HttpPost("Add")]
        public IActionResult AddOrder(OrderModel addOrder)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var res = orderbl.AddOrder(addOrder, userId);
                if (res != null)
                {
                    return Ok(new { success = true, message = res });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Faild to Order" });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
    }
}
