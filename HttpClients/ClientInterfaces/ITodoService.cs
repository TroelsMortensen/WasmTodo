using Domain.DTOs;

namespace HttpClients.ClientInterfaces;

public interface ITodoService
{
    Task CreateAsync(TodoCreationDto dto);
}