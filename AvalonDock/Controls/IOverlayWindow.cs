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

namespace Xceed.Wpf.AvalonDock.Controls
{
    internal interface IOverlayWindow
    {
        IEnumerable<IDropTarget> GetTargets();

        void DragEnter(LayoutFloatingWindowControl floatingWindow);
        void DragLeave(LayoutFloatingWindowControl floatingWindow);

        void DragEnter(IDropArea area);
        void DragLeave(IDropArea area);

        void DragEnter(IDropTarget target);
        void DragLeave(IDropTarget target);
        void DragDrop(IDropTarget target);
    }
}
