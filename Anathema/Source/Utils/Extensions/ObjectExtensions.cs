using Anathema.Source.PropertyEditor;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Anathema.Source.Utils.Extensions
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

        /// <summary>
        /// Returns a set of fields assocated with the calling object.
        /// </summary>
        /// <param name="Object"></param>
        /// <returns></returns>
        public static IEnumerable<Property> GetProperties(this Object Object)
        {
            FieldInfo[] FieldInfo = Object.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
            List<Property> Properties = new List<Property>();

            foreach (FieldInfo Field in FieldInfo)
            {
                if (Field == null)
                    continue;

                // Type Type = Field.GetType();
                // var A = Type.GetProperty("Id");
                // Boolean IgnoreProperty = Attribute.IsDefined(A, typeof(IgnoreProperty));

                String Name = Field.Name;
                Object Value = Object.GetType().InvokeMember(Field.Name, BindingFlags.GetField, null, Object, null);

                Properties.Add(new Property(Name, Value));
            }

            return Properties;
        }

    } // End calss

} // End namespace