using System.Xml.Linq;

namespace SGAM.Elfec.CustomUI.SyntaxHighlight.Rules
{
    /// <summary>
    /// A regex and its RuleOptions.
    /// </summary>
    public class AdvancedHighlightRule : IHighlightRule
    {
        public string Expression { get; private set; }
        public RuleOptions Options { get; private set; }

        public AdvancedHighlightRule(XElement rule)
        {
            Expression = rule.Element("Expression").Value.Trim();
            Options = new RuleOptions(rule);
        }
    }
}
