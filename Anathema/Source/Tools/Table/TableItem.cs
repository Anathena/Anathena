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

        public ScriptItem()
        {
            this.Description = "No Description";
        }

        public ScriptItem(String Script)
        {
            this.Script = Script;
        }
    }

    [DataContract()]
    public class AddressItem : TableItem
    {
        [DataMember()]
        public UInt64 Address { get; set; }

        [DataMember()]
        public Int32[] Offsets { get; set; }

        [DataMember()]
        public String TypeName { get; set; }

        public Type ElementType { get { return Type.GetType(TypeName); } set { this.TypeName = (value == null ? String.Empty : value.FullName); } }

        private dynamic _Value;
        public dynamic Value { get { return _Value; } set { if (!Activated) _Value = value; } }

        public AddressItem(UInt64 Address, Type ElementType)
        {
            this.Address = Address;
            this.ElementType = ElementType;
            Offsets = null;
        }

        public AddressItem(UInt64 Address, String Description, Type ElementType, Int32[] Offsets)
        {
            this.Address = Address;
            this.Description = Description;
            this.ElementType = ElementType;
            this.Offsets = Offsets;
        }

        public void ForceUpdateValue(dynamic Value)
        {
            if (Value == null)
                return;

            this._Value = Value;
        }
    }

    [DataContract()]
    public class TableItem
    {

        [DataMember()]
        public Int32 TextColorARGB { get; set; }

        [DataMember()]
        public String Description { get; set; }

        public Color TextColor { get { return Color.FromArgb(TextColorARGB); } set { TextColorARGB = value.ToArgb(); } }

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
