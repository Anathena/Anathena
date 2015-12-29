using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anathema
{
    abstract class Scanner
    {
        private CancellationTokenSource CancelRequest;  // Tells the scan task to cancel (ie finish)
        private Task ScannerTask;                       // Event that constantly checks the target process for changes

        private const Int32 AbortTime = 3000;   // Time to wait (in ms) before giving up when ending scan
        private Int32 WaitTime = 200;           // Time to wait (in ms) for a cancel request between each scan

        public virtual void BeginScan()
        {
            CancelRequest = new CancellationTokenSource();
            ScannerTask = Task.Run(async () =>
            {
                while (true)
                {
                    UpdateScan();

                    // Await with cancellation
                    await Task.Delay(WaitTime, CancelRequest.Token);
                }
            }, CancelRequest.Token);
        }

        protected abstract void UpdateScan();

        public virtual void EndScan()
        {
            // Wait for the filter to finish
            CancelRequest.Cancel();
            try
            {
                ScannerTask.Wait(AbortTime);
            }
            catch (AggregateException)
            {

            }
        }
    }
}