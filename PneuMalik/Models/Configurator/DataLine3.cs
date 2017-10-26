using System.Xml.Serialization;

namespace PneuMalik.Models.Configurator
{
    public class DataLine3
    {
        [XmlElement("BrandCode")]
        public string BrandCode { get; set; }
        [XmlElement("TypeCode")]
        public string TypeCode { get; set; }
        [XmlElement("ModelCode")]
        public string ModelCode { get; set; }
        [XmlElement("ModelText")]
        public string ModelText { get; set; }
        [XmlElement("ModelYear")]
        public string ModelYear { get; set; }
        [XmlElement("Power")]
        public string Power { get; set; }
        [XmlElement("Displacement")]
        public string Displacement { get; set; }
        [XmlElement("BoltPattern")]
        public string BoltPattern { get; set; }
        [XmlElement("BoltCircle")]
        public string BoltCircle { get; set; }
        [XmlElement("CenterBore")]
        public string CenterBore { get; set; }
        [XmlElement("ABELine")]
        public ABELine ABELine { get; set; }
    }
}