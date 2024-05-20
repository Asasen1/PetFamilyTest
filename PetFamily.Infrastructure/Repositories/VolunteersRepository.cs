using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Features.Volunteers;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;
using PetFamily.Infrastructure.DbContexts;

namespace PetFamily.Infrastructure.Repositories;

public class VolunteersRepository : IVolunteersRepository
{
    private readonly PetFamilyWriteDbContext _dbContext;

    public VolunteersRepository(PetFamilyWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Volunteer volunteer, CancellationToken ct)
    {
        await _dbContext.Volunteers.AddAsync(volunteer, ct);
    }

    public async Task<Result<int, Error>> Save(CancellationToken ct)
    {
        var result = await _dbContext.SaveChangesAsync(ct);

        if (result == 0)
            return Errors.General.SaveFailure("Volunteer");

        return result;
    }
    
    public async Task<Result<Volunteer, Error>> GetById(Guid id, CancellationToken ct)
    {
        var volunteer = await _dbContext.Volunteers
            .Include(v => v.Pets)
            .Include(v => v.Photos)
            .FirstOrDefaultAsync(v => v.Id == id, cancellationToken: ct);

        if (volunteer is null)
            return Errors.General.NotFound(id);

        return volunteer;
    }

    public async Task<Result<List<Volunteer>, Error>> GetAll(int size, int page, CancellationToken ct)
    {
        var volunteers = await _dbContext.Volunteers
            .Include(v => v.Pets)
            .Include(v => v.Photos).AsNoTracking().ToListAsync(cancellationToken: ct);
        if (volunteers.Count == 0)
            return Errors.General.NotFound();
        if (size != 0 && page != 0)
        {
            var volunteersWithPagination = await _dbContext.Volunteers
                .Include(v => v.Pets)
                .Include(v => v.Photos)
                .Skip(size*page)
                .Take(size)
                .AsNoTracking()
                .ToListAsync(cancellationToken: ct);
            if (volunteers.Count == 0)
                return Errors.General.NotFound();
            return volunteersWithPagination;
        }
        return volunteers;
    }
}