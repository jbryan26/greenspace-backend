using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using TodoApi.Controllers;
using TodoApi.DTO;

namespace TodoApi.ExpressionHelper
{
    public static class ExpressionHelper
    {
        public static Expression<Func<T, bool>> ConstructAndExpressionTree<T>(List<FieldCondition> filters)
        {
            if (filters.Count == 0)
                return null;

            ParameterExpression param = Expression.Parameter(typeof(T), "t");
            Expression exp = null;

            if (filters.Count == 1)
            {
                exp = ExpressionHelper.ExpressionRetriever.GetExpression<T>(param, filters[0]);
            }
            else
            {
                exp = ExpressionHelper.ExpressionRetriever.GetExpression<T>(param, filters[0]);
                for (int i = 1; i < filters.Count; i++)
                {
                    exp = Expression.And(exp, ExpressionHelper.ExpressionRetriever.GetExpression<T>(param, filters[i]));
                }
            }

            return Expression.Lambda<Func<T, bool>>(exp, param);
        }

        public static class ExpressionRetriever
        {
            private static MethodInfo containsMethod = typeof(string).GetMethod("Contains");
            private static MethodInfo startsWithMethod = typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
            private static MethodInfo endsWithMethod = typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) });

            public static Expression GetExpression<T>(ParameterExpression param, FieldCondition filter)
            {
                MemberExpression member = Expression.Property(param, filter.Name);
                ConstantExpression constant = Expression.Constant(filter.Value);
                switch (filter.Condition)
                {
                    case Comparison.Equal:
                        return Expression.Equal(member, constant);
                    case Comparison.GreaterThan:
                        return Expression.GreaterThan(member, constant);
                    case Comparison.GreaterThanOrEqual:
                        return Expression.GreaterThanOrEqual(member, constant);
                    case Comparison.LessThan:
                        return Expression.LessThan(member, constant);
                    case Comparison.LessThanOrEqual:
                        return Expression.LessThanOrEqual(member, constant);
                    case Comparison.NotEqual:
                        return Expression.NotEqual(member, constant);
                    case Comparison.Contains:
                        return Expression.Call(member, containsMethod, constant);
                    case Comparison.StartsWith:
                        return Expression.Call(member, startsWithMethod, constant);
                    case Comparison.EndsWith:
                        return Expression.Call(member, endsWithMethod, constant);
                    default:
                        return null;
                }
            }

            public enum Comparison
            {
                Equal,
                LessThan,
                LessThanOrEqual,
                GreaterThan,
                GreaterThanOrEqual,
                NotEqual,
                Contains, //for strings  
                StartsWith, //for strings  
                EndsWith //for strings  
            }

        }
    }
}
