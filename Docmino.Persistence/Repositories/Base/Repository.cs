using Docmino.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace Docmino.Persistence.Repositories.Base;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    protected IDbContextTransaction? _transaction;
    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }
    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public void DeleteRange(IEnumerable<T> listEntity)
    {
        _context.Set<T>().RemoveRange(listEntity);
    }

    public async Task AddRangeAsync(IEnumerable<T> listEntity)
    {
        await _context.Set<T>().AddRangeAsync(listEntity);
    }


    public virtual async Task<(IEnumerable<TResult> Data, int TotalCount)> GetByFilterAsync<TResult>(int? pageSize, int? pageNumber,
                                                                                                Expression<Func<T, TResult>> selectQuery,
                                                                                                Expression<Func<T, bool>>? predicate = null,
                                                                                                (Expression<Func<T, object>> OrderBy, bool IsDescending)[]? orderByExpressions = null,
                                                                                                CancellationToken token = default,
                                                                                                params Expression<Func<T, object>>[] navigationProperties)
    {
        var query = GetQuery(predicate, navigationProperties);
        var totalCount = await query.CountAsync(token);

        if (orderByExpressions != null && orderByExpressions.Any())
        {
            IOrderedQueryable<T>? orderedQuery = null;
            for (int i = 0; i < orderByExpressions.Length; i++)
            {
                var orderByExpression = orderByExpressions[i];
                var orderBy = orderByExpression.OrderBy;
                var isDescending = orderByExpression.IsDescending;

                if (i == 0)
                {
                    orderedQuery = isDescending ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);
                }
                else
                {
                    orderedQuery = isDescending ? orderedQuery!.ThenByDescending(orderBy) : orderedQuery!.ThenBy(orderBy);
                }

            }
            query = orderedQuery!;
        }

        IQueryable<TResult> projectedQuery = query.Select(selectQuery);

        if (pageSize.HasValue && pageNumber.HasValue)
        {
            projectedQuery = projectedQuery.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);
        }

        //var queryString = projectedQuery.ToQueryString();
        return (await projectedQuery.ToListAsync(token), totalCount);
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null,
                                             CancellationToken token = default,
                                             params Expression<Func<T, object>>[] navigationProperties)
    {
        return await GetQuery(predicate, navigationProperties).ToListAsync(token);
    }
    public virtual async Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<T, bool>>? predicate = null,
                                                                             Expression<Func<T, TResult>>? selectQuery = null,
                                                                             (Expression<Func<T, object>> OrderBy, bool IsDescending)[]? orderByExpressions = null,
                                                                             CancellationToken token = default,
                                                                             params Expression<Func<T, object>>[] navigationProperties)
    {
        if (selectQuery == null) throw new ArgumentNullException(nameof(selectQuery));
        var query = GetQuery(predicate, navigationProperties);
        if (orderByExpressions != null && orderByExpressions.Any())
        {
            IOrderedQueryable<T>? orderedQuery = null;
            for (int i = 0; i < orderByExpressions.Length; i++)
            {
                var orderByExpression = orderByExpressions[i];
                var orderBy = orderByExpression.OrderBy;
                var isDescending = orderByExpression.IsDescending;

                if (i == 0)
                {
                    orderedQuery = isDescending ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);
                }
                else
                {
                    orderedQuery = isDescending ? orderedQuery!.ThenByDescending(orderBy) : orderedQuery!.ThenBy(orderBy);
                }

            }
            query = orderedQuery!;
        }
        return await query.Select(selectQuery).ToListAsync(token);
    }


    public virtual async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken token = default, params Expression<Func<T, object>>[] navigationProperties)
    {
        return await GetQuery(predicate, navigationProperties).FirstOrDefaultAsync(token);
    }


    public async Task<TResult?> FirstOrDefaultAsync<TResult>(Expression<Func<T, bool>>? predicate = null, Expression<Func<T, TResult>> selectQuery = null!, CancellationToken token = default, params Expression<Func<T, object>>[] navigationProperties)
    {
        if (selectQuery == null) throw new ArgumentNullException(nameof(selectQuery));
        var query = GetQuery(predicate, navigationProperties);
        return await query.Select(selectQuery).FirstOrDefaultAsync(token);
    }

    public async Task<TResult> FirstAsync<TResult>(Expression<Func<T, bool>>? predicate = null, Expression<Func<T, TResult>> selectQuery = null!, CancellationToken token = default, params Expression<Func<T, object>>[] navigationProperties)
    {
        if (selectQuery == null) throw new ArgumentNullException(nameof(selectQuery));
        var query = GetQuery(predicate, navigationProperties);
        return await query.Select(selectQuery).FirstAsync(token);
    }

    public virtual async Task<T> FirstAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken token = default, params Expression<Func<T, object>>[] navigationProperties)
    {
        return await GetQuery(predicate, navigationProperties).FirstAsync(token);
    }

    public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken token = default, params Expression<Func<T, object>>[] navigationProperties)
    {
        return await GetQuery(predicate, navigationProperties).AnyAsync(token);
    }

    public virtual async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken token = default, params Expression<Func<T, object>>[] navigationProperties)
    {
        return await GetQuery(predicate, navigationProperties).CountAsync(token);
    }

    public virtual async Task<int> SumAsync(Expression<Func<T, int>> selector, Expression<Func<T, bool>>? predicate = null, CancellationToken token = default, params Expression<Func<T, object>>[] navigationProperties)
    {
        return await GetQuery(predicate, navigationProperties).SumAsync(selector);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    protected IQueryable<T> GetQuery(Expression<Func<T, bool>>? predicate = null, params Expression<Func<T, object>>[] navigationProperties)
    {
        IQueryable<T> dbQuery = _context.Set<T>().AsNoTracking();
        if (predicate != null)
        {
            dbQuery = dbQuery.Where(predicate);
        }
        return navigationProperties.Aggregate(dbQuery, (current, navigationProperty) => current.Include(navigationProperty));
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }

    }

    public async Task RollbackAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }

    }

    public async Task ExecuteDeleteAsync(Expression<Func<T, bool>>? predicate = null)
    {
        if (predicate != null)
        {
            await _context.Set<T>().Where(predicate).ExecuteDeleteAsync();
        }
    }

    public async Task ExecuteUpdateAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, params (Expression<Func<T, object?>> Property, object? Value)[] updates)
    {
        var updateExpression = ExpressionProvider<T>.BuildUpdateExpression(updates);
        await _context.Set<T>().Where(predicate).ExecuteUpdateAsync(updateExpression, cancellationToken);
    }

    public async Task<TResult?> ExecuteRawSqlSingleAsync<TResult>(string sql, params object[] parameters)
    {
        return await _context.Database.SqlQueryRaw<TResult>(sql, parameters).SingleOrDefaultAsync();
    }

    public async Task<List<TResult>> ExecuteRawSqlAsync<TResult>(string sql, params object[] parameters)
    {
        return await _context.Database.SqlQueryRaw<TResult>(sql, parameters).ToListAsync();
    }

    public async Task<int> ExecuteRawSqlNonQueryAsync(string sql, params object[] parameters)
    {
        return await _context.Database.ExecuteSqlRawAsync(sql, parameters);
    }


}