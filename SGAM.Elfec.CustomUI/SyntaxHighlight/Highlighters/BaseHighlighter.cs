using SGAM.Elfec.CustomUI.SyntaxHighlight.Rules;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Media;

namespace SGAM.Elfec.CustomUI.SyntaxHighlight.Highlighters
{
    /// <summary>
    /// Base highlighter which does the job of highlighting checking against the
    /// set of rules
    /// </summary>
    public abstract class BaseHighlighter : IHighlighter
    {
        public IList<HighlightWordsRule> WordsRules;
        public IList<HighlightLineRule> LineRules;
        public IList<AdvancedHighlightRule> RegexRules;

        public BaseHighlighter()
        {
            WordsRules = new List<HighlightWordsRule>();
            LineRules = new List<HighlightLineRule>();
            RegexRules = new List<AdvancedHighlightRule>();
        }

        public int Highlight(FormattedText text, int previousBlockCode)
        {
            // WORDS RULES
            Regex wordsRgx = new Regex("[a-zA-Z_][a-zA-Z0-9_]*");
            foreach (Match m in wordsRgx.Matches(text.Text))
            {
                foreach (HighlightWordsRule rule in WordsRules)
                {
                    foreach (string word in rule.Words)
                    {
                        if (string.Equals(m.Value, word, rule.Options.IgnoreCase ?
                            StringComparison.InvariantCultureIgnoreCase : StringComparison.InvariantCulture))
                        {
                            ApplyFormat(text, rule.Options, m.Index, m.Length);
                        }
                    }
                }
            }
            // REGEX RULES
            foreach (AdvancedHighlightRule rule in RegexRules)
            {
                Regex regexRgx = new Regex(rule.Expression);
                foreach (Match m in regexRgx.Matches(text.Text))
                {
                    ApplyFormat(text, rule.Options, m.Index, m.Length);
                }
            }
            // LINES RULES
            foreach (HighlightLineRule rule in LineRules)
            {
                Regex lineRgx = new Regex(Regex.Escape(rule.LineStart) + ".*");
                foreach (Match m in lineRgx.Matches(text.Text))
                {
                    ApplyFormat(text, rule.Options, m.Index, m.Length);
                }
            }

            return -1;
        }

        /// <summary>
        /// Applies a format in rule options to the formatted text
        /// </summary>
        /// <param name="text"></param>
        /// <param name="options"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        public void ApplyFormat(FormattedText text, RuleOptions options, int index, int length)
        {
            text.SetForegroundBrush(options.Foreground, index, length);
            text.SetFontWeight(options.FontWeight, index, length);
            text.SetFontStyle(options.FontStyle, index, length);
        }
    }
}
