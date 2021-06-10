using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Totvs.Sample.Shop.Web.Controllers
{
    [Produces("application/json")]
    [Route(WebConstants.ApplicationName)]
    public class ApplicationController : TnfController
    {
        public ApplicationController()
        {
        }

        [HttpGet("info")]
        public IActionResult Info()
        {
            return Ok(new
            {
                Name = "TOTVS Shop",
                TOTVSTNFVersion = typeof(TnfController).Assembly.GetName().Version.ToString(),
                TOTVSShopVersion = GetType().Assembly.GetName().Version.ToString()
            });
        }
    }
}
