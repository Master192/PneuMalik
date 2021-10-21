using PneuMalik.Helpers;
using PneuMalik.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PneuMalik.Models
{
    public class EshopViewModel
    {

        public EshopViewModel(ApplicationDbContext db)
        {
            var customer = new CustomerHelper();

            Cart = db.CartRows.Where(c => c.CustomerId == customer.Id).ToList();

            Cathegories = db.Cathegories.ToList();
        }

        public EshopViewModel(ApplicationDbContext db, int cathegory) 
            : this(db, cathegory, db.Products.Where(p => p.Action && p.Active && p.VehicleType.Id == cathegory).OrderBy(p => p.Price).ToList()) {
        }

        public EshopViewModel(ApplicationDbContext db, int cathegory, List<Product> products)
        {
            Cathegories = db.Cathegories.ToList();

            var co = products.ToList();

            var customer = new CustomerHelper();

            if (cathegory == 13)        // speciální kategorie - akční nabídka
            {

                products = db.Products.Where(p => p.Action && p.Active).OrderBy(p => p.Price).ToList();
            }

            foreach (var product in products)
            {

                product.Prices = db.Prices.Where(p => p.ProductId == product.Id).ToList();
            }

            foreach (var product in products)
            {

                if (product.TyreId.HasValue)
                {
                    product.Tyre = db.ProductsTyres.FirstOrDefault(t => t.Id == product.TyreId.Value);
                    if (product.Tyre.SirkaId.HasValue)
                    {
                        product.Tyre.Sirka = db.ParamSirka.FirstOrDefault(s => s.Id == product.Tyre.SirkaId);
                    }
                    if (product.Tyre.ProfilId.HasValue)
                    {
                        product.Tyre.Profil = db.ParamProfil.FirstOrDefault(s => s.Id == product.Tyre.ProfilId);
                    }
                    if (product.Tyre.RafekId.HasValue)
                    {
                        product.Tyre.Rafek = db.ParamRafek.FirstOrDefault(s => s.Id == product.Tyre.RafekId);
                    }
                    if (product.Tyre.SiId.HasValue)
                    {
                        product.Tyre.Si = db.ParamSi.FirstOrDefault(s => s.Id == product.Tyre.SiId);
                    }
                    if (product.Tyre.LiId.HasValue)
                    {
                        product.Tyre.Li = db.ParamLi.FirstOrDefault(s => s.Id == product.Tyre.LiId);
                    }
                }
                if (product.AluDiscId.HasValue)
                {
                    product.AluDisc = db.ProductsAluDisc.FirstOrDefault(t => t.Id == product.AluDiscId.Value);
                    if (product.AluDisc.RafekId.HasValue)
                    {
                        product.AluDisc.Rafek = db.ParamRafek.FirstOrDefault(s => s.Id == product.AluDisc.RafekId);
                    }
                    if (product.AluDisc.SirkaId.HasValue)
                    {
                        product.AluDisc.Sirka = db.ParamSirka.FirstOrDefault(s => s.Id == product.AluDisc.SirkaId);
                    }
                }
                if (product.PbDiscId.HasValue)
                {
                    product.PbDisc = db.ProductsPbDisc.FirstOrDefault(t => t.Id == product.PbDiscId.Value);
                    if (product.PbDisc.RafekId.HasValue)
                    {
                        product.PbDisc.Rafek = db.ParamRafek.FirstOrDefault(s => s.Id == product.PbDisc.RafekId);
                    }
                    if (product.PbDisc.ZnackaId.HasValue)
                    {
                        product.PbDisc.Znacka = db.ParamZnacka.FirstOrDefault(s => s.Id == product.PbDisc.ZnackaId);
                    }
                }
            }

            if (cathegory < 9)
            {

                SirkaList = db.ParamSirka.ToList();
                ProfilList = db.ParamProfil.ToList();
                RafekList = db.ParamRafek.ToList();
                LiList = db.ParamLi.ToList();
                SiList = db.ParamSi.ToList();
            }

            if (cathegory == 9)
            {

                RafekList = db.ParamRafek.ToList();
                ZnackaList = db.ParamZnacka.ToList();
                ModelList = db.ParamModel.ToList();
            }

            if (cathegory == 10)
            {

                RafekList = db.ParamRafek.ToList();
                SirkaList = db.ParamSirka.ToList();
            }

            Products = products;

            var tips = db.Products.Where(p => p.Tip && p.Active).ToList();
            foreach (var tip in tips)
            {

                if (tip.TyreId.HasValue)
                {
                    tip.Tyre = db.ProductsTyres.FirstOrDefault(t => t.Id == tip.TyreId.Value);
                    if (tip.Tyre.SirkaId.HasValue)
                    {
                        tip.Tyre.Sirka = db.ParamSirka.FirstOrDefault(s => s.Id == tip.Tyre.SirkaId);
                    }
                    if (tip.Tyre.ProfilId.HasValue)
                    {
                        tip.Tyre.Profil = db.ParamProfil.FirstOrDefault(s => s.Id == tip.Tyre.ProfilId);
                    }
                    if (tip.Tyre.RafekId.HasValue)
                    {
                        tip.Tyre.Rafek = db.ParamRafek.FirstOrDefault(s => s.Id == tip.Tyre.RafekId);
                    }
                    if (tip.Tyre.SiId.HasValue)
                    {
                        tip.Tyre.Si = db.ParamSi.FirstOrDefault(s => s.Id == tip.Tyre.SiId);
                    }
                    if (tip.Tyre.LiId.HasValue)
                    {
                        tip.Tyre.Li = db.ParamLi.FirstOrDefault(s => s.Id == tip.Tyre.LiId);
                    }
                }
                if (tip.AluDiscId.HasValue)
                {
                    tip.AluDisc = db.ProductsAluDisc.FirstOrDefault(t => t.Id == tip.AluDiscId.Value);
                    if (tip.AluDisc.RafekId.HasValue)
                    {
                        tip.AluDisc.Rafek = db.ParamRafek.FirstOrDefault(s => s.Id == tip.AluDisc.RafekId);
                    }
                    if (tip.AluDisc.SirkaId.HasValue)
                    {
                        tip.AluDisc.Sirka = db.ParamSirka.FirstOrDefault(s => s.Id == tip.AluDisc.SirkaId);
                    }
                }
                if (tip.PbDiscId.HasValue)
                {
                    tip.PbDisc = db.ProductsPbDisc.FirstOrDefault(t => t.Id == tip.PbDiscId.Value);
                    if (tip.PbDisc.RafekId.HasValue)
                    {
                        tip.PbDisc.Rafek = db.ParamRafek.FirstOrDefault(s => s.Id == tip.PbDisc.RafekId);
                    }
                    if (tip.PbDisc.ZnackaId.HasValue)
                    {
                        tip.PbDisc.Znacka = db.ParamZnacka.FirstOrDefault(s => s.Id == tip.PbDisc.ZnackaId);
                    }
                }
            }

            Tips = tips;
            Manufacturers = db.Manufacturers.ToList();
            VehicleTypes = db.VehicleTypes.ToList();
            Seasons = Enum.GetValues(typeof(Season)).Cast<int>().ToList();
            CathegoryType = cathegory;
            Filter = new FilterHelper(db, cathegory).Filter;
            Cart = db.CartRows.Where(c => c.CustomerId == customer.Id).ToList();
        }

        public int CathegoryType { get; set; }
        public IList<Product> Products { get; set; }
        public IList<Product> Tips { get; set; }
        public IList<Manufacturer> Manufacturers { get; set; }
        public IList<ProductParamModel> ModelList { get; set; }
        public IList<ProductParamProfil> ProfilList { get; set; }
        public IList<ProductParamRafek> RafekList { get; set; }
        public IList<ProductParamSirka> SirkaList { get; set; }
        public IList<ProductParamZnacka> ZnackaList { get; set; }
        public IList<ProductParamSi> SiList { get; set; }
        public IList<ProductParamLi> LiList { get; set; }
        public IList<VehicleType> VehicleTypes { get; set; }
        public IList<int> Seasons { get; set; }
        public Filter Filter { get; set; }
        public IList<CartRow> Cart { get; set; }
        public IList<Product> CartProducts { get; set; }
        public double Shipping { get; set; }
        public Product ProductDetail { get; set; }
        public Cathegory Cathegory { get; set; }
        public Customer Customer { get; set; }
        public IList<Cathegory> Cathegories { get; set; }
    }
}