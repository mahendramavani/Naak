using System.Configuration;

namespace Tjoc.Web.Validator
{
	/// <summary>
	/// Used to quickly create ConfigurationElementCollections for use
	/// with custom configuration sections.
	/// </summary>
	/// <typeparam name="T">A type that inherits ConfigurationElement and implements IConfigurationElementKey</typeparam>
	public class ConfigurationElementCollection<T> : ConfigurationElementCollection
		where T : ConfigurationElement, IConfigurationElementKey, new()
	{
		public ConfigurationElementCollection()
		{
			var t = (T) CreateNewElement();
			Add(t);
		}

		public override ConfigurationElementCollectionType CollectionType
		{
			get { return ConfigurationElementCollectionType.AddRemoveClearMap; }
		}

		protected override ConfigurationElement CreateNewElement()
		{
			return new T();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((T) element).Key;
		}

		public T this[int index]
		{
			get { return (T) BaseGet(index); }
			set
			{
				if (BaseGet(index) != null)
				{
					BaseRemoveAt(index);
				}
				BaseAdd(index, value);
			}
		}

		public new T this[string name]
		{
			get { return (T) BaseGet(name); }
		}

		public int IndexOf(T t)
		{
			return BaseIndexOf(t);
		}

		public void Add(T t)
		{
			BaseAdd(t);
		}

		protected override void BaseAdd(ConfigurationElement element)
		{
			BaseAdd(element, false);
		}

		public void Remove(T t)
		{
			if (BaseIndexOf(t) >= 0)
				BaseRemove(t.Key);
		}

		public void RemoveAt(int index)
		{
			BaseRemoveAt(index);
		}

		public void Remove(string name)
		{
			BaseRemove(name);
		}

		public void Clear()
		{
			BaseClear();
		}
	}
}