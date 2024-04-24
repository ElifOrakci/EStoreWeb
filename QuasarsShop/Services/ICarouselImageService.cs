using Microsoft.EntityFrameworkCore.Query.Internal;
using QuasarShopData;

namespace QuasarShop;

public interface ICarouselImageService
{
    IQueryable<CarouselImage> GetAll();
     CarouselImage? GetById(Guid  id);
    Task CreateAsync(string image, DateTime? dateFirst, DateTime? dateEnd, Guid userId, bool enabled = true);
    Task UpdateAsync(Guid id, string image, DateTime? dateFirst, DateTime? dateEnd, bool enabled);
    Task DeleteAsync(Guid  id);
}


public class CarouselImageService  : ICarouselImageService
{
    private readonly AppDbContext context;

    public CarouselImageService(
        AppDbContext context   
        )

    {
        this.context = context;
    }
        
      
    public async  Task CreateAsync(string image, DateTime? dateFirst, DateTime? dateEnd,  Guid userId, bool enabled = true)
    {
        var item = new CarouselImage
        {
            Image = image,
            DateFirst = dateFirst,
            DateEnd = dateEnd,
            DateCreated = DateTime.UtcNow,
            UserId = userId,
            Enabled= enabled,
            

        };
        context.CarouselImages.Add(item);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var item = await context.CarouselImages.FindAsync(id);
        context.CarouselImages.Remove (item!);
        await context.SaveChangesAsync();

    }

    public IQueryable<CarouselImage> GetAll()
    {
       return context.CarouselImages;
    }

    public CarouselImage? GetById(Guid id)
    {
        return context.CarouselImages.Find(id);
    }

    public async Task UpdateAsync( Guid id, string  image, DateTime? dateFirst, DateTime? dateEnd,bool enabled)
    {
        var item = await context.CarouselImages.FindAsync(id);

        item.DateEnd = dateEnd;
        item.DateFirst= dateFirst;
        item.Image = image;
        item.Enabled = enabled;
        context.CarouselImages.Update(item);
        await context.SaveChangesAsync();
    }
}

