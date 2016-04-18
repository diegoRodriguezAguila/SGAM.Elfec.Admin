using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace SGAM.Elfec.CustomUI.SyntaxHighlight.Rules
{
    /// <summary>
    /// A set of words and their RuleOptions.
    /// </summary>
    public class HighlightWordsRule : IHighlightRule
    {
        public List<string> Words { get; private set; }
        public Regex Expression { get; private set; }
        public RuleOptions Options { get; private set; }

        public HighlightWordsRule(XElement rule)
        {
            Words = new List<string>();
            Options = new RuleOptions(rule);
            string wordsStr = rule.Element("Words").Value;
            wordsStr = wordsStr.Replace("  ", " ").Replace(" ", "|");
            Expression = new Regex("\b(" + wordsStr + ")\b");
            string[] words = Regex.Split(wordsStr, "\\s+");

            foreach (string word in words)
                if (!string.IsNullOrWhiteSpace(word))
                    Words.Add(word);
        }

        public MatchCollection Satisfies(string text)
        {
            return Options.IgnoreCase ? Regex.Matches(text,
                Expression.ToString(), RegexOptions.IgnoreCase) : Expression.Matches(text);
        }
    }
}
