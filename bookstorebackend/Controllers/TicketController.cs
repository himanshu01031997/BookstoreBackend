using BusinessLayer.Interface;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace bookstorebackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly IBus bus;
       private readonly IUserBL userbl;
        public TicketController(IBus bus, IUserBL userbl)
        {
            this.bus = bus;
            this.userbl = userbl;
        }
        public async Task<IActionResult> CreateTicket(string  emailid)
        {
            try
            {
                if (emailid != null)
                {
                    var token = userbl.ForgotPassword(emailid);
                    if (!string.IsNullOrEmpty(token))
                    {
                        var ticketresponce = userbl.CreateTicketForPw(emailid, token);
                        Uri uri = new Uri("rabbitmq://localhost/ticketQueue");
                        var endPoint = await bus.GetSendEndpoint(uri);
                        await endPoint.Send(ticketresponce);
                        return Ok(new { success = true, message = "email sent" });



                    }
                    else
                    {
                        return BadRequest(new { success = false, message = "email not sent" });
                    }
                }
                else
                {
                    return BadRequest(new { success = false, message = "something went wrong" });
                }
            }
            catch(Exception e)
            {
                throw e;
            }
           
        }
    }
}
