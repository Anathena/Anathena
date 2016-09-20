namespace Ana.Source.Utils.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Extension methods for all objects
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Prints the method that called this function, as well as any provided parameters.
        /// Returns the same object being operated on, allowing for lock(Object.PrintDebugTag()) for lock debugging.
        /// TODO: Debug channel specification
        /// </summary>
        /// <param name="self"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static Object PrintDebugTag(this Object self, [CallerMemberName] String callerName = "", params String[] parameters)
        {
            // Write calling class and method name
            String Tag = "[" + self.GetType().Name + "] - " + callerName;

            // Write parameters
            if (parameters.Length > 0)
                (new List<String>(parameters)).ForEach(x => Tag += " " + x);

            Console.WriteLine(Tag);

            return self;
        }
    }
    //// End calss
}
//// End namespace