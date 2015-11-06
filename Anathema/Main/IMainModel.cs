using Binarysharp.MemoryManagement;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    public delegate void MainModelHandler<IModel>(IMainModel sender, MainModelEventArgs e);

    public interface IMainModel
    {
        void attach(IMainModelObserver imo);
        void increment();
        void setvalue(int v);
    }

    public interface IMainModelObserver
    {
        void valueIncremented(IMainModel MainModel, MainModelEventArgs e);
    }

    public class MainModelEventArgs : EventArgs
    {
        public int newval;
        public MainModelEventArgs(int v)
        {
            newval = v;
        }
    }

    public class MainModel : IMainModel
    {
        private MemorySharp MemoryEditor;   // Memory editor instance
        public event MainModelHandler<MainModel> changed;
        int value;

        public MainModel()
        {
            value = 0;
        }

        /// <summary>
        /// Update the target process 
        /// </summary>
        /// <param name="TargetProcess"></param>
        public void UpdateTargetProcess(Process TargetProcess)
        {
            // Instantiate a new memory editor with the new target process
            // MemoryEditor = new MemorySharp(TargetProcess);
        }
        public void setvalue(int v)
        {
            value = v;
        }

        public void increment()
        {
            value++;
            changed.Invoke(this, new MainModelEventArgs(value));
        }

        public void attach(IMainModelObserver imo)
        {
            changed += new MainModelHandler<MainModel>(imo.valueIncremented);
        }
    }
}
