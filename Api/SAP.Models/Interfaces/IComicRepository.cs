using System;
using System.Collections.Generic;

namespace SAP.Models.Interfaces
{
    public interface IComicRepository
    {
        IEnumerable<Comic> GetTodaysComics();
        IEnumerable<Comic> GetComics(int episodeNumber);
        IEnumerable<Comic> GetComics(DateTime value);
    }
}