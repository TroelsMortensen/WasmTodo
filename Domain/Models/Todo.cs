namespace Domain.Models;

public class Todo
{
    public int Id { get; set; }
    public int OwnerId { get; set; }
    public string Title { get; set; }
    public bool IsCompleted { get; set; }

    public Todo(int ownerId, string title)
    {
        OwnerId = ownerId;
        Title = title;
    }
}