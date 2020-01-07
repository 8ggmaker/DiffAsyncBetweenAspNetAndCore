using CommonAsync;
using System.Web.Http;

namespace AspNetWeb.Controllers
{
    public partial class ValueController
    {
        /// <summary>
        /// Can increase 'CommonAsyncHelper.Count', because we use 'ConfigureAwait(false)' to suppress SynchronizationContext,
        /// the callback ('Interlocked.Increment') of the async i/o action ('await httpclient.Getasync') does not need to be invoked in
        /// AspNetSynchronizationContext anymore, it will be dispatched to threadpool and get invoked.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/value/awaitfalse/cal")]
        public double CalWithAwaitfalse()
        {
            double res = new CommonAsyncHelper().DoCalJob();
            var _ = new CommonAsyncHelper().DoAsyncJobConfigureAwaitfalse();
            return res;
        }
    }
}