using System.Xml.Serialization;

namespace PneuMalik.Models.Configurator
{
    public class TyreLine
    {
        [XmlElement("Tyre")]
        public string Tyre { get; set; }
        [XmlElement("isABE")]
        public string isABE { get; set; }
        [XmlElement("Axle")]
        public string Axle { get; set; }
        [XmlElement("SnowChains")]
        public string SnowChains { get; set; }
    }
}