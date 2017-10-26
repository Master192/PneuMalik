using System.Xml.Serialization;

namespace PneuMalik.Models.Configurator
{
    public class DataLine2
    {
        [XmlElement("BrandCode")]
        public string BrandCode { get; set; }
        [XmlElement("TypeCode")]
        public string TypeCode { get; set; }
        [XmlElement("TypeText")]
        public string TypeText { get; set; }
        [XmlElement("BoltPattern")]
        public string BoltPattern { get; set; }
        [XmlElement("BoltCircle")]
        public string BoltCircle { get; set; }
        [XmlElement("CenterBore")]
        public string CenterBore { get; set; }
    }
}