using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace AspNetWeb.Controllers
{
    public partial class ValueController
    {
        [HttpGet]
        [Route("api/value/dignostic/cal")]
        public double CalWithDignostic()
        {
            double res = new AspNetAsyncHelper().DoCalJob();
            var _ = new AspNetAsyncHelper().DoAsyncJobWithDignosticSyncContext();
            return res;
        }
    }
}