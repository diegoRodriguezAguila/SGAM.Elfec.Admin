using System.Text.RegularExpressions;

namespace SGAM.Elfec.CustomUI.SyntaxHighlight.Rules
{
    /// <summary>
    /// Interface abstraction of highlighting rules
    /// </summary>
    public interface IHighlightRule
    {
        Regex Expression { get; }
        RuleOptions Options { get; }
        /// <summary>
        /// Returns the match collection that satisfies the rule
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        MatchCollection Satisfies(string text);
    }
}
