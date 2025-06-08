namespace Roomies.Application.Interfaces;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}
