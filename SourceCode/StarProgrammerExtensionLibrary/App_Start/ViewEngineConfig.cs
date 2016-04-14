using System.Web.Mvc;

namespace StarProgrammerExtensionLibrary
{
    public class StarProgrammerExtensionLibraryViewEngine : RazorViewEngine
    {
        public StarProgrammerExtensionLibraryViewEngine()
        {
            ViewLocationFormats = new[]
            {
                "~/Views/Extensions/{1}/{0}.cshtml",
                "~/Views/Extensions/{1}/{0}.vbhtml",
                "~/Views/{1}/{0}.cshtml",
                "~/Views/{1}/{0}.vbhtml",
                "~/Views/Shared/{0}.cshtml",
                "~/Views/Shared/{0}.vbhtml"
            };

            PartialViewLocationFormats = new[]
            {
                "~/Views/Extensions/{1}/{0}.cshtml",
                "~/Views/Extensions/{1}/{0}.vbhtml",
                "~/Views/{1}/{0}.cshtml",
                "~/Views/{1}/{0}.vbhtml",
                "~/Views/Shared/{0}.cshtml",
                "~/Views/Shared/{0}.vbhtml"
            };
        }
    }
}