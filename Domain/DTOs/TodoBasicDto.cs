namespace Domain.DTOs;

public class TodoBasicDto
{
    public int Id { get; }
    public string OwnerName { get; }
    public string Title { get; }
    public bool IsCompleted { get;  }

    public TodoBasicDto(int id, string ownerName, string title, bool isCompleted)
    {
        Id = id;
        OwnerName = ownerName;
        Title = title;
        IsCompleted = isCompleted;
    }
}