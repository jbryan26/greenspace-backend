using TodoApi.Controllers;

namespace TodoApi.DTO
{
    public class FieldCondition
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public ExpressionHelper.ExpressionHelper.ExpressionRetriever.Comparison Condition { get; set; }
    }
}