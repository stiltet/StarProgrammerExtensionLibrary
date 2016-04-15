# What is this?
Star Programmer Extension Library is a C# extension library for simplyfy your daily work. It's lightweight, easy and completely open source and free to use, both  for comercial and personal purposes.

# How do I use it?
1. First of, clone the repo to your computer or download it as a archive.
1. Open the solution located under "SourceCode" in Visual Studio.
1. Run the application and you will see a front page with a list of all availible extension methods.
1. All exstension methods is located in the "Extensions"-folder and all controllers are located in the "Controllers"-folder, just as you would expect.
1. Happy coding and testing. :)

# Code example and usage
### Extension Method:
A two methods used together picked straight from the ObjectExtension-class included in the extension library.
 
```csharp
public static bool AreTwoObjectsEqual<T>(
    T newObject,
    T oldObject,
    List<string> ignoreList = null,
    List<string> ignoreNullValuesOfTheseProperties = null)
{
    var newObjectProperties = newObject.GetType().GetProperties();
    newObjectProperties = newObjectProperties.Where(x =>
        !x.Name.Equals("Id") &&
        !x.Name.Contains("Updated") &&
        !x.PropertyType.FullName.StartsWith(ProjectExtensions.GetProjectName()) &&
        !x.PropertyType.FullName.StartsWith("System.Collections.Generic.ICollection") &&
        !x.PropertyType.FullName.StartsWith("System.Collections.Generic.List") &&
        null != x.SetMethod).ToArray();

    if (null != ignoreList)
        newObjectProperties = ignoreList.Aggregate(newObjectProperties,
            (current, str) => current.Where(x => !x.Name.Contains(str)).ToArray());

    var newObjectDictionary = newObjectProperties.ToDictionary(pName => pName.Name,
        pValue => pValue.GetValue(newObject, null));

    var oldObjectProperties = oldObject.GetType().GetProperties();
    var oldObjectDictionary = oldObjectProperties.ToDictionary(pName => pName.Name,
        pValue => pValue.GetValue(oldObject, null));

    return
        newObjectDictionary.Keys.Where(
            item =>
                (null != newObjectDictionary[item] && null != oldObjectDictionary[item]) 
                || null == ignoreNullValuesOfTheseProperties 
                || !ignoreNullValuesOfTheseProperties.Contains(item))
            .All(item => !AreTwoObjectPropertiesDifferent(newObjectDictionary[item], oldObjectDictionary[item]));
}

public static bool AreTwoObjectPropertiesDifferent(
    object newItem, 
    object oldItem)
{
    return null == newItem && null != oldItem ||
            null != newItem && !newItem.Equals(oldItem);
}
```

### Usage of AreTwoObjectsEqual-method:
```csharp
var users = new List<User>
{
    new User
    {
        Id = new Guid("57F4BC65-D219-4491-88EE-3367436147AA"),
        Email = "test@test.com",
        Username = "Test"
    },
    new User
    {
        Id = new Guid("57F4BC65-D219-4491-88EE-3367436147BB"),
        Email = "test@test.com",
        Username = "Test2"
    },
    new User
    {
        Id = new Guid("99F69AE0-2181-40EF-82A9-A8F5CC8E7144"),
        Email = "test@test.com",
        Username = "Test"
    },
    new User
    {
        Id = new Guid("99F69AE0-2181-40EF-82A9-A8F5CC8E7144"),
        Email = null,
        Username = "Test"
    }
};

var ignoreList = new List<string> {"Username"};
var ignoreNullValuesList = new List<string> {"Email"};

//Will return true.
var areTwoObjectsEqual = ObjectExtensions.AreTwoObjectsEqual(users[0], users[2]);

//Will return false.
var areTwoDifferentObjectsEqual = ObjectExtensions.AreTwoObjectsEqual(users[0], users[1]);

//Will return true.
var areTwoObjectsEqualWhenUsingIgnoreList = 
    ObjectExtensions.AreTwoObjectsEqual(users[0], users[1], ignoreList);

//Will return false.
var areTwoObjectsEqualWhenNotUsingIgnoreNullValuesList = ObjectExtensions.AreTwoObjectsEqual(users[0], users[3]);

//Will return true.
var areTwoObjectsEqualWhenUsingIgnoreNullValuesList = 
    ObjectExtensions.AreTwoObjectsEqual(users[0], users[3], null, ignoreNullValuesList);
```

# FAQ
#### Do you have a demo or preview page with all extention methods?
A demo page a work in progress and will be availible soon.

#### Can I download Star Programmer Extension Library on any package managers?
No. Not for now. But I will be creating a Nuget package for the project as soon as possible.
