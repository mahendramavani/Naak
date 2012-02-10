using HtmlAgilityPack;

namespace Naak.HtmlRules.Impl
{
    public static class HtmlDocumentExtensions
    {
        public static HtmlNodeCollection SelectNodes(this HtmlDocument document, string xpath)
        {
            var node = document.DocumentNode;
            return node.SelectNodes(xpath) ?? new HtmlNodeCollection(node);
        }

        public static HtmlNode SelectSingleNode(this HtmlDocument document, string xpath)
        {
            return document.DocumentNode.SelectSingleNode(xpath);
        }
    }
}