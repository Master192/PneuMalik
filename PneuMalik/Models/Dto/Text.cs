using System.Web.Mvc;

namespace PneuMalik.Models.Dto
{
    public class Text
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }

        [AllowHtml]
        public string Content { get; set; }
    }
}