//UnityEditor imports
using CustomPropertyDrawer = UnityEditor.CustomPropertyDrawer;
using EditorGUI = UnityEditor.EditorGUI;
using EditorGUIUtility = UnityEditor.EditorGUIUtility;
using PropertyDrawer = UnityEditor.PropertyDrawer;
using SerializedProperty = UnityEditor.SerializedProperty;
using SerializedPropertyType = UnityEditor.SerializedPropertyType;

//UnityEngine imports
using Debug = UnityEngine.Debug;
using GameObject = UnityEngine.GameObject;
using Component = UnityEngine.Component;
using Rect = UnityEngine.Rect;
using GUIContent = UnityEngine.GUIContent;
using Color = UnityEngine.Color;

//PHATASS imports
using static PHATASS.Utils.Extensions.TypeExtensions;
using static PHATASS.Utils.Extensions.RectExtensions;

namespace PHATASS.Utils.Attributes
{
	//property delimiting that a serialized field is constrained to a specific sub-type
	//can only be used with reference types inheriting from UnityEngine.Object.
	//UnityEngine.Object is the recomended field type
	[CustomPropertyDrawer(typeof(SerializedTypeRestrictionAttribute))]
	public class PropertyDrawerSerializedTypeRestrictionAttribute : PropertyDrawer
	{
	//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
	//[TO-DO] export this somewhere safe
		//[TO-DO] move this method somewhere public and safe
		private TArray[] ArrayFill <TArray>(TArray fillValue, int times)
		{
			TArray[] array = new TArray[times];
			for (int i = 0; i < times; i++)	{ array[i] = fillValue;	}
			return array;
		}
	//ENDOF [TO-DO]

	//PropertyDrawer lifecycle
 		public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
 		{
 			//validate serialized property is a reference type
 			if (property.propertyType != SerializedPropertyType.ObjectReference)
 			{ throw new System.InvalidOperationException(string.Format("Attribute \"SerializedTypeRestrictionAttribute(Type)\" can ONLY be used with OBJECT REFERENCE field types. Field: {0} Wrong type: {1}", property.name, property.propertyType.ToString())); }
 			//validate serialized field is a serializable reference type
 			if (!this.fieldIsUnityObject)
 			{ throw new System.InvalidOperationException(string.Format("Attribute \"SerializedTypeRestrictionAttribute(Type)\" field type MUST inherit from UnityEngine.Object. Field: {0}", property.name)); }

 			//begin drawing property
 			label = EditorGUI.BeginProperty(position, label, property);

 			this.DoColorOverlay(position, property);

 			//calculate rows
 			Rect[,] rows = GetPropertyRows(position, property);

 			/*
			EditorGUI.DrawRect(rows[0,0], new UnityEngine.Color(r: 0f, g: 1f, b: 0f, a: 0.5f));
			EditorGUI.DrawRect(rows[1,0], new UnityEngine.Color(r: 1f, g: 0f, b: 0f, a: 0.5f));
			*/
			
			ERestrictedFieldState propertyStatus = this.GetPropertyStatus(property);

 			//draw labels and reserve field rects
 			Rect propertyFieldRect = EditorGUI.PrefixLabel(rows[0,0], label);
 			//Rect componentPickerRect = rows[1,0]; //= EditorGUI.PrefixLabel(rows[1,0], typeRequirementLabel);

 			//draw property field
 			bool propertyValueChanged = this.DoPropertyField(propertyFieldRect, property);

 			//draw component picker
 			if (propertyStatus == ERestrictedFieldState.ComponentPickerRequired)
 			{
				EditorGUI.indentLevel = 1;
	 			this.DoComponentPicker(rows[1,0], property);
	 			EditorGUI.indentLevel = 0;
	 		}

 			EditorGUI.EndProperty();
		}
		//OnGUI()-only private methods
			//calculate grid. create 3 rows if component picker required, 1 otherwise
			private Rect[,] GetPropertyRows (Rect rect, SerializedProperty property)
			{
				int rowCount = 1;
				if (this.GetPropertyStatus(property) == ERestrictedFieldState.ComponentPickerRequired)
				{ rowCount = 2; }

	 			return rect.EGridSplitRect(
	 				rows: ArrayFill<float>(-1f, rowCount),
	 				rowMargin: EditorGUIUtility.standardVerticalSpacing
	 			);
			}

