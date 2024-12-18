using System.Linq.Expressions;
using EdCom.Data.Exceptions;
using EdCom.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EdCom.Data;

public static class EntityExtensions
{
    public static Task<T?> TryGetById<T>(
        this IQueryable<T> query,
        Guid id,
        CancellationToken cancellationToken)
        where T : IEntity => query.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

    public static async Task<T> GetById<T>(
        this IQueryable<T> query,
        Guid id,
        CancellationToken cancellationToken = default)
        where T : IEntity
    {
        var entity = await query.TryGetById(id, cancellationToken);

        if (entity == null)
            throw new DbEntityNotFoundByIdException<T>(id);

        return entity;
    }

    public static async Task<TResult> GetById<TS, TResult>(
        this IQueryable<TS> query,
        Guid id,
        Expression<Func<TS, TResult>> selector,
        CancellationToken cancellationToken = default)
        where TS : IEntity
    {
        var result = await query
            .Where(p => p.Id == id)
            .Select(selector)
            .FirstOrDefaultAsync(cancellationToken);

        if (result == null)
            throw new DbEntityNotFoundByIdException<TS>(id);

        return result;
    }
}
