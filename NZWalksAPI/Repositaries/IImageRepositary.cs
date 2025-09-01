using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositaries
{
    public interface IImageRepositary
    {
        Task<Image> Upload(Image image);
    }
}
