using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BassGame
{
    public class NumberTextBox : System.Windows.Forms.TextBox
    {
        private double _maxValue;
        private double _minValue;
        private bool _flag;
        private string _previousValue;

        private string oldValue = string.Empty;
        public NumberTextBox()
        {
            this.TextAlign = HorizontalAlignment.Right;
            //TextAlignment = TextAlignment.Right;
            KeyDown += TextBox_KeyDown;
            TextChanged += TextBox_TextChanged;
            _minValue = double.MinValue;
            _maxValue = double.MaxValue;
        }

         
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            _previousValue = this.Text;
            _flag = this.SelectedText.Length > 0;
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        //private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = this.Text;
            if (text.Length < 1)
            {
                this.Text = oldValue;
                return;
            }
            oldValue = text;
            var cursorPosition = SelectionStart == 0 ? SelectionStart : SelectionStart - 1;
            var insertedChar = text[cursorPosition];
            if (IsInvalidInput(insertedChar, cursorPosition, text))
            {
                HandleText(text, cursorPosition);
            }
            ValidateRange(text, cursorPosition);
        }

        private bool IsInvalidInput(char insertedChar, int cursorPosition, string text)
        {
            return !char.IsDigit(insertedChar) && insertedChar != '.' && insertedChar != '-' ||
                   insertedChar == '-' && cursorPosition != 0 ||
                   text.Count(x => x == '.') > 1 ||
                   text.Count(x => x == '-') > 1;
        }

        private void HandleText(string text, int cursorPosition)
        {
            this.Text = _flag ? _previousValue : text.Remove(cursorPosition, 1);
            this.SelectionStart = cursorPosition;
            this.SelectionLength = 0;
        }

        private void ValidateRange(string text, int cursorPosition)
        {
            try
            {
                if (text == "." || _minValue < 0 && text == "-") return;
                var doubleValue = Convert.ToDouble(text);
                if (doubleValue > _maxValue || doubleValue < _minValue)
                {
                    HandleText(text, cursorPosition);
                }
            }
            catch (Exception)
            {
                HandleText(text, cursorPosition);
            }
        }

        protected void SetProperties(double minValue = double.MinValue, double maxValue = double.MaxValue)
        {
            _minValue = minValue;
            _maxValue = maxValue;
        }
    }
}
