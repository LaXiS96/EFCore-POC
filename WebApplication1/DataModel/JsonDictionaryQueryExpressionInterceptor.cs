using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Linq.Expressions;

namespace WebApplication1.DataModel
{
    public class JsonDictionaryQueryExpressionInterceptor : IQueryExpressionInterceptor
    {
        // TODO translate dictionary access (get_Item, ContainsKey, ?)

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
                //if (node.Method == AsJsonDictionaryMethod)
                //{
                //}

                return base.VisitMethodCall(node);
            }
        }
    }
}
