namespace FilmsCatalog.Services
{
    public static class StringEx
    {
        public static string SubstringIfLonger(this string source, int length)
        {
            if (source.Length < length) return source;
            return source.Substring(0, length);
        }
    }
}