﻿using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using BusinessLayer.Interface;

namespace bookstorebackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressBL addressbl;
        public AddressController(IAddressBL addressbl)
        {
            this.addressbl = addressbl;
        }

        [Authorize]
        [HttpPost("AddAddress")]
        public ActionResult AddAddress(AddressModel addressModel)
        {
            try
            {
                var UserId = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "UserId").Value);
                var result = addressbl.AddAddress(UserId, addressModel);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Address Added Successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Address Adding Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpPut("UpdateAddress")]
        public ActionResult UpdateAddress(AddressModel addressModel)
        {
            try
            {
                var UserId = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "UserId").Value);
                var result = addressbl.UpdateAddress(UserId, addressModel);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Address Updated Successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Address Updating Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpDelete("DeleteAddress")]
        public ActionResult DeleteAddress(int AddressId)
        {
            try
            {
                var UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = addressbl.DeleteAddress(UserId, AddressId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Address Deleted Successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Address Deletion Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpGet("GetAllAddress")]
        public ActionResult GetAllAddress()
        {
            try
            {
                var UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = addressbl.GetAllAddress(UserId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Getting All Address", Response = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Failed For Getting Address" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
