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
        public async Task DoAsyncJobWithDiagnosticSyncContext()
        {
            HttpApplication syncContext = HttpContext.Current.ApplicationInstance;
            SynchronizationContext.SetSynchronizationContext(new AspNetSynchronizationContextSimulator(syncContext));
            await DoIOJob();                   
            Interlocked.Increment(ref count);
        }

        internal class AspNetSynchronizationContextSimulator : SynchronizationContext
        {
            /// <summary>
            /// HttpApplication implements ISyncContext, which is internal
            /// https://referencesource.microsoft.com/#System.Web/Util/ISyncContext.cs
            /// </summary>
            private HttpApplication syncContext;
            public AspNetSynchronizationContextSimulator(HttpApplication syncContext)
            {
                this.syncContext = syncContext;
            }

            public override void Post(SendOrPostCallback d, object state)
            {
                // simulate what AspNetSynchronizationContext does when call await callback
                // try to enter current synccontext to ensure only one callback get executed at each time,
                // https://referencesource.microsoft.com/#System.Web/Util/SynchronizationHelper.cs,157
                // but when response has returned, ISyncContex.Enter will raise an exception and cause callback can not get invoked properly
                try
                {
                    var type = typeof(HttpApplication);
                    var method = type.GetTypeInfo().GetDeclaredMethod("OnThreadEnterPrivate");
                    if (method != null)
                    {
                        method.Invoke(syncContext, new object[] { true }); // throw exception here
                    }

                    d(state); // can not get executed
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
        }
    }


}