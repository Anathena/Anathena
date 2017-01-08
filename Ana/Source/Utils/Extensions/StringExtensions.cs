namespace Ana.Source.Utils.Extensions
{
    using System;
    using System.Text;

    /// <summary>
    /// Extension methods for strings.
    /// </summary>
    internal static class StringExtensions
    {
        /// <summary>
        /// Removes the provided subjects from the given string. Will remove the first match only.
        /// </summary>
        /// <param name="str">The provided string.</param>
        /// <param name="suffixes">The suffixes to search for and remove.</param>
        /// <returns>The string with the trimmed suffix, if any were found.</returns>
        public static String RemoveSuffixes(this String str, params String[] suffixes)
        {
            if (suffixes == null)
            {
                return str;
            }

            String suffix = String.Empty;
            foreach (String nextSuffix in suffixes)
            {
                if (str.EndsWith(nextSuffix))
                {
                    suffix = nextSuffix;
                    break;
                }
            }

            if (String.IsNullOrEmpty(suffix))
            {
                return str;
            }

            return str.Substring(0, str.Length - suffix.Length);
        }

        /// <summary>
        /// Replaces any substring within a string with a new value. Allows for string comparison types.
        /// </summary>
        /// <param name="source">The original string.</param>
        /// <param name="oldValue">The value for which to search.</param>
        /// <param name="newValue">The replacement value.</param>
        /// <param name="comparisonType">The string comparison type between the old and replacement values.</param>
        /// <returns>A string with replaced values.</returns>
        public static String Replace(this String source, String oldValue, String newValue, StringComparison comparisonType)
        {
            if (source.Length == 0 || oldValue.Length == 0)
            {
                return source;
            }

            StringBuilder result = new StringBuilder();
            Int32 startingPos = 0;
            Int32 nextMatch;

            while ((nextMatch = source.IndexOf(oldValue, startingPos, comparisonType)) > -1)
            {
                result.Append(source, startingPos, nextMatch - startingPos);
                result.Append(newValue);
                startingPos = nextMatch + oldValue.Length;
            }

            result.Append(source, startingPos, source.Length - startingPos);

            return result.ToString();
        }
    }
    //// End class
}
//// End namespace