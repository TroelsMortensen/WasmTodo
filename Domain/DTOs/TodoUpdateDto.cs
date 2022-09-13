namespace Domain.DTOs;

public class TodoUpdateDto
{
    public int Id { get; }
    public int OwnerId { get; }
    public string Title { get; }
    public bool IsCompleted { get; }

    public TodoUpdateDto(int id, int ownerId, string title, bool isCompleted)
    {
        Id = id;
        OwnerId = ownerId;
        Title = title;
        IsCompleted = isCompleted;
    }
}