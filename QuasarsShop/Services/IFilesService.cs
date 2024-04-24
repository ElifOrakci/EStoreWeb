using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp;

namespace QuasarShop;

public  interface IFilesService
{
    Task<string> ResizeImageAsync(Stream imageStrem, Stream watermarkImage, Size size);
    Task<string> ResizeImageAsync(Stream imageStrem, Stream watermarkImage, int width,int height);
      Task<string> ResizeImageAsync(Stream imageStrem,  Size size);
    Task<string> ResizeImageAsync(Stream imageStrem, int width,int height);
}

public class FileService : IFilesService
{
    public async Task<string> ResizeImageAsync(Stream imageStrem, Stream watermarkImage, Size size)
    {
        var image = await Image.LoadAsync(imageStrem);

        image.Mutate(p =>
        {
            p.Resize(new ResizeOptions
            {
                Size = size,
                Mode = ResizeMode.Pad

            });
        });
        image.Mutate((p) =>
        {
            var watermark = Image.Load(watermarkImage);
            p.DrawImage(watermark, 0.3f);
        });
        return image.ToBase64String(JpegFormat.Instance);
    }

   
    public async  Task<string> ResizeImageAsync(Stream imageStrem, Stream watermarkImage, int width, int height)
    {
        return await ResizeImageAsync(imageStrem, watermarkImage,new Size (width, height));
    }
    public async Task<string> ResizeImageAsync(Stream imageStrem,  Size size)
    {
        var image = await Image.LoadAsync(imageStrem);

        image.Mutate(p =>
        {
            p.Resize(new ResizeOptions
            {
                Size = size,
                Mode = ResizeMode.Pad

            });
        });
      
        return image.ToBase64String(JpegFormat.Instance);
    }


    public async Task<string> ResizeImageAsync(Stream imageStrem,  int width, int height)
    {
        return await ResizeImageAsync(imageStrem,  new Size(width, height));
    }

}

