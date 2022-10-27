using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Todo
{
    public int Id { get; set; }
    public User Owner { get; private set; }
    public string Title { get; private set; }

    public bool IsCompleted { get; set; }

    public Todo(User owner, string title)
    {
        Owner = owner;
        Title = title;
    }
    
    private Todo(){}
}