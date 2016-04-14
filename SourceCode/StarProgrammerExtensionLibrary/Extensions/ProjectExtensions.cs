using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using WebGrease.Css.Extensions;

namespace StarProgrammerExtensionLibrary.Extensions
{
    public class ProjectExtensions
    {
        public static string GetProjectName()
        {
            var declaringType = MethodBase.GetCurrentMethod().DeclaringType;

            return declaringType != null ? declaringType.Assembly.GetName().Name : "";
        }

        public static List<KeyValuePair<string, string>> GetClassesAndMethodsInProjectAsKeyValuePairs(
            string namespaceOfClasses,
            List<string> classesToIgnore = null,
            string controllerName = null)
        {
            if (null == classesToIgnore)
                classesToIgnore = new List<string>();

            //Get all methods in assembly
            var methodInfos = Assembly.GetExecutingAssembly().GetTypes()
                //Filter by namespacesOfClasses and classesToIgnore.
                .Where(type => namespaceOfClasses.Equals(type.Namespace)
                               && !classesToIgnore.Contains(type.FullName.Split('.').Last()))
                .SelectMany(type => type.GetMethods())
                .Where(
                    method =>
                        method.IsPublic &&
                        !method.IsDefined(typeof (NonActionAttribute))
                        && null != method.DeclaringType
                            //Filter out system methods.
                            // ReSharper disable once PossibleNullReferenceException
                        && !method.DeclaringType.Namespace.Contains("System")
                        && !method.DeclaringType.Name.Contains("<>c__DisplayClass")
                        && !classesToIgnore.Contains(method.DeclaringType.Name));

            //Filter methods if input parameter isn't null.
            if (null != controllerName)
                methodInfos =
                    methodInfos.Where(
                        methodInfo =>
                            null != methodInfo.DeclaringType &&
                            methodInfo.DeclaringType.Name.ToLower().StartsWith(controllerName.ToLower()));

            //Return a list of KeyValuePairs with key = controller and value = method.
            return methodInfos.Where(methodInfo => methodInfo.DeclaringType != null)
                .Select(
                    methodInfo => new KeyValuePair<string, string>(methodInfo.DeclaringType.Name, methodInfo.Name))
                .OrderBy(methodInfo => methodInfo.Key)
                .ToList();
        }
    }
}