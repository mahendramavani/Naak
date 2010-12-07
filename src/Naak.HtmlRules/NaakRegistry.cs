using Naak.HtmlRules.Impl;
using StructureMap.Configuration.DSL;

namespace Naak.HtmlRules
{
	public class NaakRegistry : Registry
	{
	    public NaakRegistry()
	    {
            Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.WithDefaultConventions();
            });

            For<IHtmlRule>().Add<AtLeastOneH1>();
            For<IHtmlRule>().Add<FormElementsHaveLabels>();
            For<IHtmlRule>().Add<ImageInputsHaveAltText>();
            For<IHtmlRule>().Add<TablesHaveColumnHeaders>();
            For<IHtmlRule>().Add<FieldsetsHaveLegends>();
            For<IHtmlRule>().Add<LabelsRelateToFormElements>();
            For<IHtmlRule>().Add<ImagesHaveAltText>();
            For<IHtmlRule>().Add<ImagesDontHaveDuplicateAltText>();
            For<IHtmlRule>().Add<ContextOfLinkTextMustMakeSense>();
            For<IHtmlRule>().Add<HeadingsAreLogicallyOrdered>();
        }
	}
}
