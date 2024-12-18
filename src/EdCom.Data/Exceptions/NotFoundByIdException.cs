namespace EdCom.Data.Exceptions;

public abstract class NotFoundByIdException : Exception
{
    public NotFoundByIdException(string message) : base(message)
    {
    }
}
