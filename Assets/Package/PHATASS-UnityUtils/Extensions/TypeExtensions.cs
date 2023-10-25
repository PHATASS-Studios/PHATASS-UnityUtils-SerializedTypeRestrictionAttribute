using System.Collections.Generic; //List

using Type = System.Type;

namespace PHATASS.Utils.Extensions
{
// Extension methods for System.Type
	public static class TypeExtensions
	{
//public extension methods
	//returns true if type corresponds to a serializable UnityEngine.Object OR a serializable collection of such elements
		public static bool EIsSerializableUnityObject (this Type self)
		{
			return self
				.EGetSerializableCollectionElementType()
				.EIsOrInherits(typeof (UnityEngine.Object));
		}

	//returns true if type is an instance or subclass of other type
		public static bool EIsOrInherits (this Type self, Type other)
		{
			if (self.Equals(other) || self.IsSubclassOf(other)) { return true; }
			return false;
		}

	//returns true if type is one of the serializable collection types: either a native array T[] or a List<T>()
		public static bool EIsSerializableCollection (this Type self)
		{
			if (self.IsArray) { return true; }
			if (self.EIsGenericCollection()) { return true; }
			return false;
		}

	//returns true if type is List<any>
		public static bool EIsGenericCollection (this Type self)
		{
			if (!self.IsGenericType) 
			{ return false; }

			Type genericType = self.GetGenericTypeDefinition();
			if (genericType.Equals(typeof (List<>))) { return true; }
			
			return false;
		}

	//returns System.Type of the elements of a serializable list - will return null if not a serializable list
		public static Type EGetSerializableCollectionElementType (this Type self, bool returnSelfIfNotACollection = true)
		{
			if (self.IsArray) { return self.GetElementType(); }
			if (self.EIsGenericCollection()) { return self.GetGenericArguments()[0]; }
			return (returnSelfIfNotACollection) ? self : null;
		}
//ENDOF public extension methods
	}
}