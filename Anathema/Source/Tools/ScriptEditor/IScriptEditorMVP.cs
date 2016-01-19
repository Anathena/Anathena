using Binarysharp.MemoryManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    delegate void ScriptEditorEventHandler(Object Sender, PointerScannerEventArgs Args);
    class ScriptEditorEventArgs : EventArgs
    {
        public String ProcessTitle = String.Empty;
    }

    interface IScriptEditorView : IView
    {
        // Methods invoked by the presenter (upstream)
    }

    interface IScriptEditorModel : IModel, IProcessObserver
    {
        // Events triggered by the model (upstream)
        event ScriptEditorEventHandler EventUpdateProcessTitle;

        // Functions invoked by presenter (downstream)
        void SaveScript(String ScriptText);
    }

    class ScriptEditorPresenter : Presenter<IScriptEditorView, IScriptEditorModel>
    {
        public ScriptEditorPresenter(IScriptEditorView View, IScriptEditorModel Model) : base(View, Model)
        {
            // Bind events triggered by the model
        }

        #region Method definitions called by the view (downstream)

        #endregion

        #region Event definitions for events triggered by the model (upstream)
        
        public void SaveScript(String ScriptText)
        {
            Model.SaveScript(ScriptText);
        }

        #endregion
    }
}
