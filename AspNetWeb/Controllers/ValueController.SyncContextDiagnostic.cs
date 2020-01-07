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
        [Route("api/value/diagnostic/cal")]
        public double CalWithDiagnostic()
        {
            double res = new AspNetAsyncHelper().DoCalJob();
            var _ = new AspNetAsyncHelper().DoAsyncJobWithDiagnosticSyncContext();
            return res;
        }
    }
}