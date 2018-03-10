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
        public List<int> Brands { get; set; }
        public List<int> Models { get; set; }
    }
}