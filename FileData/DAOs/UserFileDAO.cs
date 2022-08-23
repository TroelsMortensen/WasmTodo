using Domain.DaoInterfaces;
using Domain.Models;

namespace FileData.DAOs;

public class UserFileDAO : IUserDao
{
    private readonly FileContext context;

    public UserFileDAO(FileContext context)
    {
        this.context = context;
    }

    public Task<User> Create(User user)
    {
        int nextId = context.Users.Max(u => u.Id);
        nextId++;

        user.Id = nextId;

        context.Users.Add(user);
        context.SaveChanges();

        return Task.FromResult(user);
    }

    public Task<User?> GetByUsername(string userName)
    {
        User? existing = context.Users.FirstOrDefault(u =>
            u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase)
        );
        return Task.FromResult(existing);
    }
}