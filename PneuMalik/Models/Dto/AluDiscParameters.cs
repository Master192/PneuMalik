using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PneuMalik.Models.Dto
{
    public class ProductsAluDisc
    {

        public int Id { get; set; }

        [Column("Sirka_Id")]
        public int? SirkaId { get; set; }
        public ProductParamSirka Sirka { get; set; }
        [Column("Rafek_Id")]
        public int? RafekId { get; set; }
        public ProductParamRafek Rafek { get; set; }

        public string Model { get; set; }
        public string Roztec { get; set; }
        public int Et { get; set; }
        public int PocetDer { get; set; }
        public int Rok { get; set; }
        public int StredovaDira { get; set; }
        public string Dezen { get; set; }
        public string Rozmer { get; set; }
    }
}