namespace TodoApi.DTO
{
    public class FieldCondition
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public  Condition Condition { get; set; }
    }
}