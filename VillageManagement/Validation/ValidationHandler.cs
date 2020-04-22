using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace VillageManagement.Validation
{
    public class ValidationHandler
    {
        private string _maxLengthMsg = "Maximum number of characters is {0}";
        private string _inputIsNotANumber = "Input value should be a number!";
        private string _mustNotBeZero = "The field must not be empty.";

        private Brush _badNewsBrush;
        private Brush _defaultBrush;

        public ValidationHandler() { _badNewsBrush = Brushes.Black; _defaultBrush = Brushes.Black; }

        public ValidationHandler(Brush badNewsBrush) { _badNewsBrush = badNewsBrush; _defaultBrush = Brushes.Black; }

        /// <summary>
        /// Checks if number of TextBox characters is smaller than a max number.
        /// </summary>
        /// <param name="textBox">TextBox which should be checked.</param>
        /// <param name="maxChars">Check number.</param>
        /// <returns>A ValidationMessage with the corresponding result.</returns>
        private  ValidationMessage CheckTextBoxInputForLength(TextBox textBox, int maxChars)
        {
            if(textBox.Text.Length >= maxChars)
            {
                return new ValidationMessage
                {
                    IsValid = false,
                    Message = string.Format(_maxLengthMsg, maxChars)
                };
            }

            return new ValidationMessage { IsValid = true };

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="textBox">TextBox which should be checked.</param>
        /// <param name="maxChars">Check number.</param>
        /// <returns>A ValidationMessage with the corresponding result.</returns>
        public ValidationMessage CheckTextBoxInputForRange(TextBox textBox, int maxChars)
        {
            if(textBox.Text == null || textBox.Text.Length < 1)
            {
                return new ValidationMessage {
                    IsValid = false,
                    Message = _mustNotBeZero
                };
            }

            var validMsg = CheckTextBoxInputForLength(textBox, maxChars);
            return validMsg;
        }

        public ValidationMessage CheckTextBoxForNumbers(TextBox textBox)
        {
            if (!int.TryParse(textBox.Text, out _))
            {
                return new ValidationMessage
                {
                    IsValid = false,
                    Message = _inputIsNotANumber
                };
            }

            return new ValidationMessage { IsValid = true };
        }

        public ValidationMessage CheckTextBoxCompleteForNumbers(TextBox textBox, int maxChars)
        {
            var validationMsg = CheckTextBoxInputForRange(textBox, maxChars);
            if (!validationMsg.IsValid)
            {
                return validationMsg;
            }

            validationMsg = CheckTextBoxForNumbers(textBox);
            return validationMsg;
        }

        public void SetBadNewsLabel(Label label, string msg)
        {
            label.Content = msg;
            label.Foreground = _badNewsBrush;
        }

        public void SetDefaultLabel(Label label)
        {
            label.Content = string.Empty;
            label.Foreground = _defaultBrush;
        }
    }
}
