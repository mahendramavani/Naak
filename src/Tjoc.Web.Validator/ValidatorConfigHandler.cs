using System.Configuration;

namespace Tjoc.Web.Validator
{
	public class ValidatorConfigHandler : ConfigurationSection
	{
		[ConfigurationProperty("enabled", DefaultValue = "true", IsRequired = false)]
		public bool Enabled
		{
			get { return bool.Parse(this["enabled"].ToString()); }
			set { this["enabled"] = value.ToString(); }
		}

		[ConfigurationProperty("mode", IsRequired = true)]
		public Mode Mode
		{
			get { return (Mode) this["mode"]; }
			set { this["mode"] = value; }
		}

		[ConfigurationProperty("customRenderer", IsRequired = false)]
		public string CustomRenderer
		{
			get { return this["customRenderer"].ToString(); }
			set { this["customRenderer"] = value; }
		}

		[ConfigurationProperty("pageExtensions", IsRequired = false)]
		[ConfigurationCollection(typeof (ConfigurationElementCollection<StringElement>), AddItemName = "add")]
		public ConfigurationElementCollection<StringElement> PageExtensions
		{
			get { return (ConfigurationElementCollection<StringElement>) this["pageExtensions"]; }
		}

		[ConfigurationProperty("contentTypes", IsRequired = false)]
		[ConfigurationCollection(typeof (ConfigurationElementCollection<StringElement>), AddItemName = "add")]
		public ConfigurationElementCollection<StringElement> ContentTypes
		{
			get { return (ConfigurationElementCollection<StringElement>) this["contentTypes"]; }
		}
	}


	public class StringElement : ConfigurationElement, IConfigurationElementKey
	{
		[ConfigurationProperty("value", IsKey = true, IsRequired = true)]
		public string Key
		{
			get { return (string) this["value"]; }
			set { this["value"] = value; }
		}
	}

	public enum Mode
	{
		Html,
		HtmlFloat,
		Comments,
		Custom
	}
}