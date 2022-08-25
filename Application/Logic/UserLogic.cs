using Domain.DaoInterfaces;
using Domain.DTOs;
using Domain.LogicInterfaces;
using Domain.Models;

namespace Domain.Logic;

public class UserLogic : IUserLogic
{
    private readonly IUserDao userDao;

    public UserLogic(IUserDao userDao)
    {
        this.userDao = userDao;
    }

    public async Task<User> Create(UserCreationDto dto)
    {
        User? existing = await userDao.GetByUsername(dto.UserName);
        if (existing != null)
            throw new Exception("Username already taken!");

        ValidateData(dto);
        User toCreate = new User
        {
            UserName = dto.UserName
        };
        
        User created = await userDao.Create(toCreate);
        
        return created;
    }

    public Task<ICollection<User>> Get(SearchUserParametersDto searchParameters)
    {
        return userDao.Get(searchParameters);
    }

    private static void ValidateData(UserCreationDto userToCreate)
    {
        string userName = userToCreate.UserName;

        if (userName.Length < 3)
            throw new Exception("Username must be at least 3 characters!");

        if (userName.Length > 15)
            throw new Exception("Username must be less than 16 characters!");
    }
}