using System;
using System.Collections.Generic;


namespace SAP.Models
{
    public class ComicResponse
    {
        public IEnumerable<Comic> Comics { get; set; } = new List<Comic>();
        public DateTime Date { get; set; }
    }
}