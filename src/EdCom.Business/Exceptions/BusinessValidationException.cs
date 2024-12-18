namespace EdCom.Business.Exceptions;

public class BusinessValidationException(string message) : Exception(message)
{
}