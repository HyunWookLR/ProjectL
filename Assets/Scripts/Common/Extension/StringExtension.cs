public static class StringExtension
{
    public static bool NotEmpty(this string str)
    {
        return str != null && str.Length > 0;
    }
}

