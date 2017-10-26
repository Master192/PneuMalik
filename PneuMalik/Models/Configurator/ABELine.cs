using System.Xml.Serialization;

namespace PneuMalik.Models.Configurator
{
    public class ABELine
    {
        [XmlElement("ABENr")]
        public string ABENr { get; set; }
        [XmlElement("ABENG")]
        public string ABENG { get; set; }
        [XmlElement("Type")]
        public string Type { get; set; }
        [XmlElement("TPMS")]
        public string TPMS { get; set; }
    }
}