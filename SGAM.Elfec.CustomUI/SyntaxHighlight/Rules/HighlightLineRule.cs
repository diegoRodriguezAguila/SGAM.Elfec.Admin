using System.Xml.Linq;

namespace SGAM.Elfec.CustomUI.SyntaxHighlight.Rules
{
    /// <summary>
    /// A line start definition and its RuleOptions.
    /// </summary>
    public class HighlightLineRule : IHighlightRule
    {
        public string LineStart { get; private set; }
        public RuleOptions Options { get; private set; }

        public HighlightLineRule(XElement rule)
        {
            LineStart = rule.Element("LineStart").Value.Trim();
            Options = new RuleOptions(rule);
        }
    }
}
