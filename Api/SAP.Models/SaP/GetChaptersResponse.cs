using System;
using System.Collections.Generic;
using System.Text;

namespace SAP.Models.SaP
{
    public class GetChaptersResponse
    {
        public IEnumerable<Chapter> Chapters { get; set; }
        public string Error { get; set; }
    }
}
