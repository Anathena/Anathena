
namespace LineCounter
{
    static class Helpers
    {
        public static string Extract(this string source, char delimiter, int startIndex, int endIndex)
        {
            int start = source.NthIndexOf(delimiter, startIndex);
            int end = source.NthIndexOf(delimiter, endIndex);

            if (start == -1 || end == -1)
                return null;

            return source.Substring(start + 1, end - start - 1);
        }

        public static int NthIndexOf(this string source, char value, int count)
        {
            int index = 0;
            for (int i = 0; i < count; i++)
            {
                index = source.IndexOf(value, index);
                if (index == -1)
                    return -1;

                index++;
            }

            return index - 1;
        }
    }
}
