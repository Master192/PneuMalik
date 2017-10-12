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
        public IHttpActionResult Reload(int cathegory, int season, int manufacturer, int width, int rim, int profile)
        {

            var availableProducts = db.Products.Where(p => p.Active && p.VehicleType.Id == cathegory 
                        && (season == 0 || p.Season.Id == season)
                        && (manufacturer == 0 || p.Manufacturer.Id == manufacturer)
                        && (width == 0 || p.Width == width)
                        && (profile == 0 || p.HighPr == profile)
                        && (rim == 0 || p.Diameter == rim));

            var filter = new Filter()
            {
                Manufacturers = availableProducts.GroupBy(m => m.Manufacturer.Id).Select(s => s.FirstOrDefault().Manufacturer.Id).ToList(),
                Seasons = availableProducts.GroupBy(s => s.Season.Id).Select(s => s.FirstOrDefault().Season.Id).ToList(),
                Profiles = availableProducts.GroupBy(p => p.HighPr).Select(s => s.FirstOrDefault().HighPr).ToList(),
                Rims = availableProducts.GroupBy(r => r.Diameter).Select(s => s.FirstOrDefault().Diameter).ToList(),
                Widths = availableProducts.GroupBy(w => w.Width).Select(s => s.FirstOrDefault().Width).ToList()
            };

            return Json(filter);
        }

        private ApplicationDbContext db = new ApplicationDbContext();
    }
}
