using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace SGAM.Elfec.CustomUI.SyntaxHighlight.Rules
{
    /// <summary>
    /// A line start definition and its RuleOptions.
    /// </summary>
    public class HighlightLineRule : IHighlightRule
    {
        public Regex Expression { get; private set; }
        public string LineStart { get; private set; }
        public RuleOptions Options { get; private set; }

        public HighlightLineRule(XElement rule)
        {
            LineStart = rule.Element("LineStart").Value.Trim();
            Expression = new Regex(Regex.Escape(LineStart) + ".*");
            Options = new RuleOptions(rule);
        }

        public MatchCollection Satisfies(string text)
        {
            return Expression.Matches(text);
        }
    }
}
