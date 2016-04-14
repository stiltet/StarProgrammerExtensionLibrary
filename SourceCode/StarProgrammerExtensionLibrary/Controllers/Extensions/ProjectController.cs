using System.Web.Mvc;
using StarProgrammerExtensionLibrary.Extensions;

namespace StarProgrammerExtensionLibrary.Controllers.Extensions
{
    public class ProjectController : ExtensionController
    {
        public ActionResult GetProjectName()
        {
            return View(model: ProjectExtensions.GetProjectName());
        }

        public ActionResult GetClassesAndMethodsInProjectAsKeyValuePairs()
        {
            return
                View(
                    ProjectExtensions.GetClassesAndMethodsInProjectAsKeyValuePairs(
                        "StarProgrammerExtensionLibrary.Controllers.Extensions"));
        }
    }
}