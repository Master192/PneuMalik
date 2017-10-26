using System.Collections.Generic;
using System.Xml.Serialization;

namespace PneuMalik.Models.Configurator
{
    public class DataLine4
    {
        [XmlElement("Article")]
        public string Article { get; set; }
        [XmlElement("ABENr")]
        public string ABENr { get; set; }
        [XmlElement("ABENG")]
        public string ABENG { get; set; }
        [XmlElement("Kitset")]
        public string Kitset { get; set; }
        [XmlElement("KitsetAlt")]
        public string KitsetAlt { get; set; }
        [XmlElement("Axle")]
        public string Axle { get; set; }
        [XmlElement("isECE")]
        public string isECE { get; set; }
        [XmlElement("CertType")]
        public string CertType { get; set; }
        [XmlElement("Certificate")]
        public string Certificate { get; set; }
        [XmlElement("TypeApproval")]
        public string TypeApproval { get; set; }
        [XmlElement("MountingTorque")]
        public string MountingTorque { get; set; }
        [XmlElement("ImageOnCar")]
        public string ImageOnCar { get; set; }
        [XmlElement("ImageOnCarBinary")]
        public string ImageOnCarBinary { get; set; }
        [XmlElement("TyreLine")]
        public List<TyreLine> TyreLines { get; set; }
    }
}