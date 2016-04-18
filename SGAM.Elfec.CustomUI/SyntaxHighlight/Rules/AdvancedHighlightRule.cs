using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace SGAM.Elfec.CustomUI.SyntaxHighlight.Rules
{
    /// <summary>
    /// A regex and its RuleOptions.
    /// </summary>
    public class AdvancedHighlightRule : IHighlightRule
    {
        public Regex Expression { get; private set; }
        public RuleOptions Options { get; private set; }

        public AdvancedHighlightRule(XElement rule)
        {
            Expression = new Regex(rule.Element("Expression").Value.Trim());
            Options = new RuleOptions(rule);
        }

        public MatchCollection Satisfies(string text)
        {
            return Expression.Matches(text);
        }
    }
}
