namespace Domain.DTOs;

public class TodoCreationDto
{
    public int OwnerId { get; }
    public string Title { get; }

    public TodoCreationDto(int ownerId, string title)
    {
        OwnerId = ownerId;
        Title = title;
    }
}