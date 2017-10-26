using System.Xml.Serialization;

namespace PneuMalik.Models.Configurator
{
    public class ErrorHead
    {

        [XmlElement("ErrorCode")]
        public string ErrorCode { get; set; }
    }
}