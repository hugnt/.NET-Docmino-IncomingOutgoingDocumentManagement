using System.Linq.Expressions;

namespace Docmino.Domain.Abstractions;
public interface IRepository<T>
{
    public Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null,
                                CancellationToken token = default,
                                params Expression<Func<T, object>>[] navigationProperties);

    public Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<T, bool>>? predicate = null,
                                                           Expression<Func<T, TResult>>? selectQuery = null,
                                                           (Expression<Func<T, object>> OrderBy, bool IsDescending)[]? orderByExpressions = null,
                                                           CancellationToken token = default,
                                                           params Expression<Func<T, object>>[] navigationProperties);

    public Task<(IEnumerable<TResult> Data, int TotalCount)> GetByFilterAsync<TResult>(int? pageSize, int? pageNumber,
                                                                                       Expression<Func<T, TResult>> selectQuery,
                                                                                       Expression<Func<T, bool>>? predicate = null,
                                                                                       (Expression<Func<T, object>> OrderBy, bool IsDescending)[]? orderByExpressions = null,
                                                                                       CancellationToken token = default,
                                                                                       params Expression<Func<T, object>>[] navigationProperties);
    public Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>>? predicate = null,
                                        CancellationToken token = default,
                                        params Expression<Func<T, object>>[] navigationProperties);

    public Task<TResult?> FirstOrDefaultAsync<TResult>(Expression<Func<T, bool>>? predicate = null,
                                    Expression<Func<T, TResult>> selectQuery = null!,
                                    CancellationToken token = default,
                                    params Expression<Func<T, object>>[] navigationProperties);

    public Task<T> FirstAsync(Expression<Func<T, bool>>? predicate = null,
                                        CancellationToken token = default,
                                        params Expression<Func<T, object>>[] navigationProperties);

    public Task<TResult> FirstAsync<TResult>(Expression<Func<T, bool>>? predicate = null,
                                   Expression<Func<T, TResult>> selectQuery = null!,
                                   CancellationToken token = default,
                                   params Expression<Func<T, object>>[] navigationProperties);

    public Task<bool> AnyAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken token = default, params Expression<Func<T, object>>[] navigationProperties);
    public Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken token = default, params Expression<Func<T, object>>[] navigationProperties);
    public Task<int> SumAsync(Expression<Func<T, int>> selector, Expression<Func<T, bool>>? predicate = null, CancellationToken token = default, params Expression<Func<T, object>>[] navigationProperties);
    public Task AddRangeAsync(IEnumerable<T> listEntity);
    public void Add(T entity);
    public void Update(T entity);
    public void Delete(T entity);
    public void DeleteRange(IEnumerable<T> listEntity);
    public Task SaveChangesAsync(CancellationToken cancellationToken = default);
    public Task ExecuteDeleteAsync(Expression<Func<T, bool>>? predicate = null);
    public Task ExecuteUpdateAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, params (Expression<Func<T, object?>> Property, object? Value)[] updates);
    public Task BeginTransactionAsync();
    public Task CommitAsync();
    public Task RollbackAsync();

    public Task<TResult?> ExecuteRawSqlSingleAsync<TResult>(string sql, params object[] parameters);
    public Task<List<TResult>> ExecuteRawSqlAsync<TResult>(string sql, params object[] parameters);
    public Task<int> ExecuteRawSqlNonQueryAsync(string sql, params object[] parameters);
}

