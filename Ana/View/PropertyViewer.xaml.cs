namespace Ana.View
{
    using Controls;
    using Source.PropertyViewer;
    using Source.Utils.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for PropertyViewer.xaml
    /// </summary>
    internal partial class PropertyViewer : UserControl, IPropertyViewerObserver
    {
        private System.Windows.Forms.PropertyGrid propertyGrid;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyViewer" /> class
        /// </summary>
        public PropertyViewer()
        {
            this.InitializeComponent();

            // Windows Forms hosting -- TODO: Phase this out
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();

            this.SetAllColorsDark();
            this.propertyGrid.BackColor = DarkBrushes.BaseColor3;
            this.propertyGrid.CommandsBackColor = DarkBrushes.BaseColor3;
            this.propertyGrid.HelpBackColor = DarkBrushes.BaseColor3;
            this.propertyGrid.SelectedItemWithFocusBackColor = DarkBrushes.BaseColor3;
            this.propertyGrid.ViewBackColor = DarkBrushes.BaseColor3;

            this.propertyGrid.CommandsActiveLinkColor = DarkBrushes.BaseColor3;
            this.propertyGrid.CommandsDisabledLinkColor = DarkBrushes.BaseColor3;

            this.propertyGrid.CategorySplitterColor = DarkBrushes.BaseColor2;

            this.propertyGrid.CommandsBorderColor = DarkBrushes.BaseColor4;
            this.propertyGrid.HelpBorderColor = DarkBrushes.BaseColor4;
            this.propertyGrid.ViewBorderColor = DarkBrushes.BaseColor4;

            this.propertyGrid.CategoryForeColor = DarkBrushes.BaseColor2;
            this.propertyGrid.CommandsForeColor = DarkBrushes.BaseColor2;
            this.propertyGrid.DisabledItemForeColor = DarkBrushes.BaseColor2;
            this.propertyGrid.HelpForeColor = DarkBrushes.BaseColor2;
            this.propertyGrid.SelectedItemWithFocusForeColor = DarkBrushes.BaseColor2;
            this.propertyGrid.ViewForeColor = DarkBrushes.BaseColor2;

            PropertyViewerViewModel.GetInstance().Subscribe(this);

            this.propertyViewer.Children.Add(WinformsHostingHelper.CreateHostedControl(this.propertyGrid));
        }

        public void Update(Object[] targetObjects)
        {
            this.propertyGrid.SelectedObjects = targetObjects;
        }

        private void SetAllColorsDark()
        {
            PropertyInfo[] allProperties = this.propertyGrid.GetType().GetProperties();
            IEnumerable<PropertyInfo> colorProperties = allProperties.Select(x => x).Where(x => x.PropertyType == typeof(Color));
            colorProperties.ForEach(x => x.SetValue(this.propertyGrid, DarkBrushes.BaseColor3, null));
        }
    }
    //// End class
}
//// End namespace