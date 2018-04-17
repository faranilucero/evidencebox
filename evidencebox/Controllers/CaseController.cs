using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Features;


namespace evidencebox.Controllers
{
    [Route("api/[controller]")]
    public class CaseController : Controller
    {

        // GET api/case/{sessionGuid}
        [HttpGet("{sessionGuid}")]
        public JsonResult Get(string sessionGuid)
        {
            var returnDataObj = new Dictionary<string, Dictionary<string, object>>();
            var qryResult = new Dictionary<string, object>();
            var spd = new StoredProc();
            var procParams = new Dictionary<string, string>();
            string transactionUserIP = Request.HttpContext.Connection.RemoteIpAddress.ToString();

            procParams.Add("@SESSION_GUID", sessionGuid);
            qryResult = spd.RunStoredProc("evidencebx.USERS_SELECT_SELF", procParams);

            return Json(qryResult.);

            // GET SESSION INFO


            /*
            string userStamp = string.Empty;
            using (sakuraDbEntities _db = new sakuraDbEntities())
            {
                ObjectResult<USERS_SELECT_SELF_Result> userResults = _db.USERS_SELECT_SELF(sessionGuid);
                foreach (var user in userResults)
                {
                    userStamp = user.USER_STAMP;
                }
            }
            */

        }
    }
}
