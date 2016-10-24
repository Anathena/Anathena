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
            propertyGrid = new System.Windows.Forms.PropertyGrid();

            SetAllColorsDark();
            propertyGrid.BackColor = DarkBrushes.BaseColor3;
            propertyGrid.CommandsBackColor = DarkBrushes.BaseColor3;
            propertyGrid.HelpBackColor = DarkBrushes.BaseColor3;
            propertyGrid.SelectedItemWithFocusBackColor = DarkBrushes.BaseColor3;
            propertyGrid.ViewBackColor = DarkBrushes.BaseColor3;

            propertyGrid.CommandsActiveLinkColor = DarkBrushes.BaseColor3;
            propertyGrid.CommandsDisabledLinkColor = DarkBrushes.BaseColor3;

            propertyGrid.CategorySplitterColor = DarkBrushes.BaseColor2;

            propertyGrid.CommandsBorderColor = DarkBrushes.BaseColor4;
            propertyGrid.HelpBorderColor = DarkBrushes.BaseColor4;
            propertyGrid.ViewBorderColor = DarkBrushes.BaseColor4;

            propertyGrid.CategoryForeColor = DarkBrushes.BaseColor2;
            propertyGrid.CommandsForeColor = DarkBrushes.BaseColor2;
            propertyGrid.DisabledItemForeColor = DarkBrushes.BaseColor2;
            propertyGrid.HelpForeColor = DarkBrushes.BaseColor2;
            propertyGrid.SelectedItemWithFocusForeColor = DarkBrushes.BaseColor2;
            propertyGrid.ViewForeColor = DarkBrushes.BaseColor2;

            PropertyViewerViewModel.GetInstance().Subscribe(this);

            this.propertyViewer.Children.Add(WinformsHostingHelper.CreateHostedControl(propertyGrid));
        }

        private void SetAllColorsDark()
        {
            PropertyInfo[] AllProperties = propertyGrid.GetType().GetProperties();
            IEnumerable<PropertyInfo> swag = AllProperties.Select(x => x).Where(x => x.PropertyType == typeof(Color));
            swag.ForEach(x => x.SetValue(this.propertyGrid, DarkBrushes.BaseColor3, null));
        }

        public void Update(Object[] targetObjects)
        {
            propertyGrid.SelectedObjects = targetObjects;
        }
    }
    //// End class
}
//// End namespace