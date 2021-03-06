﻿using SGAM.Elfec.CustomUI.SyntaxHighlight.Highlighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace SGAM.Elfec.CustomUI.SyntaxHighlight
{
    public partial class SyntaxHighlightBox : TextBox
    {

        // --------------------------------------------------------------------
        // Attributes
        // --------------------------------------------------------------------

        #region DefaultForeground

        /// <summary>
        /// Gets or sets DefaultForeground, the default foreground color used
        /// 
        /// </summary>
        public Brush DefaultForeground
        {
            get { return (Brush)this.GetValue(DefaultForegroundProperty); }
            set { this.SetValue(DefaultForegroundProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for DefaultForeground. 
        /// This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty DefaultForegroundProperty =
            DependencyProperty.Register("DefaultForeground", typeof(Brush), typeof(SyntaxHighlightBox),
            new UIPropertyMetadata(Brushes.Black));

        #endregion


        public double LineHeight
        {
            get { return _lineHeight; }
            set
            {
                if (value != _lineHeight)
                {
                    _lineHeight = value;
                    _blockHeight = MaxLineCountInBlock * value;
                    TextBlock.SetLineStackingStrategy(this, LineStackingStrategy.BlockLineHeight);
                    TextBlock.SetLineHeight(this, _lineHeight);
                }
            }
        }

        public int MaxLineCountInBlock
        {
            get { return _maxLineCountInBlock; }
            set
            {
                _maxLineCountInBlock = value > 0 ? value : 0;
                _blockHeight = value * LineHeight;
            }
        }

        public IHighlighter CurrentHighlighter
        {
            get { return _currentHighlighter; }
            set
            {
                _currentHighlighter = value;
                //Force update all text
                InvalidateBlocks(0);
                InvalidateVisual();
            }
        }

        private IHighlighter _currentHighlighter;
        private DrawingControl _renderCanvas;
        private DrawingControl _lineNumbersCanvas;
        private ScrollViewer _scrollViewer;
        private double _lineHeight;
        private int _totalLineCount;
        private List<InnerTextBlock> _blocks;
        private double _blockHeight;
        private int _maxLineCountInBlock;

        // --------------------------------------------------------------------
        // Ctor and event handlers
        // --------------------------------------------------------------------
        static SyntaxHighlightBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SyntaxHighlightBox),
                new FrameworkPropertyMetadata(typeof(SyntaxHighlightBox)));
            TextProperty.OverrideMetadata(typeof(SyntaxHighlightBox),
            new FrameworkPropertyMetadata(typeof(SyntaxHighlightBox))
            { DefaultValue = null, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
        }


        public SyntaxHighlightBox() : base()
        {
            MaxLineCountInBlock = 100;
            LineHeight = FontSize * 1.5;
            _totalLineCount = 1;
            _blocks = new List<InnerTextBlock>();
            Foreground = Brushes.Transparent;
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _renderCanvas = (DrawingControl)GetTemplateChild("PART_RenderCanvas");
            _lineNumbersCanvas = (DrawingControl)GetTemplateChild("PART_LineNumbersCanvas");
            _scrollViewer = (ScrollViewer)GetTemplateChild("PART_ContentHost");

            _lineNumbersCanvas.Width = GetFormattedTextWidth(string.Format("{0:0000}", _totalLineCount)) + 5;
            _scrollViewer.ScrollChanged += OnScrollChanged;

            InvalidateBlocks(0);
            InvalidateVisual();

            SizeChanged += (s, e) =>
            {
                if (e.HeightChanged == false)
                    return;
                UpdateBlocks();
                InvalidateVisual();
            };

        }
        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            UpdateTotalLineCount();
            InvalidateBlocks(e.Changes.First().Offset);
            InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            DrawBlocks();
            base.OnRender(drawingContext);
        }

        private void OnScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.VerticalChange != 0)
                UpdateBlocks();
            InvalidateVisual();
        }

        // -----------------------------------------------------------
        // Updating & Block managing
        // -----------------------------------------------------------

        private void UpdateTotalLineCount()
        {
            _totalLineCount = TextUtilities.GetLineCount(Text);
        }

        private void UpdateBlocks()
        {
            if (_blocks.Count == 0)
                return;

            // While something is visible after last block...
            while (!_blocks.Last().IsLast && _blocks.Last().Position.Y + _blockHeight - VerticalOffset < ActualHeight)
            {
                int firstLineIndex = _blocks.Last().LineEndIndex + 1;
                int lastLineIndex = firstLineIndex + _maxLineCountInBlock - 1;
                lastLineIndex = lastLineIndex <= _totalLineCount - 1 ? lastLineIndex : _totalLineCount - 1;

                int fisrCharIndex = _blocks.Last().CharEndIndex + 1;
                int lastCharIndex = TextUtilities.GetLastCharIndexFromLineIndex(Text, lastLineIndex); // to be optimized (forward search)

                if (lastCharIndex <= fisrCharIndex)
                {
                    _blocks.Last().IsLast = true;
                    return;
                }

                InnerTextBlock block = new InnerTextBlock(
                    fisrCharIndex,
                    lastCharIndex,
                    _blocks.Last().LineEndIndex + 1,
                    lastLineIndex,
                    LineHeight);
                block.RawText = block.GetSubString(Text);
                block.LineNumbers = GetFormattedLineNumbers(block.LineStartIndex, block.LineEndIndex);
                _blocks.Add(block);
                FormatBlock(block, _blocks.Count > 1 ? _blocks[_blocks.Count - 2] : null);
            }
        }

        private void InvalidateBlocks(int changeOffset)
        {
            InnerTextBlock blockChanged = null;
            for (int i = 0; i < _blocks.Count; i++)
            {
                if (_blocks[i].CharStartIndex <= changeOffset && changeOffset <= _blocks[i].CharEndIndex + 1)
                {
                    blockChanged = _blocks[i];
                    break;
                }
            }

            if (blockChanged == null && changeOffset > 0)
                blockChanged = _blocks.Last();

            int fvline = blockChanged != null ? blockChanged.LineStartIndex : 0;
            int lvline = GetIndexOfLastVisibleLine();
            int fvchar = blockChanged != null ? blockChanged.CharStartIndex : 0;
            int lvchar = TextUtilities.GetLastCharIndexFromLineIndex(Text, lvline);

            if (blockChanged != null)
                _blocks.RemoveRange(_blocks.IndexOf(blockChanged), _blocks.Count - _blocks.IndexOf(blockChanged));

            int localLineCount = 1;
            int charStart = fvchar;
            int lineStart = fvline;
            for (int i = fvchar; i < Text.Length; i++)
            {
                if (Text[i] == '\n')
                {
                    localLineCount += 1;
                }
                if (i == Text.Length - 1)
                {
                    string blockText = Text.Substring(charStart);
                    InnerTextBlock block = new InnerTextBlock(
                        charStart,
                        i, lineStart,
                        lineStart + TextUtilities.GetLineCount(blockText) - 1,
                        LineHeight);
                    block.RawText = block.GetSubString(Text);
                    block.LineNumbers = GetFormattedLineNumbers(block.LineStartIndex, block.LineEndIndex);
                    block.IsLast = true;

                    foreach (InnerTextBlock b in _blocks)
                        if (b.LineStartIndex == block.LineStartIndex)
                            throw new Exception();

                    _blocks.Add(block);
                    FormatBlock(block, _blocks.Count > 1 ? _blocks[_blocks.Count - 2] : null);
                    break;
                }
                if (localLineCount > _maxLineCountInBlock)
                {
                    InnerTextBlock block = new InnerTextBlock(
                        charStart,
                        i,
                        lineStart,
                        lineStart + _maxLineCountInBlock - 1,
                        LineHeight);
                    block.RawText = block.GetSubString(Text);
                    block.LineNumbers = GetFormattedLineNumbers(block.LineStartIndex, block.LineEndIndex);

                    foreach (InnerTextBlock b in _blocks)
                        if (b.LineStartIndex == block.LineStartIndex)
                            throw new Exception();

                    _blocks.Add(block);
                    FormatBlock(block, _blocks.Count > 1 ? _blocks[_blocks.Count - 2] : null);

                    charStart = i + 1;
                    lineStart += _maxLineCountInBlock;
                    localLineCount = 1;

                    if (i > lvchar)
                        break;
                }
            }
        }

        // -----------------------------------------------------------
        // Rendering
        // -----------------------------------------------------------

        private void DrawBlocks()
        {
            if (_renderCanvas == null || _lineNumbersCanvas == null)
                return;

            var dc = _renderCanvas.GetContext();
            var dc2 = _lineNumbersCanvas.GetContext();
            for (int i = 0; i < _blocks.Count; i++)
            {
                InnerTextBlock block = _blocks[i];
                Point blockPos = block.Position;
                double top = blockPos.Y - VerticalOffset;
                double bottom = top + _blockHeight;
                if (top < ActualHeight && bottom > 0)
                {
                    try
                    {
                        dc.DrawText(block.FormattedText, new Point(2 - HorizontalOffset, block.Position.Y - VerticalOffset));
                        if (IsLineNumbersMarginVisible)
                        {
                            _lineNumbersCanvas.Width = GetFormattedTextWidth(string.Format("{0:0000}", _totalLineCount)) + 5;
                            dc2.DrawText(block.LineNumbers, new Point(_lineNumbersCanvas.ActualWidth, 1 + block.Position.Y - VerticalOffset));
                        }
                    }
                    catch
                    {
                        // Don't know why this exception is raised sometimes.
                        // Reproduce steps:
                        // - Sets a valid syntax highlighter on the box.
                        // - Copy a large chunk of code in the clipboard.
                        // - Paste it using ctrl+v and keep these buttons pressed.
                    }
                }
            }
            dc.Close();
            dc2.Close();
        }

        // -----------------------------------------------------------
        // Utilities
        // -----------------------------------------------------------

        /// <summary>
        /// Returns the index of the first visible text line.
        /// </summary>
        public int GetIndexOfFirstVisibleLine()
        {
            int guessedLine = (int)(VerticalOffset / _lineHeight);
            return guessedLine > _totalLineCount ? _totalLineCount : guessedLine;
        }

        /// <summary>
        /// Returns the index of the last visible text line.
        /// </summary>
        public int GetIndexOfLastVisibleLine()
        {
            double height = VerticalOffset + ViewportHeight;
            int guessedLine = (int)(height / _lineHeight);
            return guessedLine > _totalLineCount - 1 ? _totalLineCount - 1 : guessedLine;
        }

        /// <summary>
        /// Formats and Highlights the text of a block.
        /// </summary>
        private void FormatBlock(InnerTextBlock currentBlock, InnerTextBlock previousBlock)
        {
            currentBlock.FormattedText = GetFormattedText(currentBlock.RawText);
            if (CurrentHighlighter != null)
            {
                ThreadPool.QueueUserWorkItem(p =>
                {
                    int previousCode = previousBlock != null ? previousBlock.Code : -1;
                    currentBlock.Code = CurrentHighlighter.Highlight(currentBlock.FormattedText, previousCode);
                });
            }
        }

        /// <summary>
        /// Returns a formatted text object from the given string
        /// </summary>
        private FormattedText GetFormattedText(string text)
        {
            FormattedText ft = new FormattedText(
                text,
                System.Globalization.CultureInfo.InvariantCulture,
                FlowDirection.LeftToRight,
                new Typeface(FontFamily, FontStyle, FontWeight, FontStretch),
                FontSize,
                DefaultForeground);

            ft.Trimming = TextTrimming.None;
            ft.LineHeight = _lineHeight;

            return ft;
        }

        /// <summary>
        /// Returns a string containing a list of numbers separated with newlines.
        /// </summary>
        private FormattedText GetFormattedLineNumbers(int firstIndex, int lastIndex)
        {
            string text = "";
            for (int i = firstIndex + 1; i <= lastIndex + 1; i++)
                text += i.ToString() + "\n";
            text = text.Trim();
            FormattedText ft = new FormattedText(
                text,
                System.Globalization.CultureInfo.InvariantCulture,
                FlowDirection.LeftToRight,
                new Typeface(FontFamily, FontStyle, FontWeight, FontStretch),
                FontSize, DefaultForeground);

            ft.Trimming = TextTrimming.None;
            ft.LineHeight = _lineHeight;
            ft.TextAlignment = TextAlignment.Right;

            return ft;
        }

        /// <summary>
        /// Returns the width of a text once formatted.
        /// </summary>
        private double GetFormattedTextWidth(string text)
        {
            FormattedText ft = new FormattedText(
                text,
                System.Globalization.CultureInfo.InvariantCulture,
                FlowDirection.LeftToRight,
                new Typeface(FontFamily, FontStyle, FontWeight, FontStretch),
                FontSize,
                DefaultForeground);

            ft.Trimming = TextTrimming.None;
            ft.LineHeight = _lineHeight;

            return ft.Width;
        }

        // -----------------------------------------------------------
        // Dependency Properties
        // -----------------------------------------------------------

        public static readonly DependencyProperty IsLineNumbersMarginVisibleProperty = DependencyProperty.Register(
            "IsLineNumbersMarginVisible", typeof(bool), typeof(SyntaxHighlightBox), new PropertyMetadata(true));

        public bool IsLineNumbersMarginVisible
        {
            get { return (bool)GetValue(IsLineNumbersMarginVisibleProperty); }
            set { SetValue(IsLineNumbersMarginVisibleProperty, value); }
        }

        // -----------------------------------------------------------
        // Classes
        // -----------------------------------------------------------

        private class InnerTextBlock
        {
            public string RawText { get; set; }
            public FormattedText FormattedText { get; set; }
            public FormattedText LineNumbers { get; set; }
            public int CharStartIndex { get; private set; }
            public int CharEndIndex { get; private set; }
            public int LineStartIndex { get; private set; }
            public int LineEndIndex { get; private set; }
            public Point Position { get { return new Point(0, LineStartIndex * lineHeight); } }
            public bool IsLast { get; set; }
            public int Code { get; set; }

            private double lineHeight;

            public InnerTextBlock(int charStart, int charEnd, int lineStart, int lineEnd, double lineHeight)
            {
                CharStartIndex = charStart;
                CharEndIndex = charEnd;
                LineStartIndex = lineStart;
                LineEndIndex = lineEnd;
                this.lineHeight = lineHeight;
                IsLast = false;

            }

            public string GetSubString(string text)
            {
                return text.Substring(CharStartIndex, CharEndIndex - CharStartIndex + 1);
            }

            public override string ToString()
            {
                return string.Format("L:{0}/{1} C:{2}/{3} {4}",
                    LineStartIndex,
                    LineEndIndex,
                    CharStartIndex,
                    CharEndIndex,
                    FormattedText.Text);
            }
        }
    }
}
