using System.Web.Mvc;

namespace EduIdentityDemo.Controllers
{
    [Authorize]
    //[Authorize(Roles = "Editor")]
    public class HomeController : Controller
    {
        [AllowAnonymous] //"disable authorization"
        public ActionResult Index()
        {
            ViewBag.Title = "Index (Public)";

            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Admin()
        {
            ViewBag.Title = "Admin";
            return View("Index");
        }

        [Authorize(Roles = "Editor")]
        public ActionResult Editor()
        {
            ViewBag.Title = "Editor";
            return View("Index");
        }

        [Authorize(Roles = "Editor, Admin")]
        public ActionResult EditorOrAdmin()
        {
            ViewBag.Title = "Editor or admin";
            return View("Index");
        }

        [Authorize(Roles = "Editor")]
        //[AllowAnonymous] - ej bra, stänger av autentiseringen helt.
        [Authorize(Roles = "Admin")]
        public ActionResult EditorAndAdmin()
        {
            ViewBag.Title = "Editor and admin";
            return View("Index");
        }

        //Långsökt användning...
        [Authorize(Users = "user@lexicon.se", Roles = "Admin")] //Man ska uppfylla båda!
        public ActionResult UserIdentity()
        {
            ViewBag.Title = "User Identity";
            return View("Index");
        }

    }
}