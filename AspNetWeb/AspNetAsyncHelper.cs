using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonAsync;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;

namespace AspNetWeb
{
    public class AspNetAsyncHelper:CommonAsyncHelper
    {
        /// <summary>
        /// check the sychronizationcontext and httpcontext(isynccontext)
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public async Task DoAsyncJobWithDignosticSyncContext()
        {
            HttpApplication syncContext = HttpContext.Current.ApplicationInstance;
            SynchronizationContext.SetSynchronizationContext(new EmptySynchronizationContext());
            await DoIOJob();
            try
            {
                var type = typeof(HttpApplication);
                var method = type.GetTypeInfo().GetDeclaredMethod("OnThreadEnterPrivate");
                if (method != null)
                {
                    method.Invoke(syncContext, new object[] { true });
                }
            }
            catch(Exception ex) //will throw exception here
            {
                throw ex;
            }
          
            Interlocked.Increment(ref count);
        }
    }
}