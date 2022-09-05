using Microsoft.EntityFrameworkCore.Design;

namespace ChildObjectsEf.Data;

public class ChildObjectsEfContextFactory : IDesignTimeDbContextFactory<ChildObjectsEfContext>
{
    public ChildObjectsEfContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<ChildObjectsEfContext> optionsBuilder = new();
        optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ChildObjectsEfDb;Integrated Security=True");

        return new ChildObjectsEfContext(optionsBuilder.Options);
    }
}
