using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.Serialization;

namespace Anathema.Source.Project.ProjectItems
{
    [Obfuscation(ApplyToMembers = false)]
    [Obfuscation(Exclude = true)]
    [DataContract()]
    public class ProjectItem : IEnumerable<ProjectItem>
    {
        [Obfuscation(Exclude = true)]
        private ProjectItem _Parent;
        [Obfuscation(Exclude = true)]
        [DataMember()]
        [Browsable(false)]
        public ProjectItem Parent
        {
            [Obfuscation(Exclude = true)]
            get { return _Parent; }
            [Obfuscation(Exclude = true)]
            set { _Parent = value; OnUpdateField(); }
        }

        [Obfuscation(Exclude = true)]
        private List<ProjectItem> _Children;
        [Obfuscation(Exclude = true)]
        [DataMember()]
        [Browsable(false)]
        public List<ProjectItem> Children
        {
            [Obfuscation(Exclude = true)]
            get { return _Children; }
            [Obfuscation(Exclude = true)]
            set { _Children = value; OnUpdateField(); }
        }

        [Obfuscation(Exclude = true)]
        private String _Description;
        [Obfuscation(Exclude = true)]
        [DataMember()]
        [Category("Properties"), DisplayName("Description"), Description("Description to be shown for the Project Items")]
        public String Description
        {
            [Obfuscation(Exclude = true)]
            get { return _Description; }
            [Obfuscation(Exclude = true)]
            set { _Description = value; OnUpdateField(); }
        }

        [Obfuscation(Exclude = true)]
        [DataMember()]
        [Browsable(false)]
        public Int32 TextColorARGB;

        [Obfuscation(Exclude = true)]
        [Category("Properties"), DisplayName("Text Color"), Description("Display Color")]
        public Color TextColor
        {
            [Obfuscation(Exclude = true)]
            get { return Color.FromArgb(TextColorARGB); }
            [Obfuscation(Exclude = true)]
            set { TextColorARGB = value == null ? 0 : value.ToArgb(); }
        }

        [Obfuscation(Exclude = true)]
        [Browsable(false)]
        protected Boolean Activated { get; set; }

        public ProjectItem() : this(String.Empty) { }

        public ProjectItem(String Description)
        {
            this.Description = Description == null ? String.Empty : Description;

            Parent = null;
            Children = new List<ProjectItem>();
            TextColor = SystemColors.ControlText;
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

        [Obfuscation(Exclude = true)]
        public void AddChild(ProjectItem Child)
        {
            Children.Add(Child);
        }

        [Obfuscation(Exclude = true)]
        public void OnUpdateField()
        {

        }

        [Obfuscation(Exclude = true)]
        public IEnumerator<ProjectItem> GetEnumerator()
        {
            return ((IEnumerable<ProjectItem>)Children).GetEnumerator();
        }

        [Obfuscation(Exclude = true)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<ProjectItem>)Children).GetEnumerator();
        }

    } // End class

} // End namespace