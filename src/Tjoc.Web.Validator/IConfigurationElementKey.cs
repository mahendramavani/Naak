namespace Tjoc.Web.Validator
{
	/// <summary>
	/// This allows a constraint to be enforced on the <c>ConfigurationElementCollection</c>
	/// that makes the whole concept feasible.
	/// </summary>
	public interface IConfigurationElementKey
	{
		/// <summary>
		/// The unique key for a ConfigurationElement that is
		/// placed in a collection.
		/// </summary>
		string Key { get; set; }
	}
}