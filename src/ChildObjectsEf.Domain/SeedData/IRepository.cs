﻿namespace ChildObjectsEf.Domain.SeedData;

public interface IRepository<T> where T : IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }
}
