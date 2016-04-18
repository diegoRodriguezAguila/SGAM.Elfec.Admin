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
        public RuleOptions Options { get; private set; }

        public HighlightWordsRule(XElement rule)
        {
            Words = new List<string>();
            Options = new RuleOptions(rule);

            string wordsStr = rule.Element("Words").Value;
            string[] words = Regex.Split(wordsStr, "\\s+");

            foreach (string word in words)
                if (!string.IsNullOrWhiteSpace(word))
                    Words.Add(word.Trim());
        }
    }
}
