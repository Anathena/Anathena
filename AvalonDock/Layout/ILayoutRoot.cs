/************************************************************************

   AvalonDock

   Copyright (C) 2007-2013 Xceed Software Inc.

   This program is provided to you under the terms of the New BSD
   License (BSD) as published at http://avalondock.codeplex.com/license 

   For more features, controls, and fast professional support,
   pick up AvalonDock in Extended WPF Toolkit Plus at http://xceed.com/wpf_toolkit

   Stay informed: follow @datagrid on Twitter or Like facebook.com/datagrids

  **********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Xceed.Wpf.AvalonDock.Layout
{
    public interface ILayoutRoot
    {
        DockingManager Manager { get; }

        LayoutPanel RootPanel { get; }

        LayoutAnchorSide TopSide { get; }
        LayoutAnchorSide LeftSide { get; }
        LayoutAnchorSide RightSide { get; }
        LayoutAnchorSide BottomSide { get; }

        LayoutContent ActiveContent { get; set; }

        void CollectGarbage();

        ObservableCollection<LayoutFloatingWindow> FloatingWindows { get; }
        ObservableCollection<LayoutAnchorable> Hidden { get; }
    }
}
