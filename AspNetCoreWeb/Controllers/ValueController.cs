using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CommonAsync;

namespace AspNetCoreWeb.Controllers
{
    [ApiController]
    public partial class ValueController : ControllerBase
    {
        [HttpGet]
        [Route("api/value")]
        public long Get()
        {
            return new CommonAsyncHelper().GetCount();
        }
        /// <summary>
        /// Can increase 'CommonAsyncHelper.Count', because asp-net-core does not have SynchronizationContext
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
