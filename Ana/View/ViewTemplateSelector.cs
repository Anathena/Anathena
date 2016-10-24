namespace Ana.View
{
    using Source.ChangeCounter;
    using Source.CheatBrowser;
    using Source.DotNetExplorer;
    using Source.LabelThresholder;
    using Source.ProcessSelector;
    using Source.Project;
    using Source.PropertyViewer;
    using Source.Results;
    using Source.Scanners.ManualScanner;
    using Source.Snapshots;
    using Source.UserSettings;
    using Source.Utils.ScriptEditor;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Provides the template required to view a pane
    /// </summary>
    internal class ViewTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewTemplateSelector" /> class
        /// </summary>
        public ViewTemplateSelector()
        {
        }

        /// <summary>
        /// Gets or sets the template for the Process Selector
        /// </summary>
        public DataTemplate ProcessSelectorViewTemplate { get; set; }

        /// <summary>
        /// Gets or sets the template for the Change Counter
        /// </summary>
        public DataTemplate ChangeCounterViewTemplate { get; set; }

        /// <summary>
        /// Gets or sets the template for the Input Correlator
        /// </summary>
        public DataTemplate InputCorrelatorViewTemplate { get; set; }

        /// <summary>
        /// Gets or sets the template for the Label Thresholder
        /// </summary>
        public DataTemplate LabelThresholderViewTemplate { get; set; }

        /// <summary>
        /// Gets or sets the template for the Manual Scanner
        /// </summary>
        public DataTemplate ManualScannerViewTemplate { get; set; }

        /// <summary>
        /// Gets or sets the template for the Snapshot Manager
        /// </summary>
        public DataTemplate SnapshotManagerViewTemplate { get; set; }

        /// <summary>
        /// Gets or sets the template for the Scan Results
        /// </summary>
        public DataTemplate ScanResultsViewTemplate { get; set; }

        /// <summary>
        /// Gets or sets the template for the Cheat Browser
        /// </summary>
        public DataTemplate CheatBrowserViewTemplate { get; set; }

        /// <summary>
        /// Gets or sets the template for the .Net Explorer
        /// </summary>
        public DataTemplate DotNetExplorerViewTemplate { get; set; }

        /// <summary>
        /// Gets or sets the template for the Property Viewer
        /// </summary>
        public DataTemplate PropertyViewerViewTemplate { get; set; }

        /// <summary>
        /// Gets or sets the template for the Settings
        /// </summary>
        public DataTemplate SettingsViewTemplate { get; set; }

        /// <summary>
        /// Gets or sets the template for the Project Explorer
        /// </summary>
        public DataTemplate ProjectExplorerViewTemplate { get; set; }

        /// <summary>
        /// Gets or sets the template for the Offset Editor
        /// </summary>
        public DataTemplate OffsetEditorViewTemplate { get; set; }

        /// <summary>
        /// Gets or sets the template for the Script Editor
        /// </summary>
        public DataTemplate ScriptEditorViewTemplate { get; set; }

        /// <summary>
        /// Gets or sets the template for the Hotkey Editor
        /// </summary>
        public DataTemplate HotkeyEditorViewTemplate { get; set; }

        /// <summary>
        /// Returns the required template to display the given view model
        /// </summary>
        /// <param name="item">The view model</param>
        /// <param name="container">The dependency object</param>
        /// <returns>The template associated with the provided view model</returns>
        public override DataTemplate SelectTemplate(Object item, DependencyObject container)
        {
            if (item is ProcessSelectorViewModel)
            {
                return this.ProcessSelectorViewTemplate;
            }
            else if (item is ChangeCounterViewModel)
            {
                return this.ChangeCounterViewTemplate;
            }
            else if (item is InputCorrelatorViewModel)
            {
                return this.InputCorrelatorViewTemplate;
            }
            else if (item is LabelThresholderViewModel)
            {
                return this.LabelThresholderViewTemplate;
            }
            else if (item is ManualScannerViewModel)
            {
                return this.ManualScannerViewTemplate;
            }
            else if (item is SnapshotManagerViewModel)
            {
                return this.SnapshotManagerViewTemplate;
            }
            else if (item is ScanResultsViewModel)
            {
                return this.ScanResultsViewTemplate;
            }
            else if (item is CheatBrowserViewModel)
            {
                return this.CheatBrowserViewTemplate;
            }
            else if (item is DotNetExplorerViewModel)
            {
                return this.DotNetExplorerViewTemplate;
            }
            else if (item is PropertyViewerViewModel)
            {
                return this.PropertyViewerViewTemplate;
            }
            else if (item is SettingsViewModel)
            {
                return this.SettingsViewTemplate;
            }
            else if (item is ProjectExplorerViewModel)
            {
                return this.ProjectExplorerViewTemplate;
            }
            else if (item is ScriptEditorViewModel)
            {
                return this.ScriptEditorViewTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }
    //// End class
}
//// End namespace