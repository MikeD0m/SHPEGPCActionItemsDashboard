namespace SHPEGPCActionItemsDashboard.Models
{
    public class ActionItems
    {
        public int AI_ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsDone { get; set; } = false;
        public DateTime? CompletedDate { get; set; } = null;

    }
}
