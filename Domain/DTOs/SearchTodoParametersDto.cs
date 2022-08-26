namespace Domain.DTOs;

public class SearchTodoParametersDto
{
    public string? Username { get;}
    public int? UserId { get;}
    public bool? CompletedStatus { get;}
    public string? TitleContains { get;}

    public SearchTodoParametersDto(string? username, int? userId, bool? completedStatus, string? titleContains)
    {
        Username = username;
        UserId = userId;
        CompletedStatus = completedStatus;
        TitleContains = titleContains;
    }
}