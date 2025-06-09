using Microsoft.EntityFrameworkCore;
using PersonManagement.DAL.Entities;
using PersonManagement.DAL.Interfaces;
using System.Linq.Expressions;

namespace PersonManagement.DAL.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly AppDbContext _context;

    public PersonRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Person>> GetAllAsync(Expression<Func<Person, bool>>? filter = null, params Expression<Func<Person, object>>[] includes)
    {
        IQueryable<Person> query = _context.Persons;

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.ToListAsync();
    }

    public async Task AddAsync(Person entity)
    {
        await _context.Persons.AddAsync(entity);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
