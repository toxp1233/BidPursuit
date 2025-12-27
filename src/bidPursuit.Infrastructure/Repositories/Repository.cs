using bidPursuit.Domain.Interfaces;
using bidPursuit.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace bidPursuit.Infrastructure.Repositories;

public class Repository<T>(BidPursuitDbContext context)
    : IRepository<T> where T : class
{
    private readonly BidPursuitDbContext _context = context;

    public Task AddAsync(T entity, CancellationToken cancellationToken) =>
        _context.Set<T>().AddAsync(entity, cancellationToken).AsTask();

    public Task<List<T>> GetAllAsync(CancellationToken cancellationToken) =>
        _context.Set<T>().ToListAsync(cancellationToken);

    public Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        _context.Set<T>().FindAsync([id], cancellationToken).AsTask();

    public void Update(T entity) =>
        _context.Set<T>().Update(entity);

    public void Delete(T entity) =>
        _context.Set<T>().Remove(entity);

    public IQueryable<T> Query() =>
        _context.Set<T>().AsQueryable();
}
