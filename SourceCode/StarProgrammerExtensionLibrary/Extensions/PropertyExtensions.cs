using System.Reflection;

namespace StarProgrammerExtensionLibrary.Extensions
{
	public static class PropertyExtensions
	{
		public static void SetPropertyValue<TEntity>(this TEntity entity, string memberName, object value) where TEntity : class
		{
			GetProperty(entity, memberName).SetValue(entity, value, null);
		}

		public static PropertyInfo GetProperty<TEntity>(this TEntity entity, string propertyName) where TEntity : class
		{
			return entity.GetType().GetProperty(propertyName);
		}
	}
}