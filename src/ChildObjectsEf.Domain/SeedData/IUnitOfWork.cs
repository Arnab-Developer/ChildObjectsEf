namespace ChildObjectsEf.Domain.SeedData;

public interface IUnitOfWork : IDisposable
{
    Task SaveChangesAsync();
}