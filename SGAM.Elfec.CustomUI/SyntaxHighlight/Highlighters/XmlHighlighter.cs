using SGAM.Elfec.CustomUI.SyntaxHighlight.Rules;
using System.Xml.Linq;

namespace SGAM.Elfec.CustomUI.SyntaxHighlight.Highlighters
{
    /// <summary>
    /// An IHighlighter built from an Xml syntax file
    /// </summary>
    public class XmlHighlighter : BaseHighlighter
    {

        public XmlHighlighter(XElement root) : base()
        {
            foreach (XElement elem in root.Elements())
            {
                switch (elem.Name.ToString())
                {
                    case "HighlightWordsRule": WordsRules.Add(new HighlightWordsRule(elem)); break;
                    case "HighlightLineRule": LineRules.Add(new HighlightLineRule(elem)); break;
                    case "AdvancedHighlightRule": RegexRules.Add(new AdvancedHighlightRule(elem)); break;
                }
            }
        }
    }
}
