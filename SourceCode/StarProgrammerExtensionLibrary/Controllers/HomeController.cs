using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StarProgrammerExtensionLibrary.Extensions;
using StarProgrammerExtensionLibrary.ViewModels;

namespace StarProgrammerExtensionLibrary.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var controllersAndActions = ProjectExtensions.GetClassesAndMethodsInProjectAsKeyValuePairs(
                "StarProgrammerExtensionLibrary.Controllers.Extensions",
                new List<string>
                {
                    "ExtensionController"
                });

            var extensionClassesAndMethods =
                ProjectExtensions.GetClassesAndMethodsInProjectAsKeyValuePairs(
                    "StarProgrammerExtensionLibrary.Extensions");

            var missingOrFaultyControllerActions =
                extensionClassesAndMethods.Where(
                    extensionClassAndMethods =>
                        controllersAndActions.All(x => x.Value != extensionClassAndMethods.Value))
                    .Select(
                        extensionClassAndMethods =>
                            new KeyValuePair<string, string>(
                                extensionClassAndMethods.Key,
                                extensionClassAndMethods.Value))
                    .OrderBy(x => x.Key).ThenBy(x => x.Value)
                    .ToList();

            return
                View(new IndexViewModel
                {
                    ClassesAndMethodsInProjectAsKeyValuePairs = controllersAndActions,
                    MissingOrFaultyControllerActions = missingOrFaultyControllerActions
                });
        }

        public ActionResult About()
        {
            return View();
        }
    }
}