using System.Linq.Expressions;

namespace PET.Domain.Specifications
{
    class ReplaceParameter : ExpressionVisitor
    {
        public Expression OriginalParameter { get; set; }
        public Expression NewParameter { get; set; }
        protected override Expression VisitParameter(ParameterExpression node)
        {
            return node == this.OriginalParameter ? this.NewParameter : base.VisitParameter(node);
        }
    }
}