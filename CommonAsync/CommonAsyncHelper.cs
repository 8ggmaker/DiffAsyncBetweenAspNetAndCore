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
        private static HttpClient httpClient = new HttpClient();

        private static long count = 0;

        public static async Task DoAsyncJobDefault()
        {
            await DoAsyncJobDetail(AwaitCallbackMode.Default).ConfigureAwait(false);
        }

        public static async Task DoAsyncJobSuppressContext()
        {
            await DoAsyncJobDetail(AwaitCallbackMode.ExplicitSuppressContext).ConfigureAwait(false);
        }

        public static async Task DoAsyncJobConfigureAwaitfalse()
        {
            await DoAsyncJobDetail(AwaitCallbackMode.ConfigureAwaitFalse).ConfigureAwait(false);
        }

        private static async Task DoAsyncJobDetail(AwaitCallbackMode mode)
        {
            if(mode== AwaitCallbackMode.ExplicitSuppressContext)
            {
                SynchronizationContext.SetSynchronizationContext(new EmptySynchronizationContext());
                await httpClient.GetAsync("https://www.microsoft.com");
            }
            else if(mode == AwaitCallbackMode.ConfigureAwaitFalse)
            {
                await httpClient.GetAsync("https://www.microsoft.com").ConfigureAwait(false);
            }
            else
            {
                await httpClient.GetAsync("https://www.microsoft.com");
            }
            Interlocked.Increment(ref count);
        }


        public static long GetCount()
        {
            return count;
        }

        public static double DoCalJob()
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
