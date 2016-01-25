// http://www.codeproject.com/Articles/14518/Bit-Flags-Type-Converter
#pragma warning disable 1573

using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

namespace FlagsEnumTypeConverter
{
	/// <summary>
	/// Flags enumeration type converter.
	/// </summary>
	internal class FlagsEnumConverter: EnumConverter
	{
		/// <summary>
		/// This class represents an enumeration field in the property grid.
		/// </summary>
		protected class EnumFieldDescriptor: SimplePropertyDescriptor
		{
			#region Fields
			/// <summary>
			/// Stores the context which the enumeration field descriptor was created in.
			/// </summary>
			ITypeDescriptorContext fContext;
			#endregion

			#region Methods
			/// <summary>
			/// Creates an instance of the enumeration field descriptor class.
			/// </summary>
			/// <param name="componentType">The type of the enumeration.</param>
			/// <param name="name">The name of the enumeration field.</param>
			/// <param name="context">The current context.</param>
			public EnumFieldDescriptor(Type componentType, string name, ITypeDescriptorContext context): base(componentType, name, typeof(bool))
			{
				fContext = context;
			}

			/// <summary>
			/// Retrieves the value of the enumeration field.
			/// </summary>
			/// <param name="component">
			/// The instance of the enumeration type which to retrieve the field value for.
			/// </param>
			/// <returns>
			/// True if the enumeration field is included to the enumeration; 
			/// otherwise, False.
			/// </returns>
			public override object GetValue(object component)
			{
				return ((int)component & (int)Enum.Parse(ComponentType, Name)) != 0;
			}

			/// <summary>
			/// Sets the value of the enumeration field.
			/// </summary>
			/// <param name="component">
			/// The instance of the enumeration type which to set the field value to.
			/// </param>
			/// <param name="value">
			/// True if the enumeration field should included to the enumeration; 
			/// otherwise, False.
			/// </param>
			public override void SetValue(object component, object value)
			{
				bool myValue = (bool)value;
				int myNewValue;
				if(myValue)
					myNewValue = ((int)component) | (int)Enum.Parse(ComponentType, Name);
				else
					myNewValue = ((int)component) & ~(int)Enum.Parse(ComponentType, Name);
				
				FieldInfo myField = component.GetType().GetField("value__", BindingFlags.Instance | BindingFlags.Public);
				myField.SetValue(component, myNewValue);
				fContext.PropertyDescriptor.SetValue(fContext.Instance, component);
			}

			/// <summary>
			/// Retrieves a value indicating whether the enumeration 
			/// field is set to a non-default value.
			/// </summary>
			public override bool ShouldSerializeValue(object component)
			{
				return (bool)GetValue(component) != GetDefaultValue();
			}

			/// <summary>
			/// Resets the enumeration field to its default value.
			/// </summary>
			public override void ResetValue(object component)
			{
				SetValue(component, GetDefaultValue());
			}

			/// <summary>
			/// Retrieves a value indicating whether the enumeration 
			/// field can be reset to the default value.
			/// </summary>
			public override bool CanResetValue(object component)
			{
				return ShouldSerializeValue(component);
			}

			/// <summary>
			/// Retrieves the enumerations field�s default value.
			/// </summary>
			private bool GetDefaultValue()
			{
				object myDefaultValue = null;
				string myPropertyName = fContext.PropertyDescriptor.Name;
				Type myComponentType = fContext.PropertyDescriptor.ComponentType;

				// Get DefaultValueAttribute
				DefaultValueAttribute myDefaultValueAttribute = (DefaultValueAttribute)Attribute.GetCustomAttribute(
					myComponentType.GetProperty(myPropertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic), 
					typeof(DefaultValueAttribute));
				if(myDefaultValueAttribute != null)
					myDefaultValue = myDefaultValueAttribute.Value;

				if(myDefaultValue != null)
					return ((int)myDefaultValue & (int)Enum.Parse(ComponentType, Name)) != 0;
				return false;
			}
			#endregion

			#region Properties
			public override AttributeCollection Attributes
			{
				get
				{
					return new AttributeCollection(new Attribute[]{RefreshPropertiesAttribute.Repaint});
				}
			}
			#endregion
		}

		#region Methods
		/// <summary>
		/// Creates an instance of the FlagsEnumConverter class.
		/// </summary>
		/// <param name="type">The type of the enumeration.</param>
		public FlagsEnumConverter(Type type): base(type){}

		/// <summary>
		/// Retrieves the property descriptors for the enumeration fields. 
		/// These property descriptors will be used by the property grid 
		/// to show separate enumeration fields.
		/// </summary>
		/// <param name="context">The current context.</param>
		/// <param name="value">A value of an enumeration type.</param>
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			if(context != null)
			{
				Type myType = value.GetType();
				string[] myNames = Enum.GetNames(myType);
				Array myValues = Enum.GetValues(myType);
				if(myNames != null)
				{
					PropertyDescriptorCollection myCollection = new PropertyDescriptorCollection(null);
					for(int i = 0; i < myNames.Length; i++)
					{
						if((int)myValues.GetValue(i) != 0 && myNames[i] != "All")
							myCollection.Add(new EnumFieldDescriptor(myType, myNames[i], context));
					}
					return myCollection;
				}
			}
			return base.GetProperties(context, value, attributes);
		}

		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			if(context != null)
			{
				return true;
			}
			return base.GetPropertiesSupported(context);
		}

		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return false;
		}
		#endregion
	}
}

#pragma warning restore 1573
