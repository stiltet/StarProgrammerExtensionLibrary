using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using StarProgrammerExtensionLibrary.Extensions;

namespace StarProgrammerExtensionLibrary.Controllers
{
    public class ExtensionController : Controller
    {
        public ActionResult Index()
        {
            return
                View("ExtensionPageIndex",
                    ProjectExtensions.GetClassesAndMethodsInProjectAsKeyValuePairs(
                        "StarProgrammerExtensionLibrary.Controllers.Extensions",
                        new List<string>
                        {
                            "ExtensionController"
                        },
                        Path.GetExtension(ToString()).Replace(".", "")));
        }
    }
}