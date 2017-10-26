using System.Xml.Serialization;

namespace PneuMalik.Models.Configurator
{
    public class DataLine
    {
        [XmlElement("BrandCode")]
        public string BrandCode { get; set; }
        [XmlElement("BrandText")]
        public string BrandText { get; set; }
    }
}