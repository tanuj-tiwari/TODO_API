namespace TODO_API.Model
{
    public class TODOModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public string? Status { get; set; }
        public string? CreatedBy { get; set; }
        public string? StatusMessage { get; set; }
    }
}
