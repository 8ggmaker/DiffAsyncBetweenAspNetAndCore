using CommonAsync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace AspNetWeb.Controllers
{
    public partial class ValueController
    {
        /// <summary>
        /// Can increase 'CommonAsyncHelper.Count', because we use 'TaskRun' to run 'CommonAsyncHelper.DoAsyncJobDefault' in ThreadPool,
        /// the current SynchronizationContext is not captured, and the callback ('Interlocked.Increment') of 
        /// the async i/o action ('await httpclient.Getasync') will get invoked in ThreadPool-Thread
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