using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

namespace WebApplication1.DataModel
{
    public class JsonDictionaryQueryExpressionInterceptor : IQueryExpressionInterceptor
    {
        // TODO we cannot translate the method itself... we need to translate dictionary accesses
        private static readonly MethodInfo AsJsonDictionaryMethod =
            typeof(DbFunctionsExtensions).GetMethod(nameof(DbFunctionsExtensions.AsJsonDictionary))!;

        public Expression QueryCompilationStarting(
            Expression queryExpression,
            QueryExpressionEventData eventData)
        {
            var newExpression = new ExpressionModifier().Modify(queryExpression);
            return newExpression;
        }

        private class ExpressionModifier : ExpressionVisitor
        {
            public Expression Modify(Expression expression)
            {
                return Visit(expression);
            }

            protected override Expression VisitMethodCall(MethodCallExpression node)
            {
                if (node.Method == AsJsonDictionaryMethod)
                {
                    // TODO argument [1] is the json string member access?
                    //node.Arguments[1]

                    return base.VisitMethodCall(node);
                }

                return base.VisitMethodCall(node);
            }
        }
    }
}