			private void DoColorOverlay (Rect rect, SerializedProperty property)
			{
				EditorGUI.DrawRect(rect: rect, color: this.GetPropertyHighlightColor(property));
			}

			private bool DoPropertyField (Rect rect, SerializedProperty property)
			{
				EditorGUI.BeginChangeCheck();
				EditorGUI.PropertyField(
					position: rect,
					property: property,
					label: GUIContent.none
				);
				if (EditorGUI.EndChangeCheck())
				{
					property.serializedObject.ApplyModifiedProperties();
					return true;
				}
				else { return false; }
			}
			private bool DoComponentPicker (Rect rect, SerializedProperty property)
			{
				GameObject gameObject = property.objectReferenceValue as GameObject;

				if (gameObject == null)
				{
					Debug.LogError(string.Format("Property \"{0}\" SerializedTypeRestrictionAttribute: detected component picker required but object is not a GameObject.", property.displayName));
					return false;
				}

				//fetch component list, then prepend a "non-choice" element
				Component[] components = gameObject.GetComponents(this.typeRestriction);
				UnityEngine.Object[] objects = new UnityEngine.Object[components.Length + 1];
				objects[0] = (UnityEngine.Object) gameObject;
				System.Array.Copy(
					sourceArray: components,
					sourceIndex: 0,
					destinationArray: objects,
					destinationIndex: 1,
					length: components.Length
				);

				//compose strings array for the dropdown
				string[] displayStrings = this.ObjectsToTypeStrings(objects);
				displayStrings[0] = (components.Length > 0)
					? "Pick a component"
					: "No appropriate components available";

				//render the dropdown
				EditorGUI.BeginChangeCheck();
				int choice = EditorGUI.Popup(
					position: rect,
					label: this.typeRequirementLabel,
					selectedIndex: 0,
					displayedOptions: displayStrings
				);

				//on choice, update serialized value
				if (EditorGUI.EndChangeCheck())
				{
					property.objectReferenceValue = objects[choice];
					property.serializedObject.ApplyModifiedProperties();
					return true;
				}
				else { return false; }
			}
		//ENDOF OnGUI()-only private methods

		public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
		{
			//if component picker required allot 3 lines
			if (this.GetPropertyStatus(property) == ERestrictedFieldState.ComponentPickerRequired)
			{ return EditorGUIUtility.singleLineHeight * 2 + EditorGUIUtility.standardVerticalSpacing; }
			//else allot only 1 line
			return EditorGUIUtility.singleLineHeight;
		}
	//ENDOF PropertyDrawer lifecycle		

	//private properties
		//returns a string representing the type constraint label
		private string typeRequirementLabel
		{ get {
			return string.Format("[{0}]",
				TypeFullStringToTypeName(
						((this.attribute as SerializedTypeRestrictionAttribute)
						.typeRestriction.ToString())
				));
		}}

		//returns SERIALIZED type (NOT type required)
		private System.Type serializedType //fieldType
		{ get { return this.fieldInfo.FieldType.EGetSerializableCollectionElementType(returnSelfIfNotACollection: true); }}

		//returns type that is required to fulfill
		private System.Type typeRestriction
		{ get { return (this.attribute as SerializedTypeRestrictionAttribute).typeRestriction; }}

		//determines if the field is a serializable collection of items
		private bool fieldIsCollection
		{ get { return this.fieldInfo.FieldType.EIsSerializableCollection(); }}

		//determines if field linked to the attribute is of a valid type
		//it must either inher serializble
		private bool fieldIsUnityObject
		{ get { return this.serializedType.EIsOrInherits(typeof(UnityEngine.Object)); }}
	//ENDOF private properties

	//private enums
		//Enumerator describing the possible states of a field
		//used for communication between private methods
		private enum ERestrictedFieldState
		{
			Empty,
			ComponentPickerRequired,
			WrongType,
			Correct
		}
	//ENDOF private enum

	//private methods
		//returns an identifier of the state of a property according to its field value
		private ERestrictedFieldState GetPropertyStatus (SerializedProperty property)
		{
			//Empty
			if (property.objectReferenceValue == null)
			{ return ERestrictedFieldState.Empty; }

			System.Type propertyValueType = property.objectReferenceValue.GetType();

			//check wether property value fulfills type requirement
			if (this.typeRestriction.IsAssignableFrom(propertyValueType))
			{ return ERestrictedFieldState.Correct; }

			//check wether property object contains components
			if (typeof(GameObject).IsAssignableFrom(propertyValueType))
			{ return ERestrictedFieldState.ComponentPickerRequired; }

			//if field is not empty, nor correct, nor contains sub-components requiring a component picker, it is wrong
			return ERestrictedFieldState.WrongType;
		}

