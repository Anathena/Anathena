using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    public interface IBenedictionModel
    {
        void attach(IBenedictionModelObserver imo);
        void increment();
        void setvalue(int v);
    }

    public delegate void BenedictionModelHandler<IModel>(IBenedictionModel sender, BenedictionModelEventArgs e);

    public class BenedictionModelEventArgs : EventArgs
    {
        public int newval;
        public BenedictionModelEventArgs(int v)
        {
            newval = v;
        }
    }

    public interface IBenedictionModelObserver
    {
        void valueIncremented(IBenedictionModel model, BenedictionModelEventArgs e);
    }

    public class BenedictionModel : IBenedictionModel
    {
        public event BenedictionModelHandler<BenedictionModel> changed;
        int value;

        public BenedictionModel()
        {
            value = 0;
        }

        public void setvalue(int v)
        {
            value = v;
        }

        public void increment()
        {
            value++;
            changed.Invoke(this, new BenedictionModelEventArgs(value));
        }

        public void attach(IBenedictionModelObserver imo)
        {
            changed += new BenedictionModelHandler<BenedictionModel>(imo.valueIncremented);
        }
    }
}
