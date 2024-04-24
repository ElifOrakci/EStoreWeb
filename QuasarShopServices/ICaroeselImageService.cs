using Microsoft.EntityFrameworkCore.Query.Internal;
using QuasarShopData;

namespace QuasarShopServices;

public  interface ICaroeselImageService
{
    IQueryable<CarouselImage> GetAll();
     CarouselImage? GetById(Guid  id);
    Task Create(string image, DateTime? dateFirst, DateTime? dateEnd, Guid userId, bool enabled = true);
    Task Update(Guid id, string image, DateTime? dateFirst, DateTime? dateEnd);
    Task Delete(Guid  id);
}


public class CarousrlImageService  : ICaroeselImageService
{
    private readonly AppDbContext context;

    public CarousrlImageService(
        AppDbContext context   
        )

    {
        this.context = context;
    }
        
      
    public async  Task Create(string image, DateTime? dateFirst, DateTime? dateEnd,  Guid userId, bool enabled = true)
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

    public async Task Delete(Guid id)
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

    public async Task Update( Guid id, string  image, DateTime? dateFirst, DateTime? dateEnd)
    {
        var item = await context.CarouselImages.FindAsync(id);

        item.DateEnd = dateEnd;
        item.DateFirst= dateFirst;
        item.Image = image;

        context.CarouselImages.Update(item);
        await context.SaveChangesAsync();
    }
}

