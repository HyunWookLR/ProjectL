
public static class EnumTranslator
{
    public static string GetTypeString(Type type)
    {
        switch (type)
        {
            case Type.Red: return "拳加己";
            case Type.Blue: return "荐加己";
            case Type.Yellow: return "堡加己";
            case Type.Green: return "浅加己";
            case Type.Purple: return "鞠加己";
            default: return string.Empty;
        }
    }
}
