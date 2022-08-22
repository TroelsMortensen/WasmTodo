using System.Text.Json;
using Entities.Models;

namespace FileData;

public class FileContext
{
    private const string filePath = "data.json";

    private DataContainer? dataContainer;

    public ICollection<Todo> Todos
    {
        get
        {
            LazyLoadData();
            return dataContainer!.Todos;
        }
    }

    public ICollection<User> Users
    {
        get
        {
            LazyLoadData();
            return dataContainer!.Users;
        }
    }

    private void LazyLoadData()
    {
        if (dataContainer == null)
        {
            LoadData();
        }
    }

    private void LoadData()
    {
        string content = File.ReadAllText(filePath);
        dataContainer = JsonSerializer.Deserialize<DataContainer>(content);
    }

    public void SaveChanges()
    {
        string serialized = JsonSerializer.Serialize(dataContainer);
        File.WriteAllText(filePath, serialized);
        dataContainer = null;
    }
}