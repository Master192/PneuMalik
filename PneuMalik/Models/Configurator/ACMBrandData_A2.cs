using System.Collections.Generic;
using System.Xml.Serialization;

namespace PneuMalik.Models.Configurator
{
    [XmlRoot("ACMBrandData_A2")]
    public class ACMBrandData_A2
    {
        [XmlElement("DocumentID")]
        public string DocumentID { get; set; }

        [XmlElement("ErrorHead")]
        public ErrorHead errorHead { get; set; }

        [XmlElement("BuyerParty")]
        public BuyerParty buyerParty { get; set; }

        [XmlElement("DataLine")]
        public List<DataLine> DataLines { get; set; }

    }
}