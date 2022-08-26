﻿using Domain.Models;

namespace Domain.DaoInterfaces;

public interface ITodoDao
{
    Task<Todo> Create(Todo todo);

}