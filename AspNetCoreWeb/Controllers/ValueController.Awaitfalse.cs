using Microsoft.AspNetCore.Mvc;
using CommonAsync;

namespace AspNetCoreWeb.Controllers
{
    public partial class ValueController : ControllerBase
    {
        /// <summary>
        /// Can increase 'CommonAsyncHelper.Count', because there is no await keyword, asp-net-core does not have SynchronizationContext
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