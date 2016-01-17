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

    abstract class IScannerModel : RepeatedTask, IModel
    {
        public override void Begin()
        {
            WaitTime = Settings.GetInstance().GetRescanInterval();
            base.Begin();
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
            Model.Begin();
        }

        public void EndScan()
        {
            Model.End();
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        #endregion
    }
}
