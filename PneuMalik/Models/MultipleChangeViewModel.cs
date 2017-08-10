using System.Collections.Generic;

namespace PneuMalik.Models
{
    public class MultipleChangeViewModel
    {

        public IList<string> Manufacturers { get; set; }
        public IList<string> Types { get; set; }
        public IList<string> IndexesSi { get; set; }
    }
}