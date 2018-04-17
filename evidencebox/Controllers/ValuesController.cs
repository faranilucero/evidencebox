using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace evidencebox.Controllers
{

    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public JsonResult Get(ControllerContext context)
        {
            var spd = new StoredProc();
            var qryResult = spd.RunStoredProc("evidencebx.AGENCIES_SELECT", "AGENCIES", null);
            return Json(qryResult);
        }

        // GET api/values/5
        [HttpGet("{agency_guid}")]
        public JsonResult Get(string agency_guid)
        {
            var spd = new StoredProc();
            var procParams = new Dictionary<string, string>();
            procParams.Add("@AGENCY_GUID",agency_guid);
            var qryResult = spd.RunStoredProc("evidencebx.AGENCY_USERS_SELECT", "AGENCY_USERS", procParams);
            return Json(qryResult);
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
