using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace PneuMalik.Models.Dto
{
    public class Cathegory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public bool Default { get; set; }
        public bool Active { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
        public string Annotation { get; set; }

        [AllowHtml]
        public string Content { get; set; }
        public int ItemsOnPage { get; set; }
        public int Type { get; set; }
        public string ExternalUrl { get; set; }
    }
}