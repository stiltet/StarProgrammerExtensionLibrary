using System.Collections.Generic;

namespace StarProgrammerExtensionLibrary.ViewModels
{
    public class IndexViewModel
    {
        public List<KeyValuePair<string, string>> ClassesAndMethodsInProjectAsKeyValuePairs { get; set; }
        public List<KeyValuePair<string, string>> MissingOrFaultyControllerActions { get; set; }
    }
}