		//returns a list of strings representing the objects in an input array, for generating popup options lists
		private string[] ObjectsToTypeStrings (UnityEngine.Object[] objects)
		{
			int iLimit = objects.Length;
			string[] strings = new string[iLimit];
			for (int i = 0; i < iLimit; i++)
			{ strings[i] = ObjectToTypeString(objects[i]); }
			return strings;
		}

		//returns a string representing the simple name of the type of an object
		private string ObjectToTypeString (UnityEngine.Object obj)
		{
			return TypeFullStringToTypeName(obj.GetType().ToString());
		}

		//from a fully-namespaced type string representation
		//removes delimiting characters and namespace data, returns only type name
		private string TypeFullStringToTypeName (string fullString)
		{
			string[] substrings = fullString.Split('.');
			return substrings[substrings.Length - 1];
		}

		//returns an appropiate indication color for property
		//red if object is invalid, yellow if object is GameObject and requires picking component
		//green if object is valid, null if object is null
		private Color GetPropertyHighlightColor (SerializedProperty property)
		{
			const float alpha = 0.35f;

			switch (this.GetPropertyStatus(property))
			{
				//no color if field is empty
				case ERestrictedFieldState.Empty:
					return new Color(r: 0f, g: 0f, b: 0f, a: 0f);

				//yellow if component picker required
				case ERestrictedFieldState.ComponentPickerRequired:
					return new Color(r: 1f, g: 1f, b: 0f, a: alpha);

				//red if wrong object type
				case ERestrictedFieldState.WrongType:
					return new Color(r: 1f, g: 0f, b: 0f, a: alpha);

				//green if all ok
				case ERestrictedFieldState.Correct:
					return new Color(r: 0f, g: 1f, b: 0f, a: alpha);
			
				//default state returns blue to avoid compiler errors
				default:
					return new Color(r: 0f, g: 0f, b: 1f, a: (alpha/2));
			}

		}

	  /*//METHOD UNUSED
		//fetches a list of every component fitting TComponent that is a sibling of received unityObject
		//if unityObject is a GameObject or Component, gets every sibling or child component
		//if it is neither, returns a 1-length array containing received object, or empty array if unityObject is not a TComponent type
		private TComponent[] FindSiblingsOfType <TComponent> (UnityEngine.Object unityObject)
			where TComponent : UnityEngine.Object
		{
			if (unityObject == null) { return new TComponent[0]{}; }
			
			//attemp to cache object casted as a GameObject
			GameObject gameObject = unityObject as GameObject;

			//if object is not a GameObject, cast as a component to find containing GameObject
			if (gameObject == null)
			{ gameObject = (unityObject as UnityEngine.Component)?.gameObject; }

			//find contained components of desired type within gameObject
			if (gameObject != null)
			{ return (TComponent[]) gameObject.GetComponents<TComponent>();	}

			//if object was not a GameObject/Component, return an array containing it
			//returns empty array if unityObject is not a TComponent type
			if (unityObject is TComponent)
			{ return new TComponent[1] {(TComponent) unityObject}; }
			return new TComponent[0]{};
		}
	  //ENDOF METHOD UNUSED*/
	//ENDOF private methods



		/* original sample code
		//returns true if typeRestriction is a valid serializable reference type
		private bool TypeestrictionIsValid (System.Type typeRestriction) 
		{
			bool isArray = this.fieldInfo.FieldType.IsListType();
			System.Type fieldType = (isArray)
				? this.fieldInfo.FieldType.GetElementTypeOfListType()
				: this.fieldInfo.FieldType
			;

			if (!TypeUtil.IsType(fieldType, typeof(UnityEngine.Object)))
			{ return false; }


			var attrib = this.attribute as TypeRestrictionAttribute;
			return (
				attrib.InheritsFromType == null
				|| attrib.InheritsFromType.IsInterface
				|| TypeUtil.IsType(attrib.InheritsFromType, fieldType)
			);
		}
		*/
	}
}