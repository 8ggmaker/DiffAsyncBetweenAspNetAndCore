using CommonAsync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace AspNetWeb.Controllers
{
    public partial class ValueController
    {
        /// Can increase 'CommonAsyncHelper.Count', because we use 'SynchronizationContext.SetSynchronizationContext' to suppress SynchronizationContext explicitly,
        /// the callback ('Interlocked.Increment') of the async i/o action ('await httpclient.Getasync') does not need to be invoked in
        /// AspNetSynchronizationContext anymore, it will be dispatched to threadpool and get invoked.
        [HttpGet]
        [Route("api/value/suppresssynccontext/cal")]
        public double CalWithSuppressSyncContext()
        {
            double res = new CommonAsyncHelper().DoCalJob();
            var _ = new CommonAsyncHelper().DoAsyncJobSuppressContext();
            return res;
        }
    }
}