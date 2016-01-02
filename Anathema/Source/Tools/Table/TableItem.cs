using System;
using System.Collections.Generic;
using System.Drawing;

namespace Anathema
{
    [Serializable]
    class ScriptItem : TableItem
    {
        public String Script { get; set; }

        public ScriptItem(String Script)
        {
            this.Script = Script;
        }
    }

    [Serializable]
    class AddressItem : TableItem
    {
        public IntPtr Address { get; set; }
        public Int32[] Offsets { get; set; }
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

    [Serializable]
    class TableItem
    {
        public Color TextColor { get; set; }
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
