namespace TodoApi.Models
{
    /*public enum ResourceType
    {
        MeetingRoom,
        TrainingRoom,
        Desk,
        ParkingSpace
    }*/
    public class ResourceType
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Plural { get; set; }
    }
    
}