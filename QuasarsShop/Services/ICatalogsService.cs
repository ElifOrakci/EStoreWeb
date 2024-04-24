using Microsoft.EntityFrameworkCore;
using QuasarShopData;

namespace QuasarShop;

public interface ICatalogsService
{
    IQueryable<Catalog> GetAll();
    Task<Catalog?> GetById(Guid  id);

    Task Create(Catalog item);
    Task Create(string Name,bool  enabled,Guid userId);
    Task Update(Catalog item);
    Task Delete(Guid id);
}

public class CatalogsService : ICatalogsService
{
    private readonly AppDbContext context;

    public CatalogsService(
        AppDbContext context
        )
    {
        this.context = context;
    }

    public async Task Create(Catalog item)
    {
        item.DateCreated = DateTime.UtcNow;

        await context.Catalogs.AddAsync(item);
        await context.SaveChangesAsync();
    }
    public async Task Create(string name, bool enabled, Guid userId)
    {
        await Create(new Catalog { UserId = userId, Name = name, Enabled = enabled });
    }

    public async Task Delete(Guid id)
    {
        var item = await GetById(id);
        if (item is null)
            throw new Exception("Katalog bulunamadı");
        context.Catalogs.Remove(item);
        await context.SaveChangesAsync();
    }

    public IQueryable<Catalog> GetAll()
    {
        return context.Catalogs.AsQueryable<Catalog>();
    }
    
    public async Task<IEnumerable<Catalog>>GetCatalogsAsync()
    {
        return await context.Catalogs.ToListAsync();
    }

    public Task<Catalog?> GetById(Guid id)
    {
        return context.Catalogs.SingleOrDefaultAsync(p => p.Id == id);
    }

    public async Task Update(Catalog item)
    {
        context.Catalogs.Update(item);
        await context.SaveChangesAsync();
    }


}

