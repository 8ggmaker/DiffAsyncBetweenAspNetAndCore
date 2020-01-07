using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommonAsync
{
    public enum AwaitCallbackMode
    {
        ExplicitSuppressContext = 0,
        ConfigureAwaitFalse,
        Default
    }
    public class CommonAsyncHelper
    {
        protected static HttpClient httpClient = new HttpClient();

        protected static long count = 0;

        public async Task DoAsyncJobDefault()
        {
            await DoAsyncJobDetail(AwaitCallbackMode.Default).ConfigureAwait(false);
        }

        public async Task DoAsyncJobSuppressContext()
        {
            await DoAsyncJobDetail(AwaitCallbackMode.ExplicitSuppressContext).ConfigureAwait(false);
        }

        public async Task DoAsyncJobConfigureAwaitfalse()
        {
            await DoAsyncJobDetail(AwaitCallbackMode.ConfigureAwaitFalse).ConfigureAwait(false);
        }

        private async Task DoAsyncJobDetail(AwaitCallbackMode mode)
        {
            if(mode== AwaitCallbackMode.ExplicitSuppressContext)
            {
                SynchronizationContext.SetSynchronizationContext(new EmptySynchronizationContext());
                await DoIOJob();
            }
            else if(mode == AwaitCallbackMode.ConfigureAwaitFalse)
            {
                await DoIOJob().ConfigureAwait(false) ;
            }
            else
            {
                await DoIOJob();
            }
            Interlocked.Increment(ref count);
        }

        protected async Task DoIOJob()
        {
            try
            {
                await httpClient.GetAsync("https://www.microsoft.com").ConfigureAwait(false);
            }
            catch
            {

            }
        }


        public long GetCount()
        {
            return count;
        }

        public double DoCalJob()
        {
            double res = 1;
            for(int i = 1; i < 10; i++)
            {
                res = res * i / 5;
            }

            return res;
        }
    }
}
