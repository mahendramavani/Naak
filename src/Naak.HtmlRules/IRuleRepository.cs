namespace Naak.HtmlRules
{
	public interface IRuleRepository
	{
		IHtmlRule[] GetNaakRulesToExecute();
	}
}