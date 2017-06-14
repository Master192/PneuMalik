using System;

namespace PneuMalik.Models.Dto
{
    public class Season
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}