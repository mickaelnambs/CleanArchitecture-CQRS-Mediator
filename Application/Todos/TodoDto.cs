namespace Application.Todos
{
    public class TodoDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; } 
        public string Status { get; set; }
    }
}