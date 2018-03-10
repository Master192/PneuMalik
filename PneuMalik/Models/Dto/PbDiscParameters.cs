using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PneuMalik.Models.Dto
{
    public class ProductsPbDisc
    {

        public int Id { get; set; }

        [Column("Rafek_Id")]
        public int? RafekId { get; set; }
        public ProductParamRafek Rafek { get; set; }
        [Column("Znacka_Id")]
        public int? ZnackaId { get; set; }
        public ProductParamZnacka Znacka { get; set; }
        [Column("Model_Id")]
        public int? ModelId { get; set; }
        public ProductParamModel Model { get; set; }

        public string Dezen { get; set; }
        public string Sirka { get; set; }
        public string Rozmer { get; set; }
        public int Et { get; set; }
        public int PocetDer { get; set; }
        public int Rok { get; set; }
        public int Roztec { get; set; }
        public int StredovaDira { get; set; }
    }

    public class ProductParamZnacka
    {

        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ProductParamModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
    }
}