using System.Collections.Generic;
using System.Xml.Serialization;

namespace PneuMalik.Models.Configurator
{
    [XmlRoot("ACMModelData_A2")]
    public class ACMModelData_A2
    {
        [XmlElement("ErrorHead")]
        public ErrorHead errorHead { get; set; }

        [XmlElement("BuyerParty")]
        public BuyerParty buyerParty { get; set; }

        [XmlElement("DataLine")]
        public List<DataLine3> DataLines { get; set; }
    }
}