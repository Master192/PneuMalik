using System.Xml.Serialization;

namespace PneuMalik.Models.Configurator
{
    public class BuyerParty
    {
        [XmlElement("PartyID")]
        public string PartyID { get; set; }
        [XmlElement("AgencyCode")]
        public string AgencyCode { get; set; }
    }
}