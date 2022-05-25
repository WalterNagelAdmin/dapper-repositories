namespace MicroOrm.Dapper.Repositories.SqlGenerator.QueryExpressions
{
#pragma warning disable

    /// <summary>
    /// Abstract Query Expression
    /// </summary>
    internal abstract class QueryExpression
    {
        /// <summary>
        /// Operator OR/AND
        /// </summary>
        public string LinkingOperator { get; set; }

        /// <summary>
        /// Query Expression Node Type
        /// </summary>
        public QueryExpressionType NodeType { get; set; }

        public override string ToString()
        {
            return $"[NodeType:{this.NodeType}, LinkingOperator:{LinkingOperator}]";
        }
    }
}
