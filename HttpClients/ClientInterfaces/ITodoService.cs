using Domain.DTOs;

namespace HttpClients.ClientInterfaces;

public interface ITodoService
{
    Task Create(TodoCreationDto dto);
}