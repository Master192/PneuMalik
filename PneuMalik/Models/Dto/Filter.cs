using System.Collections.Generic;

namespace PneuMalik.Models.Dto
{
    public class Filter
    {
        public List<int> Manufacturers { get; set; }
        public List<int> Seasons { get; set; }
        public List<int> Widths { get; set; }
        public List<int> Rims { get; set; }
        public List<int> Profiles { get; set; }
        public List<string> Brands { get; set; }
        public List<string> Models { get; set; }
    }
}