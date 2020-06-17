using MVCCaching;
using Sap.Models;

namespace Generic.Controllers
{
    public interface IComicRepository : IRepository
    {
        Comic GetTodaysComic();
    }
}