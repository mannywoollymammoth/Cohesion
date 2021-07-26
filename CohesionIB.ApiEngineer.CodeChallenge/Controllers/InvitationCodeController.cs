using CohesionIB.ApiEngineer.CodeChallenge.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CohesionIB.ApiEngineer.CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvitationCodeController : ControllerBase
    {
        private IInvitationCodeService _invitationCodeService;
        private IDataHandler _dataHandler;

        public InvitationCodeController(IInvitationCodeService invitationCodeService, IDataHandler dataHandler)
        {
            _invitationCodeService = invitationCodeService;
            _dataHandler = dataHandler;
        }

        /// <summary>
        /// Accepts the terms and conditions.
        /// updates user data to reflect that.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("acceptTerms")]
        public IActionResult AcceptTerms()
        {
            string username = User.Identity.Name;
            if (!_dataHandler.getTermsAndConditionsStatus(username))
            {
                _dataHandler.updateTermsAndConditions(username);
                return Ok("terms have been accepted");
            }
            return BadRequest("Terms have already been accepted");
        }

        /// <summary>
        /// returns a invitation code for the user. 
        /// checks if the terms and conditions have been accepted. 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                string username = User.Identity.Name;
                if (!_dataHandler.getTermsAndConditionsStatus(username))
                    return Unauthorized();

                if (!_invitationCodeService.HasCode)
                    return NoContent();
                var invitationCodeObject = new { _invitationCodeService.Code };
                ulong invitationCode = invitationCodeObject.Code;
                _dataHandler.updateInvitationCode(username, invitationCode);
                return Ok(invitationCodeObject);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex);
            }

        }

        /// <summary>
        /// Use an invitation code to assign a device id to a user
        /// </summary>
        /// <param name="code">A invitation code that was returned</param>
        /// <param name="deviceId">The device id to associate with the user.</param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        public IActionResult Put(ulong code, long deviceId)
        {
            try
            {
                string username = User.Identity.Name;
                if (_dataHandler.testIfDeviceIDRegistered(username, deviceId))
                    return StatusCode(409, "Device ID in use");
                if (!_dataHandler.testInvitationCode(code, username))
                    return Unauthorized("bad InviationCode");

                _dataHandler.addDeviceID(username, deviceId);
                _dataHandler.removeInvitationCode(username);
                return Ok();
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex);
            }

        }
    }
}
