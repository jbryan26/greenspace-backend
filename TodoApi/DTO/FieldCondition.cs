using System.ComponentModel.DataAnnotations;
using TodoApi.Controllers;

namespace TodoApi.DTO
{
    public class FieldCondition
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public object Value { get; set; }
        [Required]
        public ExpressionHelper.ExpressionHelper.ExpressionRetriever.Comparison Condition { get; set; }
    }
}