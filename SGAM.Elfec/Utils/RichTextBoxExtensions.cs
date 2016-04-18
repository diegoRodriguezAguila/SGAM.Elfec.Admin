using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace SGAM.Elfec.Utils
{
    public static class RichTextBoxExtensions
    {
        private static SolidColorBrush black = new SolidColorBrush(Colors.Black);
        /// <summary>
        /// This method highlights a word with a given color in a WPF RichTextBox
        /// </summary>
        /// <param name="richTextBox">RichTextBox Control</param>
        /// <param name="word">The word which you need to highlighted</param>
        /// <param name="color">The color with which you highlight</param>
        public static void HighlightWord(this RichTextBox richTextBox, string word, SolidColorBrush color)
        {
            TextPointer pointer = richTextBox.Document.ContentStart;
            TextRange tr = FindWordFromPosition(pointer, word);
            while (tr != null)
            {
                tr.ApplyPropertyValue(TextElement.ForegroundProperty, color);
                TextPointer next = tr.End.GetPositionAtOffset(1);
                if (tr.End != richTextBox.Document.ContentEnd)
                    (new TextRange(tr.End, next)).ApplyPropertyValue(TextElement.ForegroundProperty, black);
                pointer = pointer.GetPositionAtOffset(word.Length);
                tr = FindWordFromPosition(pointer, word);
            }
        }

        private static TextRange FindWordFromPosition(TextPointer position, string word)
        {
            while (position != null)
            {
                if (position.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text)
                {
                    string textRun = position.GetTextInRun(LogicalDirection.Forward);

                    // Find the starting index of any substring that matches "word".
                    int indexInRun = textRun.IndexOf(word);
                    if (indexInRun >= 0)
                    {
                        TextPointer start = position.GetPositionAtOffset(indexInRun);
                        TextPointer end = start.GetPositionAtOffset(word.Length);
                        return new TextRange(start, end);
                    }
                }

                position = position.GetNextContextPosition(LogicalDirection.Forward);
            }

            // position will be null if "word" is not found.
            return null;
        }
    }
}
