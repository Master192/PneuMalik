using System.Collections.Generic;
using System.Xml.Serialization;

namespace PneuMalik.Models.Configurator
{
    [XmlRoot("ACMTypeData_A2")]
    public class ACMTypeData_A2
    {

        [XmlElement("ErrorHead")]
        public ErrorHead errorHead { get; set; }

        [XmlElement("BuyerParty")]
        public BuyerParty buyerParty { get; set; }

        [XmlElement("DataLine")]
        public List<DataLine2> DataLines { get; set; }
    }
}