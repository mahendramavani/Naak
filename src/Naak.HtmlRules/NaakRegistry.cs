using Naak.HtmlRules.Impl;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace Naak.HtmlRules
{
	public class NaakRegistry : Registry
	{
		protected override void configure()
		{
			Scan(x =>
			     	{
			     		x.AssemblyContainingType(GetType());
			     		x.With<DefaultConventionScanner>();
			     	});

			ForRequestedType<IHtmlRule>().AddConcreteType<AtLeastOneH1>();
			ForRequestedType<IHtmlRule>().AddConcreteType<FormElementsHaveLabels>();
			ForRequestedType<IHtmlRule>().AddConcreteType<ImageInputsHaveAltText>();
			ForRequestedType<IHtmlRule>().AddConcreteType<TablesHaveColumnHeaders>();
			ForRequestedType<IHtmlRule>().AddConcreteType<FieldsetsHaveLegends>();
			ForRequestedType<IHtmlRule>().AddConcreteType<LabelsRelateToFormElements>();
			ForRequestedType<IHtmlRule>().AddConcreteType<ImagesHaveAltText>();
			ForRequestedType<IHtmlRule>().AddConcreteType<ImagesDontHaveDuplicateAltText>();
			ForRequestedType<IHtmlRule>().AddConcreteType<LinksHaveUniqueTextUrlCombo>();
			ForRequestedType<IHtmlRule>().AddConcreteType<HeadingsAreLogicallyOrdered>();
		}
	}
}