namespace Sincronia.Models
{
    public class PlannerRequest
    {
        public string DietType { get; set; } = string.Empty;
        public int MaxReadyTime { get; set; }
        public List<string> Tasks { get; set; } = new List<string>();
    }
}