using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Ana.Source.Utils.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Prints the method that called this function, as well as any provided parameters.
        /// Returns the same object being operated on, allowing for lock(Object.PrintDebugTag()) for lock debugging.
        /// TODO: Debug channel specification
        /// </summary>
        /// <param name="Object"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        public static Object PrintDebugTag(this Object Object, [CallerMemberName] String CallerName = "", params String[] Params)
        {
            // Write calling class and method name
            String Tag = "[" + Object.GetType().Name + "] - " + CallerName;

            // Write parameters
            if (Params.Length > 0)
                (new List<String>(Params)).ForEach(x => Tag += " " + x);

            Console.WriteLine(Tag);

            return Object;
        }

    } // End calss

} // End namespace