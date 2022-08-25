﻿using Domain.DTOs;
using Domain.Models;

namespace Domain.LogicInterfaces;

public interface IUserLogic
{
    public Task<User> Create(UserCreationDto dto);
    public Task<ICollection<User>> Get(SearchUserParametersDto searchParameters);
}