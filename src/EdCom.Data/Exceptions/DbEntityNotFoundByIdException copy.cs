using EdCom.Domain.Abstractions;

namespace EdCom.Data.Exceptions;

public class DbEntityNotFoundByIdException<T> : NotFoundByIdException
    where T : IEntity
{
    public DbEntityNotFoundByIdException(Guid id) : base($"Not found. Entity type: {typeof(T).Name}. Id: {id}.")
    {
    }
}
