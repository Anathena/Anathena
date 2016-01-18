using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;

namespace Anathema
{
    [DataContract()]
    public class ScriptItem : TableItem
    {
        [DataMember()]
        public String Script { get; set; }

        public ScriptItem(String Script)
        {
            this.Script = Script;
        }
    }

    [DataContract()]
    public class AddressItem : TableItem
    {
        [DataMember()]
        public IntPtr Address { get; set; }
        
        [DataMember()]
        public Int32[] Offsets { get; set; }

        [DataMember()]
        public Type ElementType { get; set; }

        private dynamic _Value;
        public dynamic Value { get { return _Value; } set { if (!Activated) _Value = value; } }

        public AddressItem(IntPtr Address, Type ElementType)
        {
            this.Address = Address;
            this.ElementType = ElementType;
            Offsets = null;
        }

        public AddressItem(IntPtr Address, String Description, Type ElementType, Int32[] Offsets)
        {
            this.Address = Address;
            this.Description = Description;
            this.ElementType = ElementType;
            this.Offsets = Offsets;
        }

        public void ForceUpdateValue(dynamic Value)
        {
            if (Value != null)
                this._Value = Value;
        }
    }

    [DataContract()]
    public class TableItem
    {
        [DataMember()]
        public Color TextColor { get; set; }

        [DataMember()]
        public String Description { get; set; }

        protected Boolean Activated;

        public TableItem()
        {
            TextColor = Color.Black;
            Description = String.Empty;
            Activated = false;
        }

        public TableItem(String Description)
        {
            TextColor = Color.Black;
            this.Description = Description;
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
    }
}
