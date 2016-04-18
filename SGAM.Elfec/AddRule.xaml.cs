using SGAM.Elfec.Presenters.Views;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace SGAM.Elfec
{
    /// <summary>
    /// Interaction logic for AddRule.xaml
    /// </summary>
    public partial class AddRule : UserControl, IAddRuleView
    {
        public AddRule()
        {
            InitializeComponent();
            TxtValue.CurrentHighlighter = HighlighterManager.Instance.Highlighters["RuleSyntax"];
            TxtValue.LineHeight = 19;
            TxtException.CurrentHighlighter = HighlighterManager.Instance.Highlighters["RuleSyntax"];
        }
    }
}
