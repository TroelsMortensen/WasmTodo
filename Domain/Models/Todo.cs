using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Todo
{
    public int Id { get; set; }
    public User Owner { get; private set; }
    public int OwnerId { get; set; }
    public string Title { get; private set; }

    public bool IsCompleted { get; set; }

    public Todo(int ownerId, string title)
    {
        OwnerId = ownerId;
        Title = title;
    }
    
    private Todo(){}
}