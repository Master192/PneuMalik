namespace PneuMalik.Models.Dto
{
    public class WebCathegory : Cathegory
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public bool Default { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
        public string Annotation { get; set; }
        public string Content { get; set; }

    }
}