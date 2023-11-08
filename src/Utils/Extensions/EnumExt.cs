namespace Utils.Extensions;

public static class EnumExt
{
    public static T ToEnum<T>(this string value, bool ignoreCase = true)
    {
        return (T)Enum.Parse(typeof(T), value, ignoreCase);
    }
}
