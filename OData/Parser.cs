using Microsoft.OData.UriParser;
using Microsoft.OData.UriParser.Aggregation;
using System.Linq.Expressions;

namespace OData
{
    public static class Parser
    {
        private static readonly UriQueryExpressionParser _parser = new UriQueryExpressionParser(20);

        public static void ParseFilter(string filter)
        {
            var token = _parser.ParseFilter(filter);
            var visitor = new TreeVisitor();
            token.Accept(visitor);
        }
    }

    class TreeVisitor : ISyntacticTreeVisitor<Expression>
    {
        public Expression Visit(AllToken tokenIn)
        {
            throw new NotImplementedException();
        }

        public Expression Visit(AnyToken tokenIn)
        {
            throw new NotImplementedException();
        }

        public Expression Visit(BinaryOperatorToken tokenIn)
        {
            var type = tokenIn.OperatorKind switch
            {
                BinaryOperatorKind.And => ExpressionType.And,
                BinaryOperatorKind.Or => ExpressionType.Or,
                BinaryOperatorKind.Equal => ExpressionType.Equal,
                BinaryOperatorKind.GreaterThan => ExpressionType.GreaterThan,
            };
            return Expression.MakeBinary(type, tokenIn.Left.Accept(this), tokenIn.Right.Accept(this));
        }

        public Expression Visit(CountSegmentToken tokenIn)
        {
            throw new NotImplementedException();
        }

        public Expression Visit(InToken tokenIn)
        {
            throw new NotImplementedException();
        }

        public Expression Visit(DottedIdentifierToken tokenIn)
        {
            throw new NotImplementedException();
        }

        public Expression Visit(ExpandToken tokenIn)
        {
            throw new NotImplementedException();
        }

        public Expression Visit(ExpandTermToken tokenIn)
        {
            throw new NotImplementedException();
        }

        public Expression Visit(FunctionCallToken tokenIn)
        {
            throw new NotImplementedException();
        }

        public Expression Visit(LambdaToken tokenIn)
        {
            throw new NotImplementedException();
        }

        public Expression Visit(LiteralToken tokenIn)
        {
            return Expression.Constant(tokenIn.Value, typeof(object)); // TODO boxing
        }

        public Expression Visit(InnerPathToken tokenIn)
        {
            throw new NotImplementedException();
        }

        public Expression Visit(OrderByToken tokenIn)
        {
            throw new NotImplementedException();
        }

        public Expression Visit(EndPathToken tokenIn)
        {
            // TODO this needs to access member of target type (it being the queried model)
            return Expression.Parameter(typeof(object), tokenIn.Identifier);
            throw new NotImplementedException();
        }

        public Expression Visit(CustomQueryOptionToken tokenIn)
        {
            throw new NotImplementedException();
        }

        public Expression Visit(RangeVariableToken tokenIn)
        {
            throw new NotImplementedException();
        }

        public Expression Visit(SelectToken tokenIn)
        {
            throw new NotImplementedException();
        }

        public Expression Visit(SelectTermToken tokenIn)
        {
            throw new NotImplementedException();
        }

        public Expression Visit(StarToken tokenIn)
        {
            throw new NotImplementedException();
        }

        public Expression Visit(UnaryOperatorToken tokenIn)
        {
            throw new NotImplementedException();
        }

        public Expression Visit(FunctionParameterToken tokenIn)
        {
            throw new NotImplementedException();
        }

        public Expression Visit(AggregateToken tokenIn)
        {
            throw new NotImplementedException();
        }

        public Expression Visit(AggregateExpressionToken tokenIn)
        {
            throw new NotImplementedException();
        }

        public Expression Visit(EntitySetAggregateToken tokenIn)
        {
            throw new NotImplementedException();
        }

        public Expression Visit(GroupByToken tokenIn)
        {
            throw new NotImplementedException();
        }
    }
}