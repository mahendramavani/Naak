using System.ComponentModel.Composition;

namespace Naak.HtmlRules
{
    public class HtmlRuleAttribute : ExportAttribute
    {
        public HtmlRuleAttribute() : base(typeof(IHtmlRule)) { }
    }
}