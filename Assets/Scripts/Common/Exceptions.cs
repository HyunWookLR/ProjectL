using System;
using BackEnd;

public class InvalidBackEndException : Exception
{
    public InvalidBackEndException(BackendReturnObject returnObject) : 
        base($"StatusCode:{returnObject.GetStatusCode()}\nErrorCode:{returnObject.GetErrorCode()}\nMessage:{returnObject.GetMessage()}") { }
}
