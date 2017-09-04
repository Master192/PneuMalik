using PneuMalik.Services;
using System.Web.Http;

namespace PneuMalik.Controllers.Api
{

    public class ImportController : ApiController
    {

        public ImportController()
        {

            _b2bService = new PneuB2bService(false);
        }

        [HttpGet]
        [Route("status")]
        public IHttpActionResult Status()
        {

            return Json(_b2bService.Status());
        }

        [HttpGet]
        [Route("importstock")]
        public void ImportStock()
        {

            _b2bService.ImportStock();
        }

        [HttpGet]
        [Route("importall")]
        public void ImportAll()
        {

            _b2bService.ImportAll();
        }


        private readonly PneuB2bService _b2bService;
    }
}
