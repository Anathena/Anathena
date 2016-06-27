using Aga.Controls.Tree;

namespace Anathema.GUI
{
    public class AddressNode : Node
    {
        public string FreezeCheckBox = "";  // This sould make the DataPropertyName specified in the Node Collection.
        public string EntryIcon = "";
        public string EntryDescription = "";
        public string EntryAddress = "";
        public string EntryType = "";
        public string EntryValue = "";

        /// <summary>
        /// Initializes a new MyNode class with a given Text property.
        /// </summary>
        /// <param name="text">String to set the text property with.</param>
        public AddressNode(string NodeControl1, string NodeControl2, string NodeControl3, string NodeControl4, string NodeControl5, string NodeControl6) : base("Address")
        {
            this.FreezeCheckBox = NodeControl1;
            this.EntryIcon = NodeControl2;
            this.EntryDescription = NodeControl3;
            this.EntryAddress = NodeControl4;
            this.EntryType = NodeControl5;
            this.EntryValue = NodeControl6;
        }


        private bool _checked;
        /// <summary>
        /// Whether the box is checked or not.
        /// </summary>
        public bool Checked
        {
            get { return _checked; }
            set { _checked = value; }
        }
    }
}
