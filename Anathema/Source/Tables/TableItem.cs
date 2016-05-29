using System;
using System.Drawing;
using System.Reflection;
using System.Runtime.Serialization;

namespace Anathema.Source.Tables
{
    [Obfuscation(ApplyToMembers = false)]
    [Obfuscation(Exclude = true)]
    [DataContract()]
    public class TableItem
    {
        [Obfuscation(Exclude = true)]
        [DataMember()]
        public Int32 TextColorARGB
        {
            [Obfuscation(Exclude = true)]
            get;
            [Obfuscation(Exclude = true)]
            set;
        }

        [Obfuscation(Exclude = true)]
        public Color TextColor
        {
            [Obfuscation(Exclude = true)]
            get { return Color.FromArgb(TextColorARGB); }
            [Obfuscation(Exclude = true)]
            set { TextColorARGB = value.ToArgb(); }
        }

        [Obfuscation(Exclude = true)]
        protected Boolean Activated;

        public TableItem()
        {
            TextColor = Color.Black;
            Activated = false;
        }

        [Obfuscation(Exclude = true)]
        public virtual void SetActivationState(Boolean Activated)
        {
            this.Activated = Activated;
        }

        [Obfuscation(Exclude = true)]
        public Boolean GetActivationState()
        {
            return Activated;
        }

    } // End class

} // End namespace