using PneuMalik.Models;
using PneuMalik.Models.Dto;
using System.Linq;
using System.Web.Http;

namespace PneuMalik.Controllers.Api
{
    public class FilterController : ApiController
    {


        [HttpGet]
        [Route("reload")]
        public IHttpActionResult Reload(int cathegory, int season, int manufacturer, int width, int rim,
            int profile, int si, int li)
        {

            var availableProducts = db.Products.Where(p => p.Active && p.VehicleType.Id == cathegory 
                        && (season == 0 || p.Tyre.Sezona == season)
                        && (manufacturer == 0 || p.Manufacturer.Id == manufacturer)
                        && (width == 0 || p.Tyre.Sirka.Id == width)
                        && (profile == 0 || p.Tyre.Profil.Id == profile)
                        && (rim == 0 || p.Tyre.Rafek.Id == rim)
                        && (si == 0 || p.Tyre.Si.Id == si)
                        && (li == 0 || p.Tyre.Li.Id == li));

            var filter = new Filter()
            {
                Manufacturers = availableProducts.GroupBy(m => m.Manufacturer.Id).Select(s => s.FirstOrDefault().Manufacturer.Id).ToList(),
                Seasons = availableProducts.GroupBy(s => s.Tyre.Sezona).Select(s => s.FirstOrDefault().Tyre.Sezona).ToList(),
                Profiles = availableProducts.GroupBy(p => p.Tyre.Profil.Id).Select(s => s.FirstOrDefault().Tyre.Profil.Id).ToList(),
                Rims = availableProducts.GroupBy(r => r.Tyre.Rafek.Id).Select(s => s.FirstOrDefault().Tyre.Rafek.Id).ToList(),
                Widths = availableProducts.GroupBy(w => w.Tyre.Sirka.Id).Select(s => s.FirstOrDefault().Tyre.Sirka.Id).ToList(),
                Sis = availableProducts.GroupBy(w => w.Tyre.Si.Id).Select(s => s.FirstOrDefault().Tyre.Si.Id).ToList(),
                Lis = availableProducts.GroupBy(w => w.Tyre.Li.Id).Select(s => s.FirstOrDefault().Tyre.Li.Id).ToList()
            };

            return Json(filter);
        }

        [HttpGet]
        [Route("steel")]
        public IHttpActionResult Steel(int rim, int brand, int model)
        {

            var availableProducts = db.Products.Where(p => p.Active && p.VehicleType.Id == SteelDiscVehicleTypeId
                        && (rim == 0 || p.PbDisc.Rafek.Id == rim)
                        && (brand == 0 || p.PbDisc.Znacka.Id == brand)
                        && (model == 0 || p.PbDisc.Model.Id == model));

            var filter = new Filter()
            {
                Rims = availableProducts.GroupBy(r => r.PbDisc.Rafek.Id).Select(s => s.FirstOrDefault().PbDisc.Rafek.Id).ToList(),
                Brands = availableProducts.GroupBy(w => w.PbDisc.Znacka.Id).Select(s => s.FirstOrDefault().PbDisc.Znacka.Id).ToList(),
                Models = availableProducts.GroupBy(w => w.PbDisc.Model.Id).Select(s => s.FirstOrDefault().PbDisc.Model.Id).ToList()
            };

            return Json(filter);
        }

        [HttpGet]
        [Route("alu")]
        public IHttpActionResult Alu(int manufacturer, int width, int rim)
        {

            var availableProducts = db.Products.Where(p => p.Active && p.VehicleType.Id == AluDiscVehicleTypeId
                        && (rim == 0 || p.AluDisc.Rafek.Id == rim)
                        && (manufacturer == 0 || p.Manufacturer.Id == manufacturer)
                        && (width == 0 || p.AluDisc.Sirka.Id == width));

            var filter = new Filter()
            {
                Rims = availableProducts.GroupBy(r => r.AluDisc.Rafek.Id).Select(s => s.FirstOrDefault().AluDisc.Rafek.Id).ToList(),
                Widths = availableProducts.GroupBy(w => w.AluDisc.Sirka.Id).Select(s => s.FirstOrDefault().AluDisc.Sirka.Id).ToList(),
                Manufacturers = availableProducts.GroupBy(m => m.Manufacturer.Id).Select(s => s.FirstOrDefault().Manufacturer.Id).ToList()
            };

            return Json(filter);
        }

        private const int SteelDiscVehicleTypeId = 9;
        private const int AluDiscVehicleTypeId = 10;
        private ApplicationDbContext db = new ApplicationDbContext();
    }
}
