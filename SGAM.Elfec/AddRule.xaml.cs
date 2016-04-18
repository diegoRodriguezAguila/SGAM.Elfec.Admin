﻿using SGAM.Elfec.Presenters.Views;
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
            TxtException.CurrentHighlighter = HighlighterManager.Instance.Highlighters["RuleSyntax"];
        }
    }
}
