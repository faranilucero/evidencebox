using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System;

namespace evidencebox.Controllers
{

    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public JsonResult Get(ControllerContext context)
        {
            var returnDataObj = new Dictionary<string, Dictionary<int, object>>();
            var spd = new StoredProc();

            returnDataObj.Add("AGENCIES", spd.RunStoredProc("evidencebx.AGENCIES_SELECT", null));            
            return Json(returnDataObj);
        }

        // GET api/values/5
        [HttpGet("{agency_guid}")]
        public JsonResult Get(string agency_guid)
        {
            var returnDataObj = new Dictionary<string, Dictionary<int, object>>();
            var spd = new StoredProc();
            var procParams = new Dictionary<string, string>();

            procParams.Add("@AGENCY_GUID", agency_guid);
            returnDataObj.Add("AGENCY_USERS", spd.RunStoredProc("evidencebx.AGENCY_USERS_SELECT", procParams));
            return Json(returnDataObj);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
