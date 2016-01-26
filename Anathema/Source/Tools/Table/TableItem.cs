using Binarysharp.MemoryManagement;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;

namespace Anathema
{
    [DataContract()]
    public class TableItem
    {
        [DataMember()]
        public Int32 TextColorARGB { get; set; }
        
        public Color TextColor { get { return Color.FromArgb(TextColorARGB); } set { TextColorARGB = value.ToArgb(); } }

        protected Boolean Activated;

        public TableItem()
        {
            TextColor = Color.Black;
            Activated = false;
        }

        public virtual void SetActivationState(Boolean Activated)
        {
            this.Activated = Activated;
        }

        public Boolean GetActivationState()
        {
            return Activated;
        }

    } // End class

} // End namespace