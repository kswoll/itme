using System;
using System.Collections.Generic;
using System.Linq;

namespace ItMe.Utils
{
    public static class EnumExtensions
    {
        public static IEnumerable<T> GetFlags<T>(this T flags) where T : Enum
        {
            foreach (T value in Enum.GetValues(typeof(T)))
                if (flags.HasFlag(value))
                    yield return value;
        }

        public static string GetFlagsAsString<T>(this T flags, string delimiter = ",") where T : Enum
        {
            return string.Join(delimiter, flags.GetFlags().Select(x => x.ToString()));
        }

        public static T ToFlags<T>(this IEnumerable<T> flags) where T : Enum
        {
            int value = 0;
            foreach (var flag in flags)
            {
                var numericFlag = (int)(object)flag;
                value |= numericFlag;
            }
            return (T)(object)value;
        }

        public static T ToFlagsFromString<T>(this string s, string delimiter = ",") where T : Enum
        {
            return s.Split(new[] { delimiter }, StringSplitOptions.None).Select(x => x.Parse<T>()).ToFlags();
        }

        public static T Parse<T>(this string value) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), value);
        }
    }
}