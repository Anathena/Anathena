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

namespace Xceed.Wpf.AvalonDock
{
    internal static class MathHelper
    {

        public static double MinMax(double value, double min, double max)
        {
            if (min > max)
                throw new ArgumentException("min>max");

            if (value < min)
                return min;
            if (value > max)
                return max;

            return value;
        }

        public static void AssertIsPositiveOrZero(double value)
        {
            if (value < 0.0)
                throw new ArgumentException("Invalid value, must be a positive number or equal to zero");
        }
    }
}
