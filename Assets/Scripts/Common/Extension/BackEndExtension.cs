using BackEnd;

public static class BackEndExtension
{
    public static bool IsNotSuccess(this BackendReturnObject returnObject)
    {
        return !returnObject.IsSuccess();
    }

}
