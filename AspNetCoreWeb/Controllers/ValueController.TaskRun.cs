using Microsoft.AspNetCore.Mvc;
using CommonAsync;
using System.Threading.Tasks;

namespace AspNetWeb.Controllers
{
    public partial class ValueController
    {
        /// <summary>
        /// Can increase 'CommonAsyncHelper.Count', because there is no await keyword, asp-net-core does not have SynchronizationContext
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/value/taskrun/cal")]
        public double CalWithTaskRun()
        {
            double res = new CommonAsyncHelper().DoCalJob();
            Task.Run(()=>new CommonAsyncHelper().DoAsyncJobDefault());
            return res;
        }
    }
}