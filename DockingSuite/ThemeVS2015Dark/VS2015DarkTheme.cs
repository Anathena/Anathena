using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WeifenLuo.WinFormsUI.Docking
{
    /// <summary>
    /// Visual Studio 2012 Dark theme.
    /// </summary>
    public class VS2015DarkTheme : ThemeBase
    {
        private static readonly VS2015DarkColorTable colorTable = new VS2015DarkColorTable();
        public VS2015DarkTheme()
        {

        }

        /// <summary>
        /// Applies the specified theme to the dock panel.
        /// </summary>
        /// <param name="dockPanel">The dock panel.</param>
        public override void Apply(DockPanel dockPanel)
        {
            if (dockPanel == null)
            {
                throw new NullReferenceException("dockPanel");
            }

            Measures.SplitterSize = 6;
            dockPanel.Extender.DockPaneCaptionFactory = new VS2015DarkDockPaneCaptionFactory();
            dockPanel.Extender.AutoHideStripFactory = new VS2015DarkAutoHideStripFactory();
            dockPanel.Extender.AutoHideWindowFactory = new VS2015DarkAutoHideWindowFactory();
            dockPanel.Extender.DockPaneStripFactory = new VS2015DarkDockPaneStripFactory();
            dockPanel.Extender.DockPaneSplitterControlFactory = new VS2015DarkDockPaneSplitterControlFactory();
            dockPanel.Extender.DockWindowSplitterControlFactory = new VS2015DarkDockWindowSplitterControlFactory();
            dockPanel.Extender.DockWindowFactory = new VS2015DarkDockWindowFactory();
            dockPanel.Extender.PaneIndicatorFactory = new VS2015DarkPaneIndicatorFactory();
            dockPanel.Extender.PanelIndicatorFactory = new VS2015DarkPanelIndicatorFactory();
            dockPanel.Extender.DockOutlineFactory = new VS2015DarkDockOutlineFactory();
            dockPanel.Skin = CreateVisualStudio2015Dark();
        }

        private class VS2015DarkDockOutlineFactory : DockPanelExtender.IDockOutlineFactory
        {
            public DockOutlineBase CreateDockOutline()
            {
                return new VS2015DarkDockOutline();
            }

            private class VS2015DarkDockOutline : DockOutlineBase
            {
                public VS2015DarkDockOutline()
                {
                    m_dragForm = new DragForm();
                    SetDragForm(Rectangle.Empty);
                    DragForm.BackColor = Color.FromArgb(0xff, 91, 173, 255);
                    DragForm.Opacity = 0.5;
                    DragForm.Show(false);
                }

                DragForm m_dragForm;
                private DragForm DragForm
                {
                    get { return m_dragForm; }
                }

                protected override void OnShow()
                {
                    CalculateRegion();
                }

                protected override void OnClose()
                {
                    DragForm.Close();
                }

                private void CalculateRegion()
                {
                    if (SameAsOldValue)
                        return;

                    if (!FloatWindowBounds.IsEmpty)
                        SetOutline(FloatWindowBounds);
                    else if (DockTo is DockPanel)
                        SetOutline(DockTo as DockPanel, Dock, (ContentIndex != 0));
                    else if (DockTo is DockPane)
                        SetOutline(DockTo as DockPane, Dock, ContentIndex);
                    else
                        SetOutline();
                }

                private void SetOutline()
                {
                    SetDragForm(Rectangle.Empty);
                }

                private void SetOutline(Rectangle floatWindowBounds)
                {
                    SetDragForm(floatWindowBounds);
                }

                private void SetOutline(DockPanel dockPanel, DockStyle dock, bool fullPanelEdge)
                {
                    Rectangle rect = fullPanelEdge ? dockPanel.DockArea : dockPanel.DocumentWindowBounds;
                    rect.Location = dockPanel.PointToScreen(rect.Location);
                    if (dock == DockStyle.Top)
                    {
                        int height = dockPanel.GetDockWindowSize(DockState.DockTop);
                        rect = new Rectangle(rect.X, rect.Y, rect.Width, height);
                    }
                    else if (dock == DockStyle.Bottom)
                    {
                        int height = dockPanel.GetDockWindowSize(DockState.DockBottom);
                        rect = new Rectangle(rect.X, rect.Bottom - height, rect.Width, height);
                    }
                    else if (dock == DockStyle.Left)
                    {
                        int width = dockPanel.GetDockWindowSize(DockState.DockLeft);
                        rect = new Rectangle(rect.X, rect.Y, width, rect.Height);
                    }
                    else if (dock == DockStyle.Right)
                    {
                        int width = dockPanel.GetDockWindowSize(DockState.DockRight);
                        rect = new Rectangle(rect.Right - width, rect.Y, width, rect.Height);
                    }
                    else if (dock == DockStyle.Fill)
                    {
                        rect = dockPanel.DocumentWindowBounds;
                        rect.Location = dockPanel.PointToScreen(rect.Location);
                    }

                    SetDragForm(rect);
                }

                private void SetOutline(DockPane pane, DockStyle dock, int contentIndex)
                {
                    if (dock != DockStyle.Fill)
                    {
                        Rectangle rect = pane.DisplayingRectangle;
                        if (dock == DockStyle.Right)
                            rect.X += rect.Width / 2;
                        if (dock == DockStyle.Bottom)
                            rect.Y += rect.Height / 2;
                        if (dock == DockStyle.Left || dock == DockStyle.Right)
                            rect.Width -= rect.Width / 2;
                        if (dock == DockStyle.Top || dock == DockStyle.Bottom)
                            rect.Height -= rect.Height / 2;
                        rect.Location = pane.PointToScreen(rect.Location);

                        SetDragForm(rect);
                    }
                    else if (contentIndex == -1)
                    {
                        Rectangle rect = pane.DisplayingRectangle;
                        rect.Location = pane.PointToScreen(rect.Location);
                        SetDragForm(rect);
                    }
                    else
                    {
                        using (GraphicsPath path = pane.TabStripControl.GetOutline(contentIndex))
                        {
                            RectangleF rectF = path.GetBounds();
                            Rectangle rect = new Rectangle((int)rectF.X, (int)rectF.Y, (int)rectF.Width, (int)rectF.Height);
                            using (Matrix matrix = new Matrix(rect, new Point[] { new Point(0, 0), new Point(rect.Width, 0), new Point(0, rect.Height) }))
                            {
                                path.Transform(matrix);
                            }

                            Region region = new Region(path);
                            SetDragForm(rect, region);
                        }
                    }
                }

                private void SetDragForm(Rectangle rect)
                {
                    DragForm.Bounds = rect;
                    if (rect == Rectangle.Empty)
                    {
                        if (DragForm.Region != null)
                        {
                            DragForm.Region.Dispose();
                        }

                        DragForm.Region = new Region(Rectangle.Empty);
                    }
                    else if (DragForm.Region != null)
                    {
                        DragForm.Region.Dispose();
                        DragForm.Region = null;
                    }
                }

                private void SetDragForm(Rectangle rect, Region region)
                {
                    DragForm.Bounds = rect;
                    if (DragForm.Region != null)
                    {
                        DragForm.Region.Dispose();
                    }

                    DragForm.Region = region;
                }
            }
        }

        private class VS2015DarkPanelIndicatorFactory : DockPanelExtender.IPanelIndicatorFactory
        {
            public DockPanel.IPanelIndicator CreatePanelIndicator(DockStyle style)
            {
                return new VS2015DarkPanelIndicator(style);
            }

            private class VS2015DarkPanelIndicator : PictureBox, DockPanel.IPanelIndicator
            {
                private static Image _imagePanelLeft = ThemeVS2012Light.Resources.DockIndicator_PanelLeft;
                private static Image _imagePanelRight = ThemeVS2012Light.Resources.DockIndicator_PanelRight;
                private static Image _imagePanelTop = ThemeVS2012Light.Resources.DockIndicator_PanelTop;
                private static Image _imagePanelBottom = ThemeVS2012Light.Resources.DockIndicator_PanelBottom;
                private static Image _imagePanelFill = ThemeVS2012Light.Resources.DockIndicator_PanelFill;
                private static Image _imagePanelLeftActive = ThemeVS2012Light.Resources.DockIndicator_PanelLeft;
                private static Image _imagePanelRightActive = ThemeVS2012Light.Resources.DockIndicator_PanelRight;
                private static Image _imagePanelTopActive = ThemeVS2012Light.Resources.DockIndicator_PanelTop;
                private static Image _imagePanelBottomActive = ThemeVS2012Light.Resources.DockIndicator_PanelBottom;
                private static Image _imagePanelFillActive = ThemeVS2012Light.Resources.DockIndicator_PanelFill;

                public VS2015DarkPanelIndicator(DockStyle dockStyle)
                {
                    m_dockStyle = dockStyle;
                    SizeMode = PictureBoxSizeMode.AutoSize;
                    Image = ImageInactive;
                }

                private DockStyle m_dockStyle;

                private DockStyle DockStyle
                {
                    get { return m_dockStyle; }
                }

                private DockStyle m_status;

                public DockStyle Status
                {
                    get { return m_status; }
                    set
                    {
                        if (value != DockStyle && value != DockStyle.None)
                            throw new InvalidEnumArgumentException();

                        if (m_status == value)
                            return;

                        m_status = value;
                        IsActivated = (m_status != DockStyle.None);
                    }
                }

                private Image ImageInactive
                {
                    get
                    {
                        if (DockStyle == DockStyle.Left)
                            return _imagePanelLeft;
                        else if (DockStyle == DockStyle.Right)
                            return _imagePanelRight;
                        else if (DockStyle == DockStyle.Top)
                            return _imagePanelTop;
                        else if (DockStyle == DockStyle.Bottom)
                            return _imagePanelBottom;
                        else if (DockStyle == DockStyle.Fill)
                            return _imagePanelFill;
                        else
                            return null;
                    }
                }

                private Image ImageActive
                {
                    get
                    {
                        if (DockStyle == DockStyle.Left)
                            return _imagePanelLeftActive;
                        else if (DockStyle == DockStyle.Right)
                            return _imagePanelRightActive;
                        else if (DockStyle == DockStyle.Top)
                            return _imagePanelTopActive;
                        else if (DockStyle == DockStyle.Bottom)
                            return _imagePanelBottomActive;
                        else if (DockStyle == DockStyle.Fill)
                            return _imagePanelFillActive;
                        else
                            return null;
                    }
                }

                private bool m_isActivated = false;

                private bool IsActivated
                {
                    get { return m_isActivated; }
                    set
                    {
                        m_isActivated = value;
                        Image = IsActivated ? ImageActive : ImageInactive;
                    }
                }

                public DockStyle HitTest(Point pt)
                {
                    return this.Visible && ClientRectangle.Contains(PointToClient(pt)) ? DockStyle : DockStyle.None;
                }
            }
        }

        private class VS2015DarkPaneIndicatorFactory : DockPanelExtender.IPaneIndicatorFactory
        {
            public DockPanel.IPaneIndicator CreatePaneIndicator()
            {
                return new VS2015DarkPaneIndicator();
            }

            private class VS2015DarkPaneIndicator : PictureBox, DockPanel.IPaneIndicator
            {
                private static Bitmap _bitmapPaneDiamond = ThemeVS2012Light.Resources.Dockindicator_PaneDiamond;
                private static Bitmap _bitmapPaneDiamondLeft = ThemeVS2012Light.Resources.Dockindicator_PaneDiamond_Fill;
                private static Bitmap _bitmapPaneDiamondRight = ThemeVS2012Light.Resources.Dockindicator_PaneDiamond_Fill;
                private static Bitmap _bitmapPaneDiamondTop = ThemeVS2012Light.Resources.Dockindicator_PaneDiamond_Fill;
                private static Bitmap _bitmapPaneDiamondBottom = ThemeVS2012Light.Resources.Dockindicator_PaneDiamond_Fill;
                private static Bitmap _bitmapPaneDiamondFill = ThemeVS2012Light.Resources.Dockindicator_PaneDiamond_Fill;
                private static Bitmap _bitmapPaneDiamondHotSpot = ThemeVS2012Light.Resources.Dockindicator_PaneDiamond_Hotspot;
                private static Bitmap _bitmapPaneDiamondHotSpotIndex = ThemeVS2012Light.Resources.DockIndicator_PaneDiamond_HotspotIndex;

                private static DockPanel.HotSpotIndex[] _hotSpots = new[]
                    {
                        new DockPanel.HotSpotIndex(1, 0, DockStyle.Top),
                        new DockPanel.HotSpotIndex(0, 1, DockStyle.Left),
                        new DockPanel.HotSpotIndex(1, 1, DockStyle.Fill),
                        new DockPanel.HotSpotIndex(2, 1, DockStyle.Right),
                        new DockPanel.HotSpotIndex(1, 2, DockStyle.Bottom)
                    };

                private GraphicsPath _displayingGraphicsPath = DrawHelper.CalculateGraphicsPathFromBitmap(_bitmapPaneDiamond);

                public VS2015DarkPaneIndicator()
                {
                    SizeMode = PictureBoxSizeMode.AutoSize;
                    Image = _bitmapPaneDiamond;
                    Region = new Region(DisplayingGraphicsPath);
                }

                public GraphicsPath DisplayingGraphicsPath
                {
                    get { return _displayingGraphicsPath; }
                }

                public DockStyle HitTest(Point pt)
                {
                    if (!Visible)
                        return DockStyle.None;

                    pt = PointToClient(pt);
                    if (!ClientRectangle.Contains(pt))
                        return DockStyle.None;

                    for (int i = _hotSpots.GetLowerBound(0); i <= _hotSpots.GetUpperBound(0); i++)
                    {
                        if (_bitmapPaneDiamondHotSpot.GetPixel(pt.X, pt.Y) == _bitmapPaneDiamondHotSpotIndex.GetPixel(_hotSpots[i].X, _hotSpots[i].Y))
                            return _hotSpots[i].DockStyle;
                    }

                    return DockStyle.None;
                }

                private DockStyle m_status = DockStyle.None;

                public DockStyle Status
                {
                    get { return m_status; }
                    set
                    {
                        m_status = value;
                        if (m_status == DockStyle.None)
                            Image = _bitmapPaneDiamond;
                        else if (m_status == DockStyle.Left)
                            Image = _bitmapPaneDiamondLeft;
                        else if (m_status == DockStyle.Right)
                            Image = _bitmapPaneDiamondRight;
                        else if (m_status == DockStyle.Top)
                            Image = _bitmapPaneDiamondTop;
                        else if (m_status == DockStyle.Bottom)
                            Image = _bitmapPaneDiamondBottom;
                        else if (m_status == DockStyle.Fill)
                            Image = _bitmapPaneDiamondFill;
                    }
                }
            }
        }

        private class VS2015DarkAutoHideWindowFactory : DockPanelExtender.IAutoHideWindowFactory
        {
            public DockPanel.AutoHideWindowControl CreateAutoHideWindow(DockPanel panel)
            {
                return new VS2012LightAutoHideWindowControl(panel);
            }
        }

        private class VS2015DarkDockPaneSplitterControlFactory : DockPanelExtender.IDockPaneSplitterControlFactory
        {
            public DockPane.SplitterControlBase CreateSplitterControl(DockPane pane)
            {
                return new VS2012LightSplitterControl(pane);
            }
        }

        private class VS2015DarkDockWindowSplitterControlFactory : DockPanelExtender.IDockWindowSplitterControlFactory
        {
            public SplitterBase CreateSplitterControl()
            {
                return new VS2012LightDockWindow.VS2012LightDockWindowSplitterControl();
            }
        }

        private class VS2015DarkDockPaneStripFactory : DockPanelExtender.IDockPaneStripFactory
        {
            public DockPaneStripBase CreateDockPaneStrip(DockPane pane)
            {
                return new VS2012LightDockPaneStrip(pane);
            }
        }

        private class VS2015DarkAutoHideStripFactory : DockPanelExtender.IAutoHideStripFactory
        {
            public AutoHideStripBase CreateAutoHideStrip(DockPanel panel)
            {
                return new VS2015DarkAutoHideStrip(panel);
            }
        }

        private class VS2015DarkDockPaneCaptionFactory : DockPanelExtender.IDockPaneCaptionFactory
        {
            public DockPaneCaptionBase CreateDockPaneCaption(DockPane pane)
            {
                return new VS2012LightDockPaneCaption(pane);
            }
        }

        private class VS2015DarkDockWindowFactory : DockPanelExtender.IDockWindowFactory
        {
            public DockWindow CreateDockWindow(DockPanel dockPanel, DockState dockState)
            {
                return new VS2012LightDockWindow(dockPanel, dockState);
            }
        }

        public static DockPanelSkin CreateVisualStudio2015Dark()
        {
            var specialBlue = Color.FromArgb(0xFF, 0x00, 0x7A, 0xCC);
            var dot = Color.FromArgb(80, 170, 220);
            var activeTab = specialBlue;
            var mouseHoverTab = Color.FromArgb(0xFF, 28, 151, 234);
            var inactiveTab = SystemColors.Control;
            var lostFocusTab = Color.FromArgb(0xFF, 204, 206, 219);
            var skin = new DockPanelSkin();

            skin.AutoHideStripSkin.DockStripBackground.StartColor = Color.Red;
            skin.AutoHideStripSkin.DockStripBackground.EndColor = Color.Red;

            skin.AutoHideStripSkin.DockStripGradient.StartColor = specialBlue;
            skin.AutoHideStripSkin.DockStripGradient.EndColor = colorTable.MenuItemSelected;
            skin.AutoHideStripSkin.TabGradient.TextColor = colorTable.MenuStripGradientBegin;

            skin.DockPaneStripSkin.DocumentGradient.DockStripGradient.StartColor = colorTable.MenuStripGradientBegin;
            skin.DockPaneStripSkin.DocumentGradient.DockStripGradient.EndColor = colorTable.MenuStripGradientEnd;
            skin.DockPaneStripSkin.DocumentGradient.ActiveTabGradient.StartColor = activeTab;
            skin.DockPaneStripSkin.DocumentGradient.ActiveTabGradient.EndColor = lostFocusTab;
            skin.DockPaneStripSkin.DocumentGradient.ActiveTabGradient.TextColor = Color.White;
            skin.DockPaneStripSkin.DocumentGradient.InactiveTabGradient.StartColor = inactiveTab;
            skin.DockPaneStripSkin.DocumentGradient.InactiveTabGradient.EndColor = mouseHoverTab;
            skin.DockPaneStripSkin.DocumentGradient.InactiveTabGradient.TextColor = Color.Black;

            skin.DockPaneStripSkin.ToolWindowGradient.DockStripGradient.StartColor = colorTable.MenuStripGradientBegin;
            skin.DockPaneStripSkin.ToolWindowGradient.DockStripGradient.EndColor = colorTable.MenuStripGradientEnd;

            skin.DockPaneStripSkin.ToolWindowGradient.ActiveTabGradient.StartColor = colorTable.MenuStripGradientBegin;
            skin.DockPaneStripSkin.ToolWindowGradient.ActiveTabGradient.EndColor = colorTable.MenuStripGradientEnd;
            skin.DockPaneStripSkin.ToolWindowGradient.ActiveTabGradient.TextColor = specialBlue;

            skin.DockPaneStripSkin.ToolWindowGradient.InactiveTabGradient.StartColor = colorTable.MenuStripGradientBegin;
            skin.DockPaneStripSkin.ToolWindowGradient.InactiveTabGradient.EndColor = colorTable.MenuStripGradientEnd;
            skin.DockPaneStripSkin.ToolWindowGradient.InactiveTabGradient.TextColor = colorTable.MenuItemText;

            skin.DockPaneStripSkin.ToolWindowGradient.ActiveCaptionGradient.StartColor = specialBlue;
            skin.DockPaneStripSkin.ToolWindowGradient.ActiveCaptionGradient.EndColor = dot;
            skin.DockPaneStripSkin.ToolWindowGradient.ActiveCaptionGradient.LinearGradientMode = LinearGradientMode.Vertical;
            skin.DockPaneStripSkin.ToolWindowGradient.ActiveCaptionGradient.TextColor = colorTable.MenuItemText;

            skin.DockPaneStripSkin.ToolWindowGradient.InactiveCaptionGradient.StartColor = colorTable.MenuStripGradientBegin;
            skin.DockPaneStripSkin.ToolWindowGradient.InactiveCaptionGradient.EndColor = colorTable.MenuStripGradientEnd;
            skin.DockPaneStripSkin.ToolWindowGradient.InactiveCaptionGradient.LinearGradientMode = LinearGradientMode.Vertical;
            skin.DockPaneStripSkin.ToolWindowGradient.InactiveCaptionGradient.TextColor = colorTable.MenuItemText;

            return skin;
        }
    }
}