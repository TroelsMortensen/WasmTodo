using System.Net.Http.Json;
using System.Text.Json;
using Domain.DTOs;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class TodoHttpClient : ITodoService
{
    private readonly HttpClient client;

    public TodoHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task CreateAsync(TodoCreationDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/todos",dto);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
}