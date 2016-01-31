using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
	public static class Extensions
	{
		public static T GetAttribute<T>(this Enum value)
		{
			return value.GetAttributes<T>().FirstOrDefault();
		}

		public static IEnumerable<T> GetAttributes<T>(this Enum value)
		{
			var type = value.GetType();
			var memInfo = type.GetMember(value.ToString());
			return memInfo[0].GetCustomAttributes(typeof(T), false).Cast<T>();
		}
	}
}
