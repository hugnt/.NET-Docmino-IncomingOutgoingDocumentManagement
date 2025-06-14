using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Docmino.Persistence.Repositories.Base;

public class ExpressionProvider<T> where T : class
{
    public static Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> BuildUpdateExpression((Expression<Func<T, object?>> Property, object? Value)[] updates)
    {
        if (updates == null || !updates.Any()) throw new ArgumentException("Updates cannot be null or empty.");

        Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> updateExpression = b => b;

        foreach (var (property, value) in updates)
        {
            if (property.Body is not MemberExpression && property.Body is not UnaryExpression { Operand: MemberExpression })
                throw new ArgumentException($"Property expression {property} must be a member access expression.");

            var setPropertyMethod = typeof(SetPropertyCalls<T>).GetMethod(nameof(SetPropertyCalls<T>.SetProperty));
            if (setPropertyMethod == null)
                throw new InvalidOperationException("SetProperty method not found.");

            var setPropertyCall = Expression.Lambda<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>>(
                Expression.Call(updateExpression.Body, setPropertyMethod, property, Expression.Constant(value)),
                updateExpression.Parameters
            );

            updateExpression = setPropertyCall;
        }

        return updateExpression;
    }
}
