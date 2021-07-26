using CohesionIB.ApiEngineer.CodeChallenge.DAL;
using CohesionIB.ApiEngineer.CodeChallenge.Models;
using CohesionIB.ApiEngineer.CodeChallenge.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CohesionIB.ApiEngineer.CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeviceController : ControllerBase
    {
        private IDataHandler _dataHandler;
        public DeviceController(IDataHandler dataHandler)
        {
            _dataHandler = dataHandler;
        }

        /// <summary>
        /// gets the device list of the user 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            string username = User.Identity.Name;
            var deviceList = _dataHandler.getDeviceList(username);
            Dictionary<string, List<long>> returnObject = new Dictionary<string, List<long>>();
            returnObject["deviceList"] = deviceList;
            return Ok(returnObject);
        }
    }
}
