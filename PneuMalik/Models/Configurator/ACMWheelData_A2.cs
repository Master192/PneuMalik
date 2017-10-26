using System.Collections.Generic;
using System.Xml.Serialization;

namespace PneuMalik.Models.Configurator
{
    [XmlRoot("ACMWheelData_A2")]
    public class ACMWheelData_A2
    {
        [XmlElement("ErrorHead")]
        public ErrorHead errorHead { get; set; }

        [XmlElement("BuyerParty")]
        public BuyerParty buyerParty { get; set; }

        [XmlElement("DataLine")]
        public List<DataLine4> DataLines { get; set; }
    }
}