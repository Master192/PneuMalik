using System;
using System.Xml.Serialization;

namespace PneuMalik.Models.PneuB2b
{

    [Serializable]
    public class Response
    {

        [XmlElement("State")]
        public string State { get; set; }
        [XmlElement("Message")]
        public string Message { get; set; }
        [XmlArray("SteelRims")]
        [XmlArrayItem("SteelRim", typeof(SteelRim))]
        public SteelRim[] SteelRims { get; set; }
        [XmlArray("Patterns")]
        [XmlArrayItem("Pattern", typeof(Pattern))]
        public Pattern[] Patterns { get; set; }
        [XmlArray("Tyres")]
        [XmlArrayItem("Tyre", typeof(Tyre))]
        public Tyre[] Tyres { get; set; }

        [Serializable()]
        public class SteelRim
        {
            [XmlElement("ID")]
            public int Id { get; set; }
            [XmlElement("PartNo")]
            public string PartNo { get; set; }
            [XmlElement("ParentPartNo")]
            public string ParentPartNo { get; set; }
            [XmlElement("DisplayName")]
            public string DisplayName { get; set; }
            [XmlElement("ManufacturerID")]
            public int ManufacturerID { get; set; }
            [XmlElement("Manufacturer")]
            public string Manufacturer { get; set; }
            [XmlElement("Dimension")]
            public string Dimension { get; set; }
            [XmlElement("BoltPattern")]
            public string BoltPattern { get; set; }
            [XmlElement("BoltCircle_mm")]
            public string BoltCircle_mm { get; set; }
            [XmlElement("CentreBore_mm")]
            public string CentreBore_mm { get; set; }
            [XmlElement("Offset_mm")]
            public string Offset_mm { get; set; }
            [XmlElement("ABE")]
            public bool Abe { get; set; }
            [XmlElement("ImageUrl")]
            public string ImageUrl { get; set; }
            [XmlElement("StockPriceInfo")]
            public StockPriceInfo StockPriceInfo { get; set; }
        }

        [Serializable()]
        public class StockPriceInfo
        {

            [XmlElement("TotalPrice")]
            public double TotalPrice { get; set; }
            [XmlElement("TotalPriceCZK")]
            public double TotalPriceCZK { get; set; }
            [XmlElement("TotalPriceIncDelivery")]
            public double TotalPriceIncDelivery { get; set; }
            [XmlElement("TotalPriceIncDeliveryCZK")]
            public double TotalPriceIncDeliveryCZK { get; set; }
            [XmlElement("TotalPriceIncDeliveryComputed")]
            public double TotalPriceIncDeliveryComputed { get; set; }
            [XmlElement("Currency")]
            public string Currency { get; set; }
            [XmlElement("StockAmount")]
            public double StockAmount { get; set; }
            [XmlElement("DeliveryTime")]
            public int DeliveryTime { get; set; }
            [XmlElement("DeliveryTimeTerm")]
            public int DeliveryTimeTerm { get; set; }
            [XmlElement("SuppliersCountry")]
            public string SuppliersCountry { get; set; }
        }

        [Serializable()]
        public class Pattern
        {

            [XmlElement("ID")]
            public int Id { get; set; }
            [XmlElement("Name")]
            public string Name { get; set; }
            [XmlElement("Description_CZ")]
            public string Description_CZ { get; set; }
        }

        [Serializable()]
        public class Tyre
        {

            [XmlElement("ID")]
            public int Id { get; set; }
            [XmlElement("PartNo")]
            public string PartNo { get; set; }
            [XmlElement("EAN")]
            public string Ean { get; set; }
            [XmlElement("DisplayName")]
            public string DisplayName { get; set; }
            [XmlElement("ManufacturerID")]
            public int ManufacturerID { get; set; }
            [XmlElement("Manufacturer")]
            public string Manufacturer { get; set; }
            [XmlElement("PatternID")]
            public int PatternID { get; set; }
            [XmlElement("Pattern")]
            public string Pattern { get; set; }
            [XmlElement("Width")]
            public double Width { get; set; }
            [XmlElement("Separator")]
            public string Separator { get; set; }
            [XmlElement("Profile")]
            public double Profile { get; set; }
            [XmlElement("ConstructionType")]
            public string ConstructionType { get; set; }
            [XmlElement("Diameter")]
            public double Diameter { get; set; }
            [XmlElement("LoadIndexFrom")]
            public string LoadIndexFrom { get; set; }
            [XmlElement("SpeedIndex")]
            public string SpeedIndex { get; set; }
            [XmlElement("Usage")]
            public string Usage { get; set; }
            [XmlElement("VehicleType")]
            public string VehicleType { get; set; }
            [XmlElement("VehicleTypeCode")]
            public string VehicleTypeCode { get; set; }
            [XmlElement("CT")]
            public string Ct { get; set; }
            [XmlElement("ROF")]
            public bool Rof { get; set; }
            [XmlElement("ImageUrl")]
            public string ImageUrl { get; set; }
            [XmlElement("TagConsumption")]
            public string TagConsumption { get; set; }
            [XmlElement("TagAdhesion")]
            public string TagAdhesion { get; set; }
            [XmlElement("TagNoiseLevel")]
            public string TagNoiseLevel { get; set; }
            [XmlElement("TagNoiseLevel_dB")]
            public string TagNoiseLevel_dB { get; set; }
            [XmlElement("RetailPriceCurrency_CZ")]
            public string RetailPriceCurrency_CZ { get; set; }
            [XmlElement("StockPriceInfo")]
            public StockPriceInfo StockPriceInfo { get; set; }
            [XmlElement("StockPriceInfo_48")]
            public StockPriceInfo StockPriceInfo_48 { get; set; }
            [XmlElement("ProductCategoryID")]
            public int ProductCategoryID { get; set; }
            [XmlElement("ProductCategoryName")]
            public string ProductCategoryName { get; set; }
            [XmlElement("CatMarketSegmentationID")]
            public string CatMarketSegmentationID { get; set; }
            [XmlElement("MarketSegmentation")]
            public string MarketSegmentation { get; set; }
            [XmlElement("Weight")]
            public double Weight { get; set; }
        }
    }
}