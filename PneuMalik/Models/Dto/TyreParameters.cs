using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PneuMalik.Models.Dto
{
    public class ProductsTyre
    {

        public int Id { get; set; }

        public int Sezona { get; set; }

        [Column("Sirka_Id")]
        public int? SirkaId { get; set; }
        public ProductParamSirka Sirka { get; set; }
        [Column("Profil_Id")]
        public int? ProfilId { get; set; }
        public ProductParamProfil Profil { get; set; }
        [Column("Rafek_Id")]
        public int? RafekId { get; set; }
        public ProductParamRafek Rafek { get; set; }
        [Column("Si_Id")]
        public int? SiId { get; set; }
        public ProductParamSi Si { get; set; }
        [Column("Li_Id")]
        public int? LiId { get; set; }
        public ProductParamLi Li { get; set; }

        public string Dezen { get; set; }
        public string SerieVyska { get; set; }
        public string Konstrukce { get; set; }
        public string PrumerRafku { get; set; }
        public string RychlostniIndex { get; set; }
        public string HmotnostniIndex { get; set; }
        public string Ct { get; set; }
        public string Rof { get; set; }
        public string Spotreba { get; set; }
        public string Prilnavost { get; set; }
        public string UrovenHluku { get; set; }
        public string UrovenHlukudB { get; set; }
        public string Hmotnost { get; set; }
    }

    public class ProductParamSirka
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ProductParamProfil
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ProductParamRafek
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ProductParamSi
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ProductParamLi
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}