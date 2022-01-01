
public static class EnumTranslator
{
    public static string GetTypeString(Type type)
    {
        switch (type)
        {
            case Type.Red: return "ȭ�Ӽ�";
            case Type.Blue: return "���Ӽ�";
            case Type.Yellow: return "���Ӽ�";
            case Type.Green: return "ǳ�Ӽ�";
            case Type.Purple: return "�ϼӼ�";
            default: return string.Empty;
        }
    }
}
