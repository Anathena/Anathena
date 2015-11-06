using Binarysharp.MemoryManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    public interface IBenedictionController
    {
        void UpdateProcess(MemorySharp MemoryEditor);
    }

    class BenedictionController : IBenedictionController
    {
        IBenedictionView BenedictionView;
        IBenedictionModel BenedictionModel;

        public BenedictionController(IBenedictionView BenedictionView, IBenedictionModel BenedictionModel)
        {
            this.BenedictionView = BenedictionView;
            this.BenedictionModel = BenedictionModel;

            BenedictionModel.Attach((IBenedictionModelObserver)BenedictionView);
            BenedictionView.SetBenedictionController(this);
            BenedictionView.UpdateTargetProcess += new BenedictionViewHandler<IBenedictionView>(this.view_changed);
        }

        public void view_changed(IBenedictionView v, BenedictionViewEventArgs e)
        {
            
        }

        public void UpdateProcess(MemorySharp MemoryEditor)
        {
            BenedictionModel.UpdateProcess(MemoryEditor);
        }
    }
}
