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
        /// Can increase 'CommonAsyncHelper.Count', because we use 'ConfigureAwait(false)' to suppress SynchronizationContext,
        /// the callback ('Interlocked.Increment') of the async i/o action ('await httpclient.Getasync') does not need to be invoked in
        /// AspNetSynchronizationContext anymore, it will be dispatched to threadpool and get invoked.
        [HttpGet]
        [Route("api/value/awaitfalse/cal")]
        public double CalWithAwaitfalse()
        {
            double res = CommonAsyncHelper.DoCalJob();
            var _ = CommonAsyncHelper.DoAsyncJobConfigureAwaitfalse();
            return res;
        }
    }
}