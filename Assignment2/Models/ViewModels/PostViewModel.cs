namespace Assignment2.Models.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Publisher { get; set; }
        public int? CategoryId { get; set; }
        public CategoryViewModel Category { get; set; }

        public string? Photo { get; set; }
    }
}
