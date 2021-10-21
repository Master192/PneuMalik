using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace PneuMalik.Services
{
    public class ConfiguratorService
    {

        public ConfiguratorService()
        {

            _xmlTags = new NameValueCollection();
        }

        public T GetItemsFromXml<T>(string tag)
        {

            var data = ProcessRequest(tag);

            T xmlData = default(T);

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            var settings = new XmlReaderSettings { DtdProcessing = DtdProcessing.Ignore };

            using (var stream = new MemoryStream(data))
            {
                using (XmlReader reader = XmlReader.Create(stream, settings))
                {
                    xmlData = (T)serializer.Deserialize(reader);
                    reader.Close();
                }
            }

            return xmlData;
        }

        public void XmlTagsClear()
        {
            _xmlTags.Clear();
        }

        public void XmlTagAdd(string xmlTagName, string xmlTagValue)
        {
            _xmlTags.Add(xmlTagName, xmlTagValue);
        }

        private byte[] ProcessRequest(string xmlName)
        {

            var arrCredentials = _userCredentials.Split(':');

            var postData = GenerateSimpleXmlRequest(xmlName);
            var postDataBytes = Encoding.UTF8.GetBytes(postData);

            var webClient = new WebClient();
            webClient.UseDefaultCredentials = true;
            webClient.Credentials = new NetworkCredential(arrCredentials[0], arrCredentials[1]);
            var response = webClient.UploadData(new Uri(_targetUrl), "POST", postDataBytes);

            return response;
        }

        private string GenerateSimpleXmlRequest(string doctype)
        {
            string strOut = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>"
                + "<" + doctype + ">"
                + "<BuyerParty>"
                + "<PartyID>" + _buyerPartyId + "</PartyID>"
                + "<AgencyCode>" + _buyerAgencyCode + "</AgencyCode>"
                + "</BuyerParty>";

            for (int i = 0; i < _xmlTags.Keys.Count; i++)
            {
                strOut += "  <" + _xmlTags.Keys[i] + ">" + _xmlTags[_xmlTags.Keys[i]] + "</" + _xmlTags.Keys[i] + ">";
            }

            strOut += "</" + doctype + ">";

            return strOut;
        }

        private const string _documentId = "a2-test";
        private const string _buyerPartyId = "32TEST";      //"3215729";
        private const string _buyerAgencyCode = "91";
        private const string _userCredentials = "Divi 320 Testkunde:12345alcar_cz";
        private const string _targetUrl = "https://adhocedi.alcar-wheels.com";

        private NameValueCollection _xmlTags;
    }
}