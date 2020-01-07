using CommonAsync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AspNetWeb.Controllers
{
    public partial class ValueController : ApiController
    {
        [HttpGet]
        [Route("api/value")]
        public long Get()
        {
            return new CommonAsyncHelper().GetCount();
        }
        /// <summary>
        /// Can not increase 'CommonAsyncHelper.Count', because there is no await keyword, 
        /// code will continue to execute before 'CommonAsyncHelper.DoAsyncJobDefault' finished,
        /// and after the 'return res' statement, the AspNetSynchronizationContext does not exist any more, 
        /// the callback ('Interlocked.Increment') of the async i/o action ('await httpclient.Getasync') can not be invoked.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/value/cal")]
        public double Cal()
        {
            double res = new CommonAsyncHelper().DoCalJob();
            var _ = new CommonAsyncHelper().DoAsyncJobDefault();
            return res;
        }
       
    }
}