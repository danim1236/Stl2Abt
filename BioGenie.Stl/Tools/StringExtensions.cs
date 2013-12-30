using System.Collections;

namespace BioGenie.Stl.Tools
{
    public static class StringExtensions
    {
        public static string FormatString(this string format, params object[] args)
        {
            switch (args.Length)
            {
                case 0:
                    return string.Format(format);
                case 1:
                    return string.Format(format, args[0]);
                case 2:
                    return string.Format(format, args[0], args[1]);
                case 3:
                    return string.Format(format, args[0], args[1], args[2]);
                case 4:
                    return string.Format(format, args[0], args[1], args[2], args[3]);
                default:
                    return string.Format(format, args[0], args[1], args[2], args[3], args[4]);
            }
        }
    }
}