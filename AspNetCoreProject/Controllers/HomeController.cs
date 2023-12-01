using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreProject.Controllers
{
    public class HomeController : Controller
    {
        [Route("/home")]
        [Route("/")]

        public string Index()
        {
            return "hello from Index";
        }

        [Route("/about")]

        public string About()
        {
            return "hello from About";
        }

        [Route("/contact-us/{mobile:regex(^\\d{{10}}$)}")]
        public string Contact()
        {
            return "hello from Contact";
        }
    }
}
