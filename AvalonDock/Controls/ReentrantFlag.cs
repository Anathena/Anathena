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
    class ReentrantFlag
    {
        public class _ReentrantFlagHandler : IDisposable
        {
            ReentrantFlag _owner;
            public _ReentrantFlagHandler(ReentrantFlag owner)
            {
                _owner = owner;
                _owner._flag = true;
            }

            public void Dispose()
            {
                _owner._flag = false;
            }
        }

        bool _flag = false;

        public _ReentrantFlagHandler Enter()
        { 
            if (_flag)
                throw new InvalidOperationException();
            return new _ReentrantFlagHandler(this);
        }

        public bool CanEnter
        {
            get { return !_flag; }
        }

    }
}
