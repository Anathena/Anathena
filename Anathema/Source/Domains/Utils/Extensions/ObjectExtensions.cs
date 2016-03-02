using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Anathema
{
    public static class ObjectExtensions
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void PrintDebugTag(this Object Object, params String[] Params)
        {
            StackTrace StackTrace = new StackTrace();

            // Write calling class and method name
            Console.Write("[" + Object.GetType().Name + "] - " + StackTrace.GetFrame(1).GetMethod().Name);

            // Write parameters
            (new List<String>(Params)).ForEach(x => Console.Write(" " + x));

            Console.WriteLine();
        }

    } // End calss

} // End namespace