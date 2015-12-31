using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anathema
{
    interface IScannerView : IView
    {
        // Methods invoked by the presenter (upstream)

    }

    abstract class IScannerModel : IModel
    {
        private CancellationTokenSource CancelRequest;  // Tells the scan task to cancel (ie finish)
        private Task ScannerTask;                       // Event that constantly checks the target process for changes

        private const Int32 AbortTime = 3000;   // Time to wait (in ms) before giving up when ending scan
        private Int32 WaitTime = 300;           // Time to wait (in ms) for a cancel request between each scan

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
            try { ScannerTask.Wait(AbortTime); }
            catch (AggregateException) { }
        }
    }

    class ScannerPresenter : Presenter<IScannerView, IScannerModel>
    {
        public ScannerPresenter(IScannerView View, IScannerModel Model) : base(View, Model)
        {
            // Bind events triggered by the model

        }
        
        #region Method definitions called by the view (downstream)

        public void BeginScan()
        {
            Model.BeginScan();
        }

        public void EndScan()
        {
            Model.EndScan();
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        #endregion
    }
}